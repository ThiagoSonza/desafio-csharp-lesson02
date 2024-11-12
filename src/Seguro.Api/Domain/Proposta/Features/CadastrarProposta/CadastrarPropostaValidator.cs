using FluentValidation;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public class CadastrarPropostaValidator : AbstractValidator<CadastrarPropostaCommand>
    {
        public CadastrarPropostaValidator()
        {
            RuleFor(v => v.Veiculo).NotNull().WithMessage("Os dados do veículo devem ser informados");
            RuleFor(v => v.Veiculo.Marca).NotNull().WithMessage("A marca do veículo deve ser informada");
            RuleFor(v => v.Veiculo.Modelo).NotNull().WithMessage("O modelo do veículo deve ser informado");
            RuleFor(v => v.Veiculo.Ano).NotNull().WithMessage("O ano do veículo deve ser informado");

            RuleFor(v => v.Proprietario).NotNull().WithMessage("Os dados do proprietário devem ser informados");
            RuleFor(v => v.Proprietario.Cpf).NotNull().WithMessage("O cpf do proprietário deve ser informado");
            RuleFor(v => v.Proprietario.Nome).NotNull().WithMessage("O nome do proprietário deve ser informado");
            RuleFor(v => v.Proprietario.DataNascimento).NotNull().WithMessage("A data de nascimento do proprietário deve ser informada");
            RuleFor(v => v.Proprietario.Residencia.Estado).NotNull().WithMessage("O Estado de residência do proprietário deve ser informado");
            RuleFor(v => v.Proprietario.Residencia.Localidade).NotNull().WithMessage("A localidade de residência do proprietário deve ser informada");
            RuleFor(v => v.Proprietario.Residencia.Bairro).NotNull().WithMessage("O bairro de residência do proprietário deve ser informado");
            RuleFor(v => v.Proprietario.Residencia.Logradouro).NotNull().WithMessage("O logradouro de residência do proprietário deve ser informado");

            RuleFor(v => v.Condutor).NotNull().WithMessage("Os dados do condutor devem ser informados");
            RuleFor(v => v.Condutor.Cpf).NotNull().WithMessage("O cpf do condutor deve ser informado");
            RuleFor(v => v.Condutor.DataNascimento).NotNull().WithMessage("A data de nascimento do condutor deve ser informada");
            RuleFor(v => v.Condutor.Residencia.Estado).NotNull().WithMessage("O Estado de residência do condutor deve ser informado");
            RuleFor(v => v.Condutor.Residencia.Localidade).NotNull().WithMessage("A localidade de residência do condutor deve ser informada");
            RuleFor(v => v.Condutor.Residencia.Bairro).NotNull().WithMessage("O bairro de residência do condutor deve ser informado");
            RuleFor(v => v.Condutor.Residencia.Logradouro).NotNull().WithMessage("O logradouro de residência do condutor deve ser informado");

            RuleFor(v => v.Coberturas).NotEmpty().WithMessage("As coberturas devem ser informadas");
        }
    }
}