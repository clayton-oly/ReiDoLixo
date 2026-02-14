using System.ComponentModel.DataAnnotations;

namespace ReiDoLixo.Client.Models;

public class ProdutoDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public int EstoqueAtual { get; set; }

    public string? ImagemUrl { get; set; }

    public bool Ativo { get; set; } = true;
}
