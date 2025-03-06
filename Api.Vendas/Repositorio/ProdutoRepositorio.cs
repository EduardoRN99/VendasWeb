using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Comum;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Vendas.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly string _connectionString;

        public ProdutoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Produto>("SELECT * FROM Produto");
            }
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var produto = await connection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produto WHERE IdProduto = @Id", new { Id = id });

                if (produto == null)
                {
                    throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");
                }

                return produto;
            }
        }

        public async Task<int> InserirAsync(Produto produto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"INSERT INTO Produto (Nome, Descricao, Preco)
                            VALUES (@Nome, @Descricao, @Preco);
                            SELECT LAST_INSERT_ID();"; // MySQL usa LAST_INSERT_ID() para obter o ID gerado
                return await connection.ExecuteScalarAsync<int>(sql, produto);
            }
        }

        public async Task<bool> AtualizarAsync(Produto produto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"UPDATE Produto SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco
                            WHERE IdProduto = @IdProduto";
                var affectedRows = await connection.ExecuteAsync(sql, produto);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "DELETE FROM Produto WHERE IdProduto = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}