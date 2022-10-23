using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AbsolutoGas.Models;
using AbsolutoGas.Repositorios;
using AbsolutoGas.ViewModels;
using System;

namespace AbsolutoGas.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        ClienteAcessoBanco repositorioCliente = new ClienteAcessoBanco();

        [HttpPost]  // CADASTRAR CLIENTE VIA REQUEST
        public IActionResult Salvar2([FromBody] SalvarClienteModel salvarClienteViewModel)
        {
            //if (salvarClienteViewModel == null)
            //    return Ok("Não foram informados dados");

            //if (salvarClienteViewModel.Cliente == null)
            //    return Ok("Dados do cliente não informados.");

            var resultado = repositorioCliente.SalvarCliente(salvarClienteViewModel.Cliente);

            if (resultado) return Ok(new JsonResult(new { sucesso = true, mensagem = "Cliente cadastrado com sucesso." }));

            return Ok("Houve um problema ao salvar. Cliente não cadastrada.");
        }

        //[HttpPut] // ATUALIZAR CLIENTE POR ID - VIA REQUEST
        //public IActionResult Atualizar2(AtualizarClienteModel atualizarcliente)
        //{

        //    var res = repositorioCliente.Atualizar2(atualizarcliente.IdEncontrar, atualizarcliente.Atualizar);

        //    if (res.IdCliente > 0) return Ok(res);
        //    return BadRequest("Não foi possivel atualizar funcionario. ");


        //}
        [HttpPut] // ATUALIZAR CLIENTE POR ID - VIA REQUEST
        public IActionResult Atualizar2(AtualizarClienteModel2 atualizarcliente)
        {
            var res = repositorioCliente.Atualizar2(atualizarcliente.Atualizar);

            if (res != null) return Ok(new JsonResult(new { sucesso = true, resultado = res, mensagem = "Cliente atualizado com sucesso!" }));
            return BadRequest("Não foi possivel atualizar funcionario. ");
        }

        [HttpDelete] // DELETAR CLIENTE POR NOME - VIA REQUEST
        
        public IActionResult DeletarCliente2(int idCliente)
        {
            var resultado = repositorioCliente.Remover2(idCliente);
            Exception exception = new Exception("Excluido com sucesso");
            if (resultado) return Ok(exception);
            exception = new Exception("Erro ao deletar");
            return Ok(exception);
        }

        [HttpGet] // BUSCAR CLIENTES - VIA REQUEST
        public IActionResult BuscarTodos2()
        {
            var resultado = repositorioCliente.BuscarTodos();

            if (resultado == null || !resultado.Any())
                return NotFound();

            return Ok(new JsonResult(new { sucesso = true, clientes = resultado }));
        }

        [HttpGet] // BUSCAR CLIENTES POR NOME - VIA REQUEST
        public IActionResult BuscarPorNome(string nome)
        {
            var resultado = repositorioCliente.BuscarPorNome(nome);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }
        [HttpGet]
        public IActionResult BuscarPorId(int Id)
        {
            var resultado = repositorioCliente.BuscarPorId(Id);
            if (resultado == null)
                return Ok(new JsonResult(new { sucesso = true, mensagem = "Não há elementos para exibir."}));
            return Ok(new JsonResult(new { sucesso = true, resultado }));
        }


        [HttpPost]  // CADASTRAR CLIENTE VIA CONSOLE
        public IActionResult Save(Cliente cliente)
        {
            if (cliente == null)
                return NoContent();

            repositorioCliente.SalvarCliente(cliente);

            return Ok("Adicionado com sucesso!");
        }


        [HttpGet]  // MOSTRAR LISTA DE CLIENTES VIA CONSOLE
        public IActionResult BuscarTodos()
        {
            var clientes = repositorioCliente.BuscarTodos();

            if (clientes == null || !clientes.Any())
                return NotFound(new { mensage = $"Lista vazia." });

            return Ok(clientes);

        }

        [HttpPut]  // ATUALIZAR CLIENTE POR ID VIA CONSOLE
        public IActionResult Atualizar(AtualizarClienteModel cliente)
        {
            var cEncontrado = repositorioCliente.Atualizar(cliente.IdEncontrar, cliente.Atualizar);
            return Ok(cEncontrado);
        }

        [HttpDelete]  // DELETAR CLIENTE POR NOME VIA CONSOLE
        public IActionResult Remover(int id)
        {
            var cEncontrado = repositorioCliente.BuscarPorId(id);

            if (cEncontrado == null)
                return NotFound("Não há nenhum registro com esse nome.");

            repositorioCliente.Remover(cEncontrado);

            return Ok();
        }
    }
}
