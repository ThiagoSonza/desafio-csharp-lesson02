using System.Text;
using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Domain.Proposta.Features.AprovarProposta
{
    public class AprovarPropostaCommand : IRequest<Result<PropostaDominio>>
    {
        private AprovarPropostaCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public static Result<AprovarPropostaCommand> Criar(int proposta)
        {
            AprovarPropostaCommand command = new(proposta);

            var validation = new AprovarPropostaValidator().Validate(command);
            if (validation.IsValid)
                return Result.Success(command);

            var errors = validation.Errors.Select(err => err.ErrorMessage);
            StringBuilder errorMessage = new();
            foreach (string error in errors)
                errorMessage.Append(error).AppendJoin(". ");

            return Result.Failure<AprovarPropostaCommand>(errorMessage.ToString());
        }
    }
}