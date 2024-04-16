using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Querues;
using System.Net;

namespace Questao5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private IMediator _mediatorService;

        public ContaController(IMediator mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpPost]
        [Route("create-moviment")]
        public async Task<IActionResult> Create([FromBody] MovimentacaoContaCommand command)
        {
            try
            {
                command.SetIdRequisicao();
                var result = await _mediatorService.Send(command);

                if (result.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("get-balance/{idConta}")]
        public async Task<IActionResult> GetAll(string idConta)
        {
            try
            {

                var result = await _mediatorService.Send(new SaldoContaQuery() { IdContaCorrente = idConta });
                if (result.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
