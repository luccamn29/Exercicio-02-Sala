using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace ExercicioEmSala2.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            string stringConexao = File.ReadAllText(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Acesso SQL Exercicio2.txt");
            _conexao = new SqlConnection(stringConexao);
        }

        public void InserirCliente(Entidade.Cliente cliente)
        {
            try
            {
                _conexao.Open();
                string query = @"INSERT INTO Aluno
                                       (Identificador
                                       ,Nome
                                       ,Email
                                       ,Telefone
                                       ,Endereco
                                       ,DataCadastro)
                                 VALUES
                                       (@Identificador
                                       ,@Nome
                                       ,@Email
                                       ,@Telefone
                                       ,@Endereco
                                       ,@DataCadastro);";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", cliente.Id);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void AtualizarCliente(Entidade.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Aluno
                                SET Nome = @Nome
                                ,Email = @Email
                                ,Telefone = @Telefone
                                ,Endereco = @Endereco
                                ,DataCadastro= @DataCadastro
                                WHERE Identificador = @Identificador";
                                
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", cliente.Id);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                    cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
       
        }
        public bool VerificarExistenciaCliente(string id)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Aluno WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", id);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void DeletarBaseCliente()
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Aluno
                                 WHERE 1 = 1";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public List<Entidade.Cliente> RecuperarClientes()
        {
            var clientes = new List<Entidade.Cliente>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Aluno";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var cliente = new Entidade.Cliente();
                        cliente.Id = rdr["Identificador"].ToString();
                        cliente.Nome = rdr["Nome"].ToString();
                        cliente.Email = rdr["Email"].ToString();
                        cliente.Telefone = Convert.ToInt64(rdr["Telefone"]);
                        cliente.Endereco = rdr["Endereco"].ToString();
                        cliente.DataCadastro = DateTime.ParseExact(rdr["DataCadastro"].ToString(), "dd/MM/yyyy HH:mm:ss", null);

                        clientes.Add(cliente);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return clientes;
        }
        public void InserirFaturamento(Entidade.Faturamento faturamento)
        {
            try
            {
                _conexao.Open();
                string query = @"INSERT INTO Faturamento
                                       (Identificador
                                       ,TotalEntrada
                                       ,TotalSaida
                                       ,DiaReferencia)
                                 VALUES
                                       (@Identificador
                                       ,@TotalEntrada
                                       ,@TotalSaida
                                       ,@DiaReferencia);";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", faturamento.Id);
                    cmd.Parameters.AddWithValue("@TotalEntrada", faturamento.Valor);
                    cmd.Parameters.AddWithValue("@TotalSaida", faturamento.Despesa);
                    cmd.Parameters.AddWithValue("@DiaReferencia", faturamento.Data);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void AtualizarFaturamento(Entidade.Faturamento faturamento)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Faturamento
                                SET TotalEntrada = @TotalEntrada
                                ,TotalSaida = @TotalSaida
                                ,DiaReferencia = @DiaReferencia
                                WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", faturamento.Id);
                    cmd.Parameters.AddWithValue("@TotalEntrada", faturamento.Valor);
                    cmd.Parameters.AddWithValue("@TotalSaida", faturamento.Despesa);
                    cmd.Parameters.AddWithValue("@DiaReferencia", faturamento.Data);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }
        public bool VerificarExistenciaFaturamento(string id)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Faturamento WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", id);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void DeletarBaseFaturamento()
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Faturamento
                                 WHERE 1 = 1";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public List<Entidade.Faturamento> RecuperarFaturamento()
        {
            var faturamentos = new List<Entidade.Faturamento>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Faturamento";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var faturamento = new Entidade.Faturamento();
                        faturamento.Id = rdr["Identificador"].ToString();
                        faturamento.Valor = Convert.ToDecimal(rdr["TotalEntrada"]);
                        faturamento.Despesa = Convert.ToDecimal(rdr["TotalSaida"]);
                        faturamento.Data = DateTime.ParseExact(rdr["DiaReferencia"].ToString(), "dd/MM/yyyy hh:mm:ss", null);


                        faturamentos.Add(faturamento);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return faturamentos;
        }
        public void InserirPropaganda(Entidade.Propaganda propaganda)
        {
            try
            {
                _conexao.Open();
                string query = @"INSERT INTO Propaganda
                                       (Identificador
                                       ,EmpresaDivulgadora
                                       ,Custo
                                       ,DataPropaganda)
                                 VALUES
                                       (@Identificador
                                       ,@EmpresaDivulgadora
                                       ,@Custo
                                       ,@DataPropaganda);";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", propaganda.Id);
                    cmd.Parameters.AddWithValue("@EmpresaDivulgadora", propaganda.Empresa);
                    cmd.Parameters.AddWithValue("@Custo", propaganda.Custo);
                    cmd.Parameters.AddWithValue("@DataPropaganda", propaganda.Data);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void AtualizarPropaganda(Entidade.Propaganda propaganda)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Propaganda
                                SET EmpresaDivulgadora = @EmpresaDivulgadora
                                ,Custo = @Custo
                                ,DataPropaganda = @DataPropaganda
                                WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", propaganda.Id);
                    cmd.Parameters.AddWithValue("@EmpresaDivulgadora", propaganda.Empresa);
                    cmd.Parameters.AddWithValue("@Custo", propaganda.Custo);
                    cmd.Parameters.AddWithValue("@DataPropaganda", propaganda.Data);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }
        public bool VerificarExistenciaPropaganda(string id)
        {
            try
            {
                _conexao.Open();

                string query = @"select Count(Identificador) AS total 
                                 from Propaganda WHERE Identificador = @Identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Identificador", id);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void DeletarBasePropaganda()
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Propaganda
                                 WHERE 1 = 1";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public List<Entidade.Propaganda> RecuperarPropaganda()
        {
            var propagandas = new List<Entidade.Propaganda>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Propaganda";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var propaganda = new Entidade.Propaganda();
                        propaganda.Id = rdr["Identificador"].ToString();
                        propaganda.Empresa = rdr["EmpresaDivulgadora"].ToString();
                        propaganda.Custo = Convert.ToDecimal(rdr["Custo"]);
                        propaganda.Data = DateTime.ParseExact(rdr["DataPropaganda"].ToString(), "dd/MM/yyyy HH:mm:ss", null);


                        propagandas.Add(propaganda);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }

            return propagandas;
        }
    }
}
    