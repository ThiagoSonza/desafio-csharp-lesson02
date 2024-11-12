using Seguro.Api.Domain.Proposta.Infraestrutura;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps
{
    public class ConsultarFipeStep(PropostaRepository propostaRepository) : StepBodyAsync
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var valoresFipe = await propostaRepository.BuscarDadosTabelaFipe(Data.Veiculo.Marca, Data.Veiculo.Modelo, Data.Veiculo.Ano);
            if (valoresFipe.HasNoValue)
                return ExecutionResult.Next();

            decimal valorVeiculo = Convert.ToDecimal(valoresFipe.Value.Valor.Replace("R$", ""));
            Data.AtualizaDadosVeiculo(
                Data.Veiculo.Marca,
                Data.Veiculo.Modelo,
                Data.Veiculo.Ano,
                valorVeiculo
            );

            return ExecutionResult.Next();
        }
    }
}