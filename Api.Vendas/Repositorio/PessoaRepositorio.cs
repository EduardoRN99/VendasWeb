using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Comum;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Vendas.Repositorio
{
    public class PessoaRepositorio
    {
        private readonly string _connectionString;

        public PessoaRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Pessoa>("SELECT * FROM Pessoa");
            }
        }

        public async Task<Pessoa> ObterPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var pessoa = await connection.QueryFirstOrDefaultAsync<Pessoa>("SELECT * FROM Pessoa WHERE IdPessoa = @Id", new { Id = id });

                if (pessoa == null)
                {
                    throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada.");
                }

                return pessoa;
            }
        }

        public async Task<int> InserirAsync(Pessoa pessoa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"INSERT INTO Pessoa (Nome, Endereco, Cidade, Uf, FoneCelular, DddCelular, Cep, Cpf, DataNascimento, Sexo, EstadoCivil)
                            VALUES (@Nome, @Endereco, @Cidade, @Uf, @FoneCelular, @DddCelular, @Cep, @Cpf, @DataNascimento, @Sexo, @EstadoCivil);
                            SELECT CAST(SCOPE_IDENTITY() as int);";
                return await connection.ExecuteScalarAsync<int>(sql, pessoa);
            }
        }

        public async Task<bool> AtualizarAsync(Pessoa pessoa)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"UPDATE Pessoa SET Nome = @Nome, Endereco = @Endereco, Cidade = @Cidade, Uf = @Uf, FoneCelular = @FoneCelular, 
                            DddCelular = @DddCelular, Cep = @Cep, Cpf = @Cpf, DataNascimento = @DataNascimento, Sexo = @Sexo, EstadoCivil = @EstadoCivil
                            WHERE IdPessoa = @IdPessoa";
                var affectedRows = await connection.ExecuteAsync(sql, pessoa);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "DELETE FROM Pessoa WHERE IdPessoa = @Id";
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}