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
    public class VeiculoAcessoBanco
    {
        //private readonly string _connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=AbsolutoGas;Integrated Security=True;";//CASA
        //private readonly string _connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=AbsolutoGas;Integrated Security=True;";//SENAC
        private readonly string _connection = @"Data source=PATRICK\SQLEXPRESS;Initial catalog=absolutoGas;Integrated Security=true;";//casa patrick


        public bool SalvarVeiculo(Veiculo veiculo, int idMotorista)
        {
            try
            {
                var query = @"INSERT INTO Veiculo (IdMotorista, Placa)
                              VALUES (@IdMotorista,@placa)";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@placa", veiculo.Placa);
                    command.Parameters.AddWithValue("@idMotorista", idMotorista);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Veiculo cadastrado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }

        }
        public VeiculoDto BuscarPorIdMotorista(int Id)
        {
            VeiculoDto veiculoEncontrados;
            try
            {
                var query = @"SELECT IdVeiculo, Placa  FROM Veiculo WHERE IdMotorista = @Id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        Id
                    };
                    veiculoEncontrados = connection.QueryFirstOrDefault<VeiculoDto>(query,parametros);
                }

                return veiculoEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                return null;
            }
        }

        public List<VeiculoDto> BuscarTodos()
        {
            List<VeiculoDto> veiculoEncontrados;
            try
            {
                var query = @"SELECT IdVeiculo, Placa  FROM Veiculo";

                using (var connection = new SqlConnection(_connection))
                {

                    veiculoEncontrados = connection.Query<VeiculoDto>(query).ToList();
                }

                return veiculoEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                return null;
            }
        }

        public VeiculoDto BuscarPorModelo(string placa)
        {
            VeiculoDto veiculoEncontrados;
            try
            {
                var query = @"SELECT IdVeiculo, Placa FROM Veiculo
                                      WHERE Placa like CONCAT('%',@placa,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        placa
                    };
                    veiculoEncontrados = connection.QueryFirstOrDefault<VeiculoDto>(query, parametros);
                }

                return veiculoEncontrados;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }
        public VeiculoDto Atualizar(VeiculoDto veiculo)
        {
            try
            {
                var query = @"UPDATE Veiculo SET Placa = @placa
                                WHERE IdVeiculo = @idVeiculo";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@placa", veiculo.Placa);
                    command.Parameters.AddWithValue("@idVeiculo", veiculo.IdVeiculo);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                return veiculo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }

        public bool Atualizar(int IdVeiculo, Veiculo veiculo)
        {
            try
            {
                var query = @"UPDATE Veiculo SET Placa = @placa
                                WHERE IdVeiculo = @idVeiculo";

                using (var sql = new SqlConnection(_connection))

                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@placa", veiculo.Placa);
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
        public void Remover(VeiculoDto id)
        {
            try
            {
                var query = @"DELETE FROM Veiculo WHERE IdVeiculo = @id";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);

                    command.Parameters.AddWithValue("@id", id.IdVeiculo);
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
