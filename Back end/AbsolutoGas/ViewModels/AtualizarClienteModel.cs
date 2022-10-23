using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbsolutoGas.Dtos;
using AbsolutoGas.Models;

namespace AbsolutoGas.ViewModels
{
    public class AtualizarClienteModel2
    {
        public ClienteDto Atualizar { get; set; }
    }
    
    public class AtualizarClienteModel
    {
        public int IdEncontrar { get; set; }
        public Cliente Atualizar { get; set; }
    }
}
