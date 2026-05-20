namespace api_doceria.Dtos
{
    public class ProdutoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;
        public int IdAdmin { get; set; }
    }

    public class ProdutoReadDto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }

    public class ProdutoUpdateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }
}
