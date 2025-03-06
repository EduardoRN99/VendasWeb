using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Comum
{
    public class Pessoa
    {
        private int idPessoa;
        private string nome;
        private string endereco;
        private string cidade;
        private string uf;
        private string foneCelular;
        private string dddCelular;
        private string cep;
        private string cpf;
        private DateTime dataNascimento;
        private string sexo;
        private string estadoCivil;

        public int IdPessoa { get => idPessoa; set => idPessoa = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Uf { get => uf; set => uf = value; }
        public string FoneCelular { get => foneCelular; set => foneCelular = value; }
        public string DddCelular { get => dddCelular; set => dddCelular = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string EstadoCivil { get => estadoCivil; set => estadoCivil = value; }
    }
}
