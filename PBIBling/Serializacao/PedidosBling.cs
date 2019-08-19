using System;
using System.Collections.Generic;
using System.Text;

namespace PBIBling.Serializacao
{
    class PedidosBling
    {
        public class Cliente
        {
            public string nome { get; set; }
            public string cnpj { get; set; }
            public string ie { get; set; }
            public string rg { get; set; }
            public string endereco { get; set; }
            public string numero { get; set; }
            public string complemento { get; set; }
            public string cidade { get; set; }
            public string bairro { get; set; }
            public string cep { get; set; }
            public string uf { get; set; }
            public string email { get; set; }
            public string celular { get; set; }
            public string fone { get; set; }
        }

        public class Item
        {
            public string codigo { get; set; }
            public string descricao { get; set; }
            public string quantidade { get; set; }
            public string valorunidade { get; set; }
            public string precocusto { get; set; }
            public string descontoItem { get; set; }
            public string un { get; set; }
        }

        public class Iten
        {
            public Item item { get; set; }
        }

        public class Pagamento
        {
            public string categoria { get; set; }
        }

        public class FormaPagamento
        {
            public int id { get; set; }
            public string descricao { get; set; }
            public int codigoFiscal { get; set; }
        }

        public class Parcela2
        {
            public string valor { get; set; }
            public string dataVencimento { get; set; }
            public string obs { get; set; }
            public FormaPagamento forma_pagamento { get; set; }
        }

        public class Parcela
        {
            public Parcela2 parcela { get; set; }
        }

        public class Remessa
        {
            public string numero { get; set; }
            public string dataCriacao { get; set; }
        }

        public class Dimensoes
        {
            public string peso { get; set; }
            public string altura { get; set; }
            public string largura { get; set; }
            public string comprimento { get; set; }
            public string diametro { get; set; }
        }

        public class Volume2
        {
            public string idServico { get; set; }
            public string servico { get; set; }
            public string codigoRastreamento { get; set; }
            public string valorFretePrevisto { get; set; }
            public Remessa remessa { get; set; }
            public string dataSaida { get; set; }
            public string prazoEntregaPrevisto { get; set; }
            public string valorDeclarado { get; set; }
            public Dimensoes dimensoes { get; set; }
        }

        public class Volume
        {
            public Volume2 volume { get; set; }
        }

        public class Transporte
        {
            public string transportadora { get; set; }
            public string tipo_frete { get; set; }
            public List<Volume> volumes { get; set; }
            public string servico_correios { get; set; }
        }

        public class Nota
        {
            public string serie { get; set; }
            public string numero { get; set; }
            public string dataEmissao { get; set; }
            public string situacao { get; set; }
            public string valorNota { get; set; }
            public string chaveAcesso { get; set; }
        }

        public class CodigosRastreamento
        {
            public string codigoRastreamento { get; set; }
        }

        public class Pedido2
        {
            public string desconto { get; set; }
            public string observacoes { get; set; }
            public string observacaointerna { get; set; }
            public string data { get; set; }
            public string numero { get; set; }
            public string vendedor { get; set; }
            public string valorfrete { get; set; }
            public string totalprodutos { get; set; }
            public string totalvenda { get; set; }
            public string situacao { get; set; }
            public Cliente cliente { get; set; }
            public List<Iten> itens { get; set; }
            public Pagamento pagamento { get; set; }
            public List<Parcela> parcelas { get; set; }
            public Transporte transporte { get; set; }
            public Nota nota { get; set; }
            public string dataPrevista { get; set; }
            public string tipoIntegracao { get; set; }
            public string numeroPedidoLoja { get; set; }
            public CodigosRastreamento codigosRastreamento { get; set; }
        }

        public class Pedido
        {
            public Pedido2 pedido { get; set; }
        }

        public class Retorno
        {
            public List<Pedido> pedidos { get; set; }
        }

        public class RootObject
        {
            public Retorno retorno { get; set; }
        }
    }
}
