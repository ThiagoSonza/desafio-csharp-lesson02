using Seguro.Api.Domain.Proposta.Infraestrutura;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps
{
    public class CalcularNivelRiscoStep(PropostaRepository propostaRepository) : StepBodyAsync
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var pontuacaoPorIdade = propostaRepository.BuscarPontuacaoPorIdadeCondutor(Data.Condutor.DataNascimento);
            var pontuacaoPorAcidentes = propostaRepository.BuscarPontuacaoPorAcidentes(Data.HistoricoAcidentes);
            var pontuacaoPorLocalidade = propostaRepository.BuscarPontuacaoPorLocalidadeCondutor(Data.Condutor.Residencia.Estado);
            int pontuacao = pontuacaoPorIdade + pontuacaoPorAcidentes + pontuacaoPorLocalidade;

            var nivelRisco = propostaRepository.BuscarClassificacaoPorPontuacao(pontuacao);
            Data.AtualizaNivelRisco(nivelRisco);

            await Task.CompletedTask;
            return ExecutionResult.Next();
        }
    }
}