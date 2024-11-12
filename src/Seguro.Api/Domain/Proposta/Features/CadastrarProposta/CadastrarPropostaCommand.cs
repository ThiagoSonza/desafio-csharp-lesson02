using System.Text;
using CSharpFunctionalExtensions;
using MediatR;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public record CadastrarPropostaCommand : IRequest<Result<PropostaDominio>>
    {
        private CadastrarPropostaCommand(Veiculo veiculo, Proprietario proprietario, Condutor condutor, IEnumerable<int> Coberturas)
        {
            Veiculo = new(veiculo.Marca, veiculo.Modelo, veiculo.Ano, 0);
            this.Coberturas = Coberturas;

            Proprietario = new(
                proprietario.Cpf,
                proprietario.Nome,
                proprietario.DataNascimento,
                new(
                    proprietario.Residencia.Estado,
                    proprietario.Residencia.Localidade,
                    proprietario.Residencia.Bairro,
                    proprietario.Residencia.Logradouro,
                    proprietario.Residencia.Numero,
                    proprietario.Residencia.Complemento
                ));

            Condutor = new(
                condutor.Cpf,
                condutor.DataNascimento,
                new(
                    condutor.Residencia.Estado,
                    condutor.Residencia.Localidade,
                    condutor.Residencia.Bairro,
                    condutor.Residencia.Logradouro,
                    condutor.Residencia.Numero,
                    condutor.Residencia.Complemento
                ));
        }

        public VeiculoCommand Veiculo { get; private set; }
        public ProprietarioCommand Proprietario { get; }
        public CondutorCommand Condutor { get; }
        public IEnumerable<int> Coberturas { get; }
        public int HistoricoAcidentes { get; private set; }
        public int NivelRisco { get; private set; }

        public static Result<CadastrarPropostaCommand> Criar(Veiculo veiculo, Proprietario proprietario, Condutor condutor, IEnumerable<int> coberturas)
        {
            CadastrarPropostaCommand command = new(veiculo, proprietario, condutor, coberturas);

            var validation = new CadastrarPropostaValidator().Validate(command);
            if (validation.IsValid)
                return Result.Success(command);

            var errors = validation.Errors.Select(err => err.ErrorMessage);
            StringBuilder errorMessage = new();
            foreach (string error in errors)
                errorMessage.Append(error).AppendJoin(". ");

            return Result.Failure<CadastrarPropostaCommand>(errorMessage.ToString());
        }

        public void AtualizaDadosVeiculo(string marca, string modelo, string ano, decimal valorVeiculo)
            => Veiculo = new(marca, modelo, ano, valorVeiculo);

        public void AtualizaHistoricoAcidentes(int quantidadeAcidentes)
            => HistoricoAcidentes = quantidadeAcidentes;

        public void AtualizaNivelRisco(int nivelRisco)
            => NivelRisco = nivelRisco;
    }


    public record VeiculoCommand(string Marca, string Modelo, string Ano, decimal? Valor);
    public record ProprietarioCommand(string Cpf, string Nome, DateTime DataNascimento, ResidenciaCommand Residencia);
    public record CondutorCommand(string Cpf, DateTime DataNascimento, ResidenciaCommand Residencia);
    public record ResidenciaCommand(string Estado, string Localidade, string Bairro, string Logradouro, string? Numero, string? Complemento);
}