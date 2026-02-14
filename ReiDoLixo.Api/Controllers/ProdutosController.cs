using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReiDoLixo.Api.Data;
using ReiDoLixo.Api.DTOs;
using ReiDoLixo.Api.Models;
using System;

namespace ReiDoLixo.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<ProdutoDto>>> GetAll()
    {
        var list = await _db.Produtos
            .OrderByDescending(p => p.Ativo)
            .ThenBy(p => p.Nome)
            .Select(p => new ProdutoDto(p.Id, p.Nome, p.Preco, p.EstoqueAtual, p.ImagemUrl, p.Ativo))
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var p = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();

        return Ok(new ProdutoDto(p.Id, p.Nome, p.Preco, p.EstoqueAtual, p.ImagemUrl, p.Ativo));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create(ProdutoCreateDto dto)
    {
        var p = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            EstoqueAtual = dto.EstoqueAtual,
            ImagemUrl = dto.ImagemUrl,
            Ativo = dto.Ativo
        };

        _db.Produtos.Add(p);
        await _db.SaveChangesAsync();

        var result = new ProdutoDto(p.Id, p.Nome, p.Preco, p.EstoqueAtual, p.ImagemUrl, p.Ativo);
        return CreatedAtAction(nameof(GetById), new { id = p.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProdutoUpdateDto dto)
    {
        var p = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();

        p.Nome = dto.Nome;
        p.Preco = dto.Preco;
        p.EstoqueAtual = dto.EstoqueAtual;
        p.ImagemUrl = dto.ImagemUrl;
        p.Ativo = dto.Ativo;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();

        _db.Produtos.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
