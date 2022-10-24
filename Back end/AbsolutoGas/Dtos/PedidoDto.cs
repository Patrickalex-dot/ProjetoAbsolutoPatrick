using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsolutoGas.Dtos
{
    public class PedidoListagemDto
    {
        public int IdPedido { get; set; }
        public string DataHoraEntrega { get; set; }
        public string NomeCliente { get; set; }
        public string DescricaoProduto { get; set; }
        public string TipoPagamento { get; set; }
        public string NomeMotorista { get; set; }
        public double ValorTotalPedido { get; set; }
        public string Situacao { get; set; }
        public string Placa { get; set; }
    }

    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public DateTime DataHoraEntrega { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int IdPagamento { get; set; }
        public int IdMotorista { get; set; }
        public double ValorTotal { get; set; }
        public string Situacao { get; set; }
    }
}
