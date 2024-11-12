using FluentValidation;

namespace Seguro.Api.Domain.Proposta.Features.AprovarProposta
{
    public class AprovarPropostaValidator : AbstractValidator<AprovarPropostaCommand>
    {
        public AprovarPropostaValidator()
        {
            RuleFor(v => v.Id).NotNull().WithMessage("O id da proposta não pode ser nulo");
        }
    }
}