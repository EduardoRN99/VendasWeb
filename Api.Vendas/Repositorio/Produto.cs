﻿namespace Api.Vendas.Repositorio
{
    public class Produto
    { 
        public int Id { get; set; }
        public string Descricao {  get; set; }
        public decimal? Preco {  get; set; }
        public string Categoria { get; set; }
        public string CodigoDeBarras { get; set; }
    }
}