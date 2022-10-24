using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsolutoGas.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TipoPagamento : ControllerBase
    {
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var tipoPagamento = repositorioTipoPagamento.BuscarTodos();

            if (tipoPagamento == null || !tipoPagamento.Qualquer())
                return NotFound(new { mensage = $" Lista vazia. " });

            retornar Ok( );

        }
    }
}
