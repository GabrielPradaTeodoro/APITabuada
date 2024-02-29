using Business.Models.Response.Multiplicacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITabuada.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MultiplicacaoController : ControllerBase
    {
        private readonly Business.Multiplicacao.Multiplicacao _multiplicacao;
        public MultiplicacaoController(Business.Multiplicacao.Multiplicacao multiplicacao)
        {
            _multiplicacao = multiplicacao;
        }

        [HttpPost("multiplicar")]
        public async Task<IActionResult> Multiplicar(List<int> lista)
        {
            if (!lista.Any())
            {
                var falha = new ListaMultiplicacaoResponse();
                falha.Sucesso = false;
                falha.Mensagem = "A lista está vazia!";
                return Ok(falha);
            }

            //fazer a gravação e retornar 
            var response = await _multiplicacao.MultiplicarLista(lista);
            return Ok(response);
        }
    }
}
