using Newtonsoft.Json;
using PBIBling.Serializacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PBIBling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Captura de Dados!");

            string apikey = string.Empty;
            apikey = args[0].ToString();

            //if (args.Where(x => !x.StartsWith("log")).Count() > 0)
            //{
            //    if (args.Contains("1"))
            //    {
            //        apikey = args[0].ToString();
            //    }
            //}

            //if (args.Contains("1"))
            //{
            //    apikey = args[0].ToString();
            //}

            if (!string.IsNullOrEmpty(apikey))
            {

                var json = GetPedidos(apikey);

                string ret = JsonConvert.SerializeObject(json);

                System.IO.File.WriteAllText(@"c:\pedidos.json", ret.ToString());


                var jsonp = GetProducts(apikey);

                string retp = JsonConvert.SerializeObject(jsonp);

                System.IO.File.WriteAllText(@"c:\produtos.json", retp.ToString());

                var jsonc = GetContacts(apikey);

                string retc = JsonConvert.SerializeObject(jsonc);

                System.IO.File.WriteAllText(@"c:\Contatos.json", retc.ToString());

                Console.ReadKey();

            }
            Console.WriteLine("Apikey não informada");


        }

        public static Serializacao.PedidosBling.RootObject GetPedidos(string apikey)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add("apikey", apikey);

            string url;
            Serializacao.PedidosBling.RootObject peds = new Serializacao.PedidosBling.RootObject();
            Serializacao.PedidosBling.RootObject addPeds = new Serializacao.PedidosBling.RootObject();

            for (int x = 1; x > 0; x++)
            {
                {
                    //Capturando pedidos com status atendido na Bling
                    url = string.Format("https://bling.com.br/Api/v2/pedidos/page=" + x + "/json/");
                    addPeds = Comunicacao.RequestContent<Serializacao.PedidosBling.RootObject>(url, parametros);

                    if (addPeds.retorno.pedidos == null)
                    {
                        Console.WriteLine("Total de Pedidos encontrados: " + peds.retorno.pedidos.Count);
                        break;
                    }

                    if (peds.retorno == null)
                    {
                        peds = Comunicacao.RequestContent<Serializacao.PedidosBling.RootObject>(url, parametros);
                        Console.WriteLine("Pagina: " + x + ", pegou " + addPeds.retorno.pedidos.Count + " pedidos." /*+ " URL: " + url*/);
                        addPeds.retorno.pedidos = null;
                    }
                    else
                    {
                        peds.retorno.pedidos.AddRange(addPeds.retorno.pedidos);
                        Console.WriteLine("Pagina: " + x + ", pegou " + addPeds.retorno.pedidos.Count + " pedidos." /*+ " URL: " + url*/);
                        addPeds.retorno.pedidos = null;
                    }
                }
            }
            return peds;
        }

        public static RetornoProduto GetProducts(string apikey)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add("apikey", apikey);
            parametros.Add("imagem", "S");
            parametros.Add("estoque", "S");

            string url;
            Serializacao.RetornoProduto prods = new RetornoProduto();
            Serializacao.RetornoProduto addProds = new RetornoProduto();

            for (int x = 1; x > 0; x++)
            {
                {
                    url = string.Format("https://bling.com.br/Api/v2/produtos/page=" + x + "/json/");
                    addProds = Comunicacao.RequestContent<Serializacao.RetornoProduto>(url, parametros);

                    if (addProds.retorno.produtos == null)
                    {
                        Console.WriteLine("Total de Produtos encontrados: " + prods.retorno.produtos.Count);
                        break;
                    }

                    if (prods.retorno == null)
                    {
                        prods = Comunicacao.RequestContent<Serializacao.RetornoProduto>(url, parametros);
                        Console.WriteLine("Pagina: " + x + ", pegou " + addProds.retorno.produtos.Count + " produtos." /*+ " URL: " + url*/);
                        addProds.retorno.produtos = null;
                    }
                    else
                    {
                        prods.retorno.produtos.AddRange(addProds.retorno.produtos);
                        Console.WriteLine("Pagina: " + x + ", pegou " + addProds.retorno.produtos.Count + " produtos." /*+ " URL: " + url*/);
                        addProds.retorno.produtos = null;
                    }
                }
            }
            return prods;
        }

        public static ContatosBling.RootObject GetContacts(string apikey)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add("apikey", apikey);


            string url;
            Serializacao.ContatosBling.RootObject contacts = new ContatosBling.RootObject();
            Serializacao.ContatosBling.RootObject addcontacts = new ContatosBling.RootObject();

            for (int x = 1; x > 0; x++)
            {
                {
                    url = string.Format("https://bling.com.br/Api/v2/contatos/json/page=" + x + "/json/");
                    addcontacts = Comunicacao.RequestContent<Serializacao.ContatosBling.RootObject>(url, parametros);
                 

                    if (addcontacts.retorno.contatos == null)
                    {
                        Console.WriteLine("Total de Contatos encontrados: " + contacts.retorno.contatos.Count);
                        break;
                    }

                    if (contacts.retorno == null)
                    {
                        contacts = Comunicacao.RequestContent<Serializacao.ContatosBling.RootObject>(url, parametros);
                        Console.WriteLine("Pagina: " + x + ", pegou " + addcontacts.retorno.contatos.Count + " produtos." /*+ " URL: " + url*/);
                        addcontacts.retorno.contatos = null;
                    }
                    else
                    {
                        contacts.retorno.contatos.AddRange(addcontacts.retorno.contatos);
                        Console.WriteLine("Pagina: " + x + ", pegou " + contacts.retorno.contatos.Count + " produtos." /*+ " URL: " + url*/);
                        addcontacts.retorno.contatos = null;
                    }
                }
            }
            return contacts;
        }
    }
}
