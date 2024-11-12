using FluentValidation;

namespace Seguro.Api.Domain.Proposta.Features.RejeitarProposta
{
    public class RejeitarPropostaValidator : AbstractValidator<RejeitarPropostaCommand>
    {
        public RejeitarPropostaValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("O id da proposta não pode ser nulo");
        }
    }
}