using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Infraestrutura;
using Seguro.Api.Domain.Proposta.Model;
using WorkflowCore.Interface;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public class CadastrarPropostaHandler(
        PropostaRepository propostaRepository,
        IWorkflowHost workflowHost
    ) : IRequestHandler<CadastrarPropostaCommand, Result<PropostaDominio>>
    {
        public async Task<Result<PropostaDominio>> Handle(CadastrarPropostaCommand command, CancellationToken cancellationToken)
        {
            await workflowHost.StartWorkflow(
                nameof(CadastrarPropostaWorkflow),
                new PropostaWorkflowData() { Data = command }
            );

            var propostaResult = PropostaDominio.Criar(
                command.Veiculo,
                command.Proprietario,
                command.Condutor,
                command.Coberturas
            );

            if (propostaResult.IsFailure)
                return Result.Failure<PropostaDominio>(propostaResult.Error);

            await propostaRepository.Adicionar(propostaResult.Value);
            await propostaRepository.Salvar();

            return Result.Success(propostaResult.Value);
        }
    }
}