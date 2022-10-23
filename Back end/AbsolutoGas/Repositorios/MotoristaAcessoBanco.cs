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
    public class MotoristaAcessoBanco
    {
        //private readonly string _connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=AbsolutoGas;Integrated Security=True;";//CASA
        //private readonly string _connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=AbsolutoGas;Integrated Security=True;";//SENAC
        private readonly string _connection = @"Data source=PATRICK\SQLEXPRESS;Initial catalog=absolutoGas;Integrated Security=true;";//casa patrick

        public Motorista SalvarMotorista2(Motorista motorista)
        {
            int idMotorista = -1;
            try
            {
                var query1 = @"INSERT INTO Motorista (Nome, CNH, Telefone) OUTPUT Inserted.IdMotorista
                              VALUES (@nome,@CNH,@telefone)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query1, sql);
                
                    command.Parameters.AddWithValue("@nome", motorista.Nome);
                    command.Parameters.AddWithValue("@CNH", motorista.CNH);
                    command.Parameters.AddWithValue("@telefone", motorista.Telefone);
                    command.Connection.Open();
                    motorista.IdMotorista = (int)command.ExecuteScalar();
                }

                Console.WriteLine("Motorista e Veiculo cadastrado com sucesso.");
                return motorista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }

        public bool SalvarMotorista(Motorista motorista)
        {

            try
            {
                var query = @"INSERT INTO Motorista (Nome, CNH, Telefone)
                              VALUES (@nome,@CNH,@telefone)";
               
                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    

                    command.Parameters.AddWithValue("@nome", motorista.Nome);
                    command.Parameters.AddWithValue("@CNH", motorista.CNH);
                    command.Parameters.AddWithValue("@telefone", motorista.Telefone);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                   
                }

                Console.WriteLine("Motorista cadastrado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }

        }

        public List<MotoristaDto> BuscarTodos()
        {
            List<MotoristaDto> motoristaEncontrados;
            try
            {
                var query = @"SELECT IdMotorista, Nome, CNH, Telefone  FROM Motorista";

                using (var connection = new SqlConnection(_connection))
                {

                    motoristaEncontrados = connection.Query<MotoristaDto>(query).ToList();
                }

                return motoristaEncontrados;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                return null;
            }
        }

        public MotoristaDto BuscarPorId(int Id)
        {
            MotoristaDto motoristaEncontrados;
            try
            {
                var query = @"SELECT IdMotorista, Nome, CNH, Telefone FROM Motorista
                                      WHERE IdMotorista = @Id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        Id
                    };
                    motoristaEncontrados = connection.QueryFirstOrDefault<MotoristaDto>(query, parametros);
                }

                return motoristaEncontrados;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }
        public MotoristaDto Atualizar(MotoristaDto motorista)
        {
            try
            {
                var query = @"UPDATE Motorista SET Nome = @nome, CNH = @CNH, Telefone = @telefone 
                                WHERE IdMotorista = @idMotorista";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", motorista.Nome);
                    command.Parameters.AddWithValue("@CNH", motorista.CNH);
                    command.Parameters.AddWithValue("@telefone", motorista.Telefone);
                    command.Parameters.AddWithValue("@idMotorista", motorista.IdMotorista);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                return motorista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }

        public bool Atualizar(int IdMotorista, Motorista motorista)
        {
            try
            {
                var query = @"UPDATE Motorista SET Nome = @nome, CNH = @CNH, Telefone = @telefone 
                                WHERE IdMotorista = @idMotorista";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", motorista.Nome);
                    command.Parameters.AddWithValue("@CNH", motorista.CNH);
                    command.Parameters.AddWithValue("@telefone", motorista.Telefone);
                    command.Parameters.AddWithValue("@idMotorista", IdMotorista);
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
        public void Remover(MotoristaDto motorista)
        {
            try
            {
                var query = @"DELETE FROM Motorista WHERE IdMotorista = @idMotorista";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);

                    command.Parameters.AddWithValue("@idMotorista", motorista.IdMotorista);
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
