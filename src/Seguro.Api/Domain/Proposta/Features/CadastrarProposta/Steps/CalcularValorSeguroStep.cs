using Seguro.Api.Domain.Proposta.Infraestrutura;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps
{
    public class CalcularValorSeguroStep(PropostaRepository propostaRepository) : StepBodyAsync
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var pontuacaoPorCoberturas = propostaRepository.BuscarCustoPorCoberturas(Data.Coberturas);
            var ajusteNivelRisco = propostaRepository.BuscarAjustePorNivelRisco(Data.NivelRisco);

            await Task.CompletedTask;
            return ExecutionResult.Next();
        }
    }
}