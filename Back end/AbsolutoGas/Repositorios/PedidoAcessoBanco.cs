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
    public class PedidoAcessoBanco
    {
        //private readonly string _connection = @"Data Source=DESKTOP-IR1AB95;Initial Catalog=AbsolutoGas;Integrated Security=True;";//CASA
        //private readonly string _connection = @"Data Source=ITELABD04\SQLEXPRESS;Initial Catalog=AbsolutoGas;Integrated Security=True;";//SENAC
        private readonly string _connection = @"Data source=PATRICK\SQLEXPRESS;Initial catalog=absolutoGas;Integrated Security=true;";//casa patrick

        public Pedido SalvarPedido(Pedido pedido)
        {
            try
            {
                var query = @"INSERT INTO Pedido (IdCliente, IdProduto, IdPagamento,  IdMotorista, ValorTotal, Situacao, DataHoraEntrega) OUTPUT Inserted.IdPedido
                              VALUES (@idCliente, @idProduto, @idPagamento, @idMotorista, @valorTotal, @situacao, @dataHoraEntrega)";


                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@dataHoraEntrega", pedido.DataHoraEntrega);
                    command.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                    command.Parameters.AddWithValue("@idProduto", pedido.IdProduto);
                    command.Parameters.AddWithValue("@idPagamento", pedido.IdPagamento);
                    command.Parameters.AddWithValue("@idMotorista", pedido.IdMotorista);
                    command.Parameters.AddWithValue("@valorTotal", pedido.ValorTotal);
                    command.Parameters.AddWithValue("@situacao", pedido.Situacao);
                    command.Connection.Open();
                    pedido.Id = (int)command.ExecuteScalar();
                }

                Console.WriteLine("Pedido cadastrado com sucesso.");
                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }

        public List<PedidoListagemDto> BuscarTodos()
        {
            List<PedidoListagemDto> pedidoEncontrados;
            try
            {
                var query = @"select p.IdPedido, c.Nome as NomeCliente, pr.Descricao as DescricaoProduto, p.ValorTotal as ValorTotalPedido, 
                                  FORMAT(p.DataHoraEntrega, 'dd/MM/yyyy HH:mm:ss') as DataHoraEntrega, t.Descricao as TipoPagamento, m.Nome as NomeMotorista, v.Placa as Placa,
								  CASE 
									WHEN  p.Situacao = '1' THEN 'Buscando Motorista'
									WHEN  p.Situacao = '2' THEN 'Motorista a caminho'
									ELSE  'Pedido entregue com êxito'
								  END AS Situacao	
                                  from Pedido p
                                  inner join Cliente c on p.IdCliente = c.IdCliente
                                  inner join Produto pr on p.IdProduto =pr.IdProduto
                                  inner join Motorista m on p.IdMotorista = m.IdMotorista
                                  inner join TipoPagamento t on p.IdPagamento = t.IdPagamento
                                  inner join Veiculo v on m.IdMotorista = v.IdMotorista";

                using (var connection = new SqlConnection(_connection))
                {
                    pedidoEncontrados = connection.Query<PedidoListagemDto>(query).ToList();
                }
                return pedidoEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }

        public PedidoDto BuscarPorId(int idPedido)
        {
            PedidoDto pedidoEncontrados;
            try
            {
                var query = @"SELECT IdPedido, IdCliente, IdProduto, IdMotorista, IdPagamento, ValorTotal, Situacao, DataHoraEntrega FROM Pedido
                                      WHERE IdPedido = @idPedido";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idPedido
                    };
                    pedidoEncontrados = connection.QueryFirstOrDefault<PedidoDto>(query, parametros);
                }

                return pedidoEncontrados;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }

        public Pedido Atualizar(Pedido pedido)
        {
            try
            {
                var query = @"UPDATE Pedido SET IdCliente = @idCliente, IdProduto = @idProduto, IdPagamento = @idPagamento, DataHoraEntrega = @dataHoraEntrega, ValorTotal = @valorTotal, Situacao = @Situacao 
                                WHERE IdPedido = @idPedido ";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@dataHoraEntrega", pedido.DataHoraEntrega);
                    command.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                    command.Parameters.AddWithValue("@idProduto", pedido.IdProduto);
                    command.Parameters.AddWithValue("@idPagamento", pedido.IdPagamento);
                    command.Parameters.AddWithValue("@valorTotal", pedido.ValorTotal);
                    command.Parameters.AddWithValue("@situacao", pedido.Situacao);
                    command.Parameters.AddWithValue("@idPedido", pedido.Id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Pedido atualizado com sucesso.");
                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Remover(PedidoListagemDto id)
        {
            try
            {
                var query = @"DELETE FROM Pedido WHERE IdPedido = @id";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);

                    command.Parameters.AddWithValue("@id", id.IdPedido);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

        }
        public void Remover(PedidoDto id)
        {
            try
            {
                var query = @"DELETE FROM Pedido WHERE IdPedido = @id";

                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);

                    command.Parameters.AddWithValue("@id", id.IdPedido);
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
