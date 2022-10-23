using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AbsolutoGas.Models;
using AbsolutoGas.Repositorios;
using AbsolutoGas.ViewModels;
using static Slapper.AutoMapper;

namespace AbsolutoGas.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        MotoristaAcessoBanco repositorioMotorista = new MotoristaAcessoBanco();
        VeiculoAcessoBanco repositorioVeiculo = new VeiculoAcessoBanco();

        [HttpPost]  // CADASTRAR MOTORISTA e  VEICULO VIA REQUEST
        public IActionResult Salvar2(SalvarMotoristaModel salvarMotoristaViewModel)
        {
            if (salvarMotoristaViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarMotoristaViewModel.Motorista == null)
                return Ok("Dados do Motorista ou Veiculo não informados.");

            var resultadoMotorista = repositorioMotorista.SalvarMotorista2(salvarMotoristaViewModel.Motorista);

            bool resultadoVeiculo = false;
            if(resultadoMotorista.IdMotorista > 0)
            {
                resultadoVeiculo = repositorioVeiculo.SalvarVeiculo(salvarMotoristaViewModel.Veiculo,resultadoMotorista.IdMotorista);
            }

            if (resultadoVeiculo) return Ok(new JsonResult(new { sucesso = true, mensagem = "Motorista e Veiculo cadastrados com sucesso." }));

            return Ok(new JsonResult(new { sucesso = true,  mensagem = "Houve um problema ao cadastrar o veículo e/ou motorista." }));
        }

        [HttpPost]  // CADASTRAR MOTORISTA VIA CONSOLE
        public IActionResult Save(Motorista motorista)
        {
            if (motorista == null)
                return NoContent();

            repositorioMotorista.SalvarMotorista(motorista);

            return Ok("Adicionado com sucesso!");
        }

        [HttpGet]  // MOSTRAR LISTA DE MOTORISTA VIA CONSOLE
        public IActionResult BuscarTodos()
        {
            var motorista = repositorioMotorista.BuscarTodos();

            if (motorista == null || !motorista.Any())
                return NotFound(new JsonResult(new { sucesso = true, mensagem = "Não há nenhum motorista." }));
            else
            {
                foreach(var m in motorista)
                {
                    m.Veiculo = repositorioVeiculo.BuscarPorIdMotorista(m.IdMotorista);
                }
                return Ok(new JsonResult(new { sucesso = true, resultado = motorista}));
            }
        }

        [HttpPut]  // ATUALIZAR MOTORISTA POR ID VIA CONSOLE
        public IActionResult Atualizar(AtualizarMotoristaModel motorista)
        {
            var mEncontrado = repositorioMotorista.Atualizar(motorista.IdEncontrar, motorista.Atualizar);
            return Ok(mEncontrado);
        }

        [HttpPut]  // ATUALIZAR MOTORISTA POR ID VIA CONSOLE
        public IActionResult Atualizar2(AtualizarMotoristaViewModel motorista)
        {
            var mEncontrado = repositorioMotorista.Atualizar(motorista.Motorista);

            var res = repositorioVeiculo.Atualizar(motorista.Motorista.Veiculo);

            if(res != null)
            {
                mEncontrado.Veiculo = res;
                return Ok(new JsonResult(new { sucesso = true, resultado = res, mensagem = "Motorista e veículo atualizados." }));
            }
            else
            {
                return Ok(new JsonResult(new { sucesso = true, mensagem = "Houve um problema ao atualizar o veículo e o motorista." }));
            }
        }


        [HttpGet]
        public IActionResult BuscarPorId(int Id)
        {
            var res = repositorioMotorista.BuscarPorId(Id);

            if (res != null)
                res.Veiculo = repositorioVeiculo.BuscarPorIdMotorista(Id);
            else 
            {
                return Ok(new JsonResult(new { sucesso = true,  mensagem = "Não há nenhum motorista com o Id informado." }));
            }

            if (res != null) return Ok(new JsonResult(new { sucesso = true, resultado = res }));
            return BadRequest("Não foi possivel buscar o Motorista pelo id =  "+Id);
        }
        [HttpDelete]  // DELETAR CLIENTE POR NOME VIA CONSOLE
        public IActionResult Remover(int id)
        {

            var cEncontrado = repositorioMotorista.BuscarPorId(id);

            if (cEncontrado == null)
                return Ok(new JsonResult(new { sucesso = true, mensagem = "Não há nenhum motorista com o Id informado." }));

            var veiculoRemover = repositorioVeiculo.BuscarPorIdMotorista(id);

            if (veiculoRemover != null) 
            {
                repositorioVeiculo.Remover(veiculoRemover);
            }
            repositorioMotorista.Remover(cEncontrado);

            return Ok(new JsonResult(new { sucesso = true, mensagem = "Motorista removido com sucesso." }));
        }
    }
}
