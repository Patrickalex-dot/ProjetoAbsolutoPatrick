using AbsolutoGas.Repositorios;
using AbsolutoGas.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbsolutoGas.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoAcessoBanco repositorioProduto = new ProdutoAcessoBanco();

        [HttpPost]
        public IActionResult Salvar(SalvarProdutoViewModel salvarProduto)
        {
            var resultado = repositorioProduto.SalvarProduto(salvarProduto.Produto);

            if (resultado) return Ok("Produto Cadastrado com sucesso.");

            return Ok("Houve um problema ao salvar. Produto não cadastrado.");
        }

        [HttpPut]
        public IActionResult Atualizar(AtualizarProdutoViewModel atualizarProduto)
        {
            var res = repositorioProduto.Atualizar(atualizarProduto.Atualizar);

            if (res != null) return Ok(new JsonResult(new { sucesso = true, resultado = res, mensagem = "Produto atualizado com sucesso!" }));
            return BadRequest("Não foi possivel atualizar funcionario. ");
        }

        [HttpDelete]
        public IActionResult Remover(int id)
        {
            var res = repositorioProduto.Remover(id);

            if (res == false) return Ok(new JsonResult(new { sucesso = res, mensagem = "Produto removido com sucesso!" }));
            return BadRequest("Não foi possivel atualizar funcionario. ");
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var res = repositorioProduto.BuscarTodos();

            if (res != null) return Ok(new JsonResult(new { sucesso = true, resultado = res, mensagem = "Cliente atualizado com sucesso!" }));
            return BadRequest("Não foi possivel atualizar funcionario. ");
        }
        [HttpGet]
        public IActionResult BuscarPorId(int id)
        {
            var res = repositorioProduto.BuscarPorId(id);

            if (res != null) return Ok(new JsonResult(new { sucesso = true, resultado = res, mensagem = "Cliente atualizado com sucesso!" }));
            return BadRequest("Não foi possivel atualizar funcionario. ");
        }
    }
}
