using Seguro.Api.Domain.Proposta.Features.CadastrarProposta.Steps;
using WorkflowCore.Interface;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public record PropostaWorkflowData()
    {
        public CadastrarPropostaCommand Data { get; set; } = null!;
    };

    public class CadastrarPropostaWorkflow : IWorkflow<PropostaWorkflowData>
    {
        public string Id => nameof(CadastrarPropostaWorkflow);
        public int Version => 1;

        public void Build(IWorkflowBuilder<PropostaWorkflowData> builder)
        {
            builder
                .StartWith<ConsultarFipeStep>()
                    .Input(step => step.Data, data => data.Data)
                .Then<ConsultarHistoricoAcidentesStep>()
                    .Input(step => step.Data, data => data.Data)
                .Then<CalcularNivelRiscoStep>()
                    .Input(step => step.Data, data => data.Data)
                .Then<CalcularValorSeguroStep>()
                    .Input(step => step.Data, data => data.Data)
                .EndWorkflow();
        }
    }
}