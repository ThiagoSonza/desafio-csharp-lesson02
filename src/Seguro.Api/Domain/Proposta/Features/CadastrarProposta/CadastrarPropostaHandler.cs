using CSharpFunctionalExtensions;
using MediatR;
using WorkflowCore.Interface;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public class CadastrarPropostaHandler() : IRequestHandler<CadastrarPropostaCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CadastrarPropostaCommand command, CancellationToken cancellationToken)
        {
            // await workflowHost.StartWorkflow(
            //     nameof(CadastrarPropostaWorkflow),
            //     new PropostaWorkflowData() { Data = command }
            // );

            return Result.Success("Proposta de seguro criada com sucesso");
        }
    }
}