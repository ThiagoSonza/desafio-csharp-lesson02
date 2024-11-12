using Seguro.Api.Domain.Proposta.Infraestrutura;
using Seguro.Api.Domain.Proposta.Model;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps
{
    public class CriarPropostaStep(PropostaRepository propostaRepository) : StepBodyAsync
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var propostaResult = PropostaDominio.Criar(
                Data.Veiculo,
                Data.Proprietario,
                Data.Condutor,
                Data.Coberturas
            );

            if (propostaResult.IsFailure)
                return ExecutionResult.Next();

            await propostaRepository.Adicionar(propostaResult.Value);
            await propostaRepository.Salvar();

            return ExecutionResult.Next();
        }
    }
}