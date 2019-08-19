using System;
using System.Collections.Generic;
using System.Text;

namespace PBIBling.Serializacao
{
    class ContatosBling
    {
        public class Contato2
        {
            public string id { get; set; }
            public string codigo { get; set; }
            public string nome { get; set; }
            public string fantasia { get; set; }
            public string tipo { get; set; }
            public string cnpj { get; set; }
            public string ie_rg { get; set; }
            public string endereco { get; set; }
            public string numero { get; set; }
            public string bairro { get; set; }
            public string cep { get; set; }
            public string cidade { get; set; }
            public string complemento { get; set; }
            public string uf { get; set; }
            public string fone { get; set; }
            public string email { get; set; }
            public string limiteCredito { get; set; }
            public string situacao { get; set; }
            public string contribuinte { get; set; }
            public string site { get; set; }
            public string celular { get; set; }
            public string dataNascimento { get; set; }
        }

        public class Contato
        {
            public Contato2 contato { get; set; }
        }

        public class Retorno
        {
            public List<Contato> contatos { get; set; }
        }

        public class RootObject
        {
            public Retorno retorno { get; set; }
        }
    }
}
