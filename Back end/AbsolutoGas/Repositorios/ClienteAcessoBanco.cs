using AbsolutoGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using AbsolutoGas.Dtos;
using System.Threading.Tasks;
using Dapper;

namespace AbsolutoGas.Repositorios
{
    public class ClienteAcessoBanco
    {
        //private readonly string _connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=AbsolutoGas;Integrated Security=True;";//CASA
        //private readonly string _connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=AbsolutoGas;Integrated Security=True;";//SENAC
        private readonly string _connection = @"Data source=PATRICK\SQLEXPRESS;Initial catalog=absolutoGas;Integrated Security=true;";//casa patrick


        public bool SalvarCliente(Cliente cliente)
        {

            try
            {
                var query = @"INSERT INTO Cliente 
                              (Nome, CPF, DataNascimento, Telefone, Rua, Numero, Bairro, Cidade, Referencia, TipoContato)
                              VALUES (@nome,@CPF,@dataNascimento,@telefone,@rua,@numero,@bairro,@cidade,@referencia,@tipoContato)";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@rua", cliente.Rua);
                    command.Parameters.AddWithValue("@numero", cliente.Numero);
                    command.Parameters.AddWithValue("@bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@cidade", cliente.Cidade);
                    command.Parameters.AddWithValue("@referencia", cliente.Referencia);
                    command.Parameters.AddWithValue("@tipoContato", cliente.TipoContato);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Cliente cadastrado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }

        }

        public List<ClienteDto> BuscarTodos()
        {
            List<ClienteDto> clientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, Nome, CPF, DataNascimento, Telefone, Rua, Numero, Bairro, Cidade, Referencia, TipoContato  FROM Cliente";

                using (var connection = new SqlConnection(_connection))
                {

                    clientesEncontrados = connection.Query<ClienteDto>(query).ToList();
                }

                return clientesEncontrados;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                return null;
            }
        }
        public ClienteDto BuscarPorId(int Id)
        {
            ClienteDto clienteEncontrados;
            try
            {
                var query = @"SELECT IdCliente, Nome, CPF, FORMAT(DataNascimento, 'dd/MM/yyyy') as DataNascimento, Telefone, Rua, Numero, Bairro, Cidade, Referencia, TipoContato FROM Cliente
                                      WHERE IdCliente = @Id";
                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        Id
                    };
                    clienteEncontrados = connection.QueryFirstOrDefault<ClienteDto>(query, parametros);
                }

                return clienteEncontrados;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }
    
        

        public ClienteDto BuscarPorNome(string nome)
        {
            ClienteDto clientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, Nome, CPF, DataNascimento, Telefone, Rua, Numero, Bairro, Cidade, Referencia, TipoContato FROM Cliente
                                      WHERE Nome like CONCAT('%',@nome,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
                    };
                    clientesEncontrados = connection.QueryFirstOrDefault<ClienteDto>(query, parametros);
                }

                return clientesEncontrados;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }
        public ClienteDto Atualizar2(ClienteDto cliente)
        {
            try
            {
                var query = @"UPDATE Cliente SET Nome = @nome, CPF = @CPF, DataNascimento = @dataNascimento, Telefone = @telefone, Rua = @rua, Numero = @numero, Bairro = @bairro, Cidade = @cidade, Referencia = @referencia, TipoContato = @tipoContato
                                WHERE IdCliente = @idCliente";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@rua", cliente.Rua);
                    command.Parameters.AddWithValue("@numero", cliente.Numero);
                    command.Parameters.AddWithValue("@bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@cidade", cliente.Cidade);
                    command.Parameters.AddWithValue("@referencia", cliente.Referencia);
                    command.Parameters.AddWithValue("@tipoContato", cliente.TipoContato);
                    command.Parameters.AddWithValue("@idCliente", cliente.IdCliente);

                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                return cliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        //public Cliente Atualizar2(int idCliente, Cliente cliente)
        //{
        //    try
        //    {
        //        var query = @"UPDATE Cliente SET Nome = @nome, CPF = @CPF, DataNascimento = @dataNascimento, Telefone = @telefone, Rua = @rua, Numero = @numero, Bairro = @bairro, Cidade = @cidade, Referencia = @referencia, TipoContato = @tipoContato
        //                        WHERE IdCliente = @idCliente";

        //        using (var sql = new SqlConnection(_connection))

        //        {
        //            SqlCommand command = new SqlCommand(query, sql);
        //            command.Parameters.AddWithValue("@nome", cliente.Nome);
        //            command.Parameters.AddWithValue("@CPF", cliente.CPF);
        //            command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
        //            command.Parameters.AddWithValue("@telefone", cliente.Telefone);
        //            command.Parameters.AddWithValue("@rua", cliente.Rua);
        //            command.Parameters.AddWithValue("@numero", cliente.Numero);
        //            command.Parameters.AddWithValue("@bairro", cliente.Bairro);
        //            command.Parameters.AddWithValue("@cidade", cliente.Cidade);
        //            command.Parameters.AddWithValue("@referencia", cliente.Referencia);
        //            command.Parameters.AddWithValue("@tipoContato", cliente.TipoContato);
        //            command.Parameters.AddWithValue("@idCliente", idCliente);

        //            command.Connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        cliente.IdCliente = idCliente;
        //        return cliente;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Erro: " + ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}

        public bool Remover2(int idCliente)
        {
            try
            {
                var query = @"DELETE FROM Cliente WHERE IdCliente = @idCliente";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Cliente Removido!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não é possível deletar um cliente se tiver dentro de um PEDIDO");
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool Atualizar(int idCliente, Cliente cliente)
        {
            try
            {
                var query = @"UPDATE Cliente SET Nome = @nome, CPF = @CPF, DataNascimento = @dataNascimento, Telefone = @telefone, Rua = @rua, Numero = @numero, Bairro = @bairro, Cidade = @cidade, Referencia = @referencia, TipoContato = @tipoContato
                                WHERE IdCliente = @idCliente";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", cliente.Nome);
                    command.Parameters.AddWithValue("@CPF", cliente.CPF);
                    command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                    command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    command.Parameters.AddWithValue("@rua", cliente.Rua);
                    command.Parameters.AddWithValue("@numero", cliente.Numero);
                    command.Parameters.AddWithValue("@bairro", cliente.Bairro);
                    command.Parameters.AddWithValue("@cidade", cliente.Cidade);
                    command.Parameters.AddWithValue("@referencia", cliente.Referencia);
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public void Remover(ClienteDto cliente)
        {
            try
            {
                var query = @"DELETE FROM Cliente WHERE IdCliente = @id";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);

                    command.Parameters.AddWithValue("@id", cliente.IdCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

        }
    }
}
