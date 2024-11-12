using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Infraestrutura;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Domain.Proposta.Features.AprovarProposta
{
    public class AprovarPropostaHandler(PropostaRepository propostaRepository) : IRequestHandler<AprovarPropostaCommand, Result<PropostaDominio>>
    {
        public async Task<Result<PropostaDominio>> Handle(AprovarPropostaCommand command, CancellationToken cancellationToken)
        {
            var proposta = await propostaRepository.BuscarProposta(command.Id);
            if (proposta.HasNoValue)
                return Result.Failure<PropostaDominio>($"Proposta inválida");

            proposta.Value.Aprovar();
            await propostaRepository.Salvar();

            return Result.Success(proposta.Value);
        }
    }
}