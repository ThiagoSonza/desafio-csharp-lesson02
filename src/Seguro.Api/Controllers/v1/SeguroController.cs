using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Seguro.Api.Controllers.Shared;
using Seguro.Api.Domain.Proposta.Features.AprovarProposta;
using Seguro.Api.Domain.Proposta.Features.CadastrarProposta;
using Seguro.Api.Domain.Proposta.Features.RejeitarProposta;

namespace Seguro.Api.Controllers.v1
{
    [ApiVersion("1")]
    public class SeguroController(IMediator mediator) : BaseController
    {
        /// <summary>
        /// Gera uma proposta
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna OK</response>
        [HttpPost]
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

        /// <summary>
        /// Aprova a proposta
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna OK</response>
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

        /// <summary>
        /// Rejeita a proposta
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Retorna OK</response>
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