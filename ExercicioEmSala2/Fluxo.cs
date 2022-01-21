using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExercicioEmSala2
{
    public class Fluxo
    {
        public void Start()
        {
            var sql = new Conexoes.SqlServer();
            int opcao=-1;
            while (opcao!=0)
            {
                Console.WriteLine("Digite a opção desejada:\n" +
                             "1 - Exportar Clientes do csv para Banco de Dados\n" +
                             "2 - Exportar Faturamento do csv para Banco de Dados\n" +
                             "3 - Exportar Propaganda do csv para Banco de Dados\n" +
                             "4 - Gerar Relatórios\n" +
                             "0 - Sair");
                opcao = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Aguarde...");
                switch (opcao)
                {
                    case 0:
                        Console.Clear();
                        break;
                    case 1:
                        
                        var listaClientes = ImportarClientes();
                        sql.DeletarBaseCliente();
                        foreach (var cliente in listaClientes)
                        {
                            if (sql.VerificarExistenciaCliente(cliente.Id))
                            {
                                sql.AtualizarCliente(cliente);
                            }
                            else
                            {
                                sql.InserirCliente(cliente);
                            }

                        }
                        Console.Clear();
                        break;

                    case 2:                        
                        var listaFaturamento = ImportarFaturamento();
                        sql.DeletarBaseFaturamento();
                        foreach (var faturamento in listaFaturamento)
                        {
                            if (sql.VerificarExistenciaFaturamento(faturamento.Id))
                            {
                                sql.AtualizarFaturamento(faturamento);
                            }
                            else
                            {
                                sql.InserirFaturamento(faturamento);
                            }

                        }
                        Console.Clear();
                        break;

                    case 3:                        
                        var listaPropagandas = ImportarPropaganda();
                        sql.DeletarBasePropaganda();
                        foreach (var propaganda in listaPropagandas)
                        {
                            if (sql.VerificarExistenciaPropaganda(propaganda.Id))
                            {
                                sql.AtualizarPropaganda(propaganda);
                            }
                            else
                            {
                                sql.InserirPropaganda(propaganda);
                            }

                        }
                        Console.Clear();
                        break;

                    case 4:
                        Console.Clear();
                        VerificarClientesCadastrados2019();
                        RelatorioLucro();
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
           
        }
        public List<Entidade.Cliente> ImportarClientes()
        {
            var listaClientes = new List<Entidade.Cliente>();     
            string[] linhas = File.ReadAllLines(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Exercicios\Exercicio 02 em sala\Alunos.csv");
            foreach (string linha in linhas.Skip(1))
            {
                string[] coluna = linha.Split(',');
                var cliente = new Entidade.Cliente();
                cliente.Id = coluna[0];
                cliente.Nome = coluna[1];
                cliente.Email = coluna[2];
                var pattern = new Regex("[() -]");
                cliente.Telefone = Convert.ToInt64(pattern.Replace(coluna[3], ""));

                // enderecos com virgula estao entre aspas, tratado com o if abaixo
                if (coluna[4].Substring(0, 1) == "\"")
                {
                    String[] aspas = linha.Split('"');
                    cliente.Endereco = aspas[1];
                    cliente.DataCadastro = DateTime.ParseExact(aspas[2].Substring(1, aspas[2].Length-1), "yyyy-MM-dd HH:mm:ss", null);

                }
                else
                {
                    cliente.Endereco = coluna[4];
                    cliente.DataCadastro = DateTime.ParseExact(coluna[5], "yyyy-MM-dd HH:mm:ss", null);
                }
                listaClientes.Add(cliente);
            }
            return listaClientes;
        }
        public List<Entidade.Propaganda> ImportarPropaganda()
        {
            var listaPropagandas = new List<Entidade.Propaganda>();
            string[] linhas = File.ReadAllLines(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Exercicios\Exercicio 02 em sala\Propagandas.csv");
            foreach (string linha in linhas.Skip(1))
            {
                var propaganda = new Entidade.Propaganda();
                string[] coluna = linha.Split(',');
                propaganda.Id = coluna[0];
                propaganda.Empresa = coluna[1];
                string[] aspas = linha.Split('\"');
                propaganda.Custo = Convert.ToDecimal(aspas[1]);
                propaganda.Data = DateTime.ParseExact(aspas[2].Substring(1, aspas[2].Length - 1), "yyyy-MM-dd HH:mm:ss", null);

                listaPropagandas.Add(propaganda);
            }
            return listaPropagandas;
        }
        public List<Entidade.Faturamento> ImportarFaturamento()
        {
            var listaFaturamentos = new List<Entidade.Faturamento>();
            string[] linhas = File.ReadAllLines(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Exercicios\Exercicio 02 em sala\Faturamentos.csv");
            foreach (string linha in linhas.Skip(1))
            {
                var faturamento = new Entidade.Faturamento();
                string[] coluna = linha.Split(',');
                faturamento.Id = coluna[0];
                faturamento.Data = DateTime.ParseExact(coluna[1], "yyyy-MM-dd", null);
                string[] aspas = linha.Split('\"');

                if (aspas[1].Substring(0, 1) == "-")
                {
                    faturamento.Valor = -Convert.ToDecimal(aspas[1].Substring(4, aspas[1].Length - 4).Replace(".",""));
                }
                else
                {
                    faturamento.Valor = Convert.ToDecimal(aspas[1].Substring(3,aspas[1].Length-3));
                    
                }
                faturamento.Despesa = Convert.ToDecimal(aspas[3].Substring(3,aspas[3].Length - 3)); 
               

                listaFaturamentos.Add(faturamento);
            }
            return listaFaturamentos;
        }
        public void VerificarClientesCadastrados2019()
        {
            var sql = new Conexoes.SqlServer();
            var listaClientes = new List<Entidade.Cliente>();
            listaClientes = sql.RecuperarClientes();
            using (StreamWriter sw = File.CreateText(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Exercicios\Exercicio 02 em sala\Clientes2019.txt"))
            {
                foreach (var cliente in listaClientes)
                {
                    if (cliente.DataCadastro.Year == 2019)
                    {
                        sw.WriteLine(cliente.Id);
                    }
                }
            }
            
        }
        public void RelatorioLucro()
        {
            var sql = new Conexoes.SqlServer();
            var listaFaturamento = new List<Entidade.Faturamento>();
            listaFaturamento = sql.RecuperarFaturamento();

            var listaPropaganda = new List<Entidade.Propaganda>();
            listaPropaganda = sql.RecuperarPropaganda();
            decimal totalFaturamento = 0, totalDespesa = 0, totalLucro = 0;
            using (StreamWriter sw = File.CreateText(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Exercicios\Exercicio 02 em sala\Relatorio Lucro.txt"))
            {
                foreach (var faturamento in listaFaturamento)
                {
                    totalFaturamento += faturamento.Valor;
                    totalDespesa += faturamento.Despesa;
                }
                foreach (var propaganda in listaPropaganda)
                {
                    totalDespesa += propaganda.Custo;
                }
                totalLucro = totalFaturamento - totalDespesa;
                sw.WriteLine("Total Faturamento: "+totalFaturamento+"\n" +
                             "Total Despesas: "+totalDespesa+"\n" +
                             "Lucro: "+totalLucro);
            }

        }
    }
}
