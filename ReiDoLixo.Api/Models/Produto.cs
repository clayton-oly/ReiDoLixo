namespace ReiDoLixo.Api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int EstoqueAtual { get; set; }
        public string ImagemUrl { get; set; }
        public bool Ativo { get; set; }
    }
}
