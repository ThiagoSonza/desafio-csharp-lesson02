using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seguro.Api.Controllers.Shared;
using Seguro.Api.Domain.Proposta.Features.AprovarProposta;
using Seguro.Api.Domain.Proposta.Features.CadastrarProposta;
using Seguro.Api.Domain.Proposta.Features.RejeitarProposta;

namespace Seguro.Api.Controllers
{
    public class SeguroController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> CadastrarProposta([FromBody] CadastrarPropostaRequest request, CancellationToken cancelationToken)
        {
            var command = request.CriarComando();
            if (command.IsFailure)
                return BadRequest(command.Error);

            var result = await mediator.Send(command.Value, cancelationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{proposta}/aprovar")]
        public async Task<IActionResult> AprovarPropostaSeguro(int proposta, CancellationToken cancelationToken)
        {
            var command = AprovarPropostaCommand.Criar(proposta);
            if (command.IsFailure)
                return BadRequest(command.Error);

            var result = await mediator.Send(command.Value, cancelationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{proposta}/rejeitar")]
        public async Task<IActionResult> RejeitarPropostaSeguro(int proposta, CancellationToken cancelationToken)
        {
            var command = RejeitarPropostaCommand.Criar(proposta);
            if (command.IsFailure)
                return BadRequest(command.Error);

            var result = await mediator.Send(command.Value, cancelationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}