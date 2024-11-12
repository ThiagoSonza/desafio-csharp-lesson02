using Seguro.Api.Domain.Proposta.Infraestrutura;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps
{
    public class ConsultarHistoricoAcidentesStep(PropostaRepository propostaRepository) : StepBodyAsync
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var historicoAcidentes = await propostaRepository.BuscarAcidentesCondutor(Data.Condutor.Cpf);

            Data.AtualizaHistoricoAcidentes(historicoAcidentes);

            return ExecutionResult.Next();
        }
    }
}