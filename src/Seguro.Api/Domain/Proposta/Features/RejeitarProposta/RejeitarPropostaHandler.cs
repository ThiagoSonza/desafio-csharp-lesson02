using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Infraestrutura;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Domain.Proposta.Features.RejeitarProposta
{
    public class RejeitarPropostaHandler(PropostaRepository propostaRepository) : IRequestHandler<RejeitarPropostaCommand, Result<PropostaDominio>>
    {
        public async Task<Result<PropostaDominio>> Handle(RejeitarPropostaCommand command, CancellationToken cancellationToken)
        {
            var proposta = await propostaRepository.BuscarProposta(command.Id);
            if (proposta.HasNoValue)
                return Result.Failure<PropostaDominio>($"Proposta inválida");

            proposta.Value.Rejeitar();
            await propostaRepository.Salvar();

            return Result.Success(proposta.Value);
        }
    }
}