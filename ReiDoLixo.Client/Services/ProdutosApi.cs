using System.Net.Http.Json;
using ReiDoLixo.Client.Models;

namespace ReiDoLixo.Client.Services;

public class ProdutosApi
{
    private readonly HttpClient _http;
    public ProdutosApi(HttpClient http) => _http = http;

    public Task<List<ProdutoDto>?> GetAll()
        => _http.GetFromJsonAsync<List<ProdutoDto>>("api/produtos");

    public Task<ProdutoDto?> GetById(int id)
        => _http.GetFromJsonAsync<ProdutoDto>($"api/produtos/{id}");

    public async Task Create(ProdutoDto p)
    {
        var resp = await _http.PostAsJsonAsync("api/produtos", new
        {
            p.Nome,
            p.Preco,
            EstoqueAtual = p.EstoqueAtual,
            p.ImagemUrl,
            p.Ativo
        });
        resp.EnsureSuccessStatusCode();
    }

    public async Task Update(int id, ProdutoDto p)
    {
        var resp = await _http.PutAsJsonAsync($"api/produtos/{id}", new
        {
            p.Nome,
            p.Preco,
            EstoqueAtual = p.EstoqueAtual,
            p.ImagemUrl,
            p.Ativo
        });
        resp.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id)
    {
        var resp = await _http.DeleteAsync($"api/produtos/{id}");
        resp.EnsureSuccessStatusCode();
    }
}
