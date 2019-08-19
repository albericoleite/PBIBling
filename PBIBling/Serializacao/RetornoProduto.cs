using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBIBling.Serializacao
{
    class RetornoProduto
    {
        public Retorno retorno { get; set; }
        public class Retorno
        {
            public List<Produtos> produtos { get; set; }
        }

        public class Produtos
        {
            public Produto produto { get; set; }
        }

        public class Imagem
        {
            public string link { get; set; }
            public string validade { get; set; }
            public string tipoArmazenamento { get; set; }
        }

        public class Variacao
        {
            public string nome { get; set; }
            public string codigo { get; set; }
        }

        public class Variaco
        {
            public Variacao variacao { get; set; }
        }

        public class Produto
        {
            public string codigo { get; set; }
            public string descricao { get; set; }
            public string tipo { get; set; }
            public string situacao { get; set; }
            public string unidade { get; set; }
            public string preco { get; set; }
            public string precoCusto { get; set; }
            public string descricaoCurta { get; set; }
            public string descricaoComplementar { get; set; }
            public string dataInclusao { get; set; }
            public string dataAlteracao { get; set; }
            public string imageThumbnail { get; set; }
            public string nomeFornecedor { get; set; }
            public string codigoFabricante { get; set; }
            public string marca { get; set; }
            public string class_fiscal { get; set; }
            public string cest { get; set; }
            public string origem { get; set; }
            public object grupoProduto { get; set; }
            public string garantia { get; set; }
            public string pesoLiq { get; set; }
            public string pesoBruto { get; set; }
            public string estoqueMinimo { get; set; }
            public string estoqueMaximo { get; set; }
            public string gtin { get; set; }
            public string gtinEmbalagem { get; set; }
            public string larguraProduto { get; set; }
            public string alturaProduto { get; set; }
            public string profundidadeProduto { get; set; }
            public string unidadeMedida { get; set; }
            public string localizacao { get; set; }
            public string crossdocking { get; set; }
            public List<Imagem> imagem { get; set; }
            public string codigoPai { get; set; }
            public List<Variaco> variacoes { get; set; }
            public int estoqueAtual { get; set; }
            public Depositos[] depositos { get; set; }
        }

        public class Depositos
        {
            public Deposito deposito { get; set; }
        }

        public class Deposito
        {
            public string id { get; set; }
            public string nome { get; set; }
            public decimal saldo { get; set; }
        }
    }


}
