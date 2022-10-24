using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsolutoGas.Models
{
    public class Pedido
    {
        public int Id { get; set; }    
        public DateTime DataHoraEntrega { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int IdPagamento { get; set; }
        public int IdMotorista { get; set; }
        public double ValorTotal { get; set; }
        public string Situacao { get; set; }
    }
}
