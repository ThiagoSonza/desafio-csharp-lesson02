using System.Text;
using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Domain.Proposta.Features.RejeitarProposta
{
    public class RejeitarPropostaCommand : IRequest<Result<PropostaDominio>>
    {
        private RejeitarPropostaCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public static Result<RejeitarPropostaCommand> Criar(int proposta)
        {
            RejeitarPropostaCommand command = new(proposta);

            var validation = new RejeitarPropostaValidator().Validate(command);
            if (validation.IsValid)
                return Result.Success(command);

            var errors = validation.Errors.Select(err => err.ErrorMessage);
            StringBuilder errorMessage = new();
            foreach (string error in errors)
                errorMessage.Append(error).AppendJoin(". ");

            return Result.Failure<RejeitarPropostaCommand>(errorMessage.ToString());
        }
    }
}