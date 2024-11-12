using CSharpFunctionalExtensions;

namespace Seguro.Api.Domain.Proposta.Features.CadastrarProposta
{
    public record CadastrarPropostaRequest
    {
        public Veiculo Veiculo { get; set; } = null!;
        public Proprietario Proprietario { get; set; } = null!;
        public Condutor Condutor { get; set; } = null!;
        public IEnumerable<int> Coberturas { get; set; } = null!;

        public Result<CadastrarPropostaCommand> CriarComando()
            => CadastrarPropostaCommand.Criar(Veiculo, Proprietario, Condutor, Coberturas);
    }

    public record Veiculo(string Marca, string Modelo, string Ano);
    public record Proprietario(string Cpf, string Nome, DateTime DataNascimento, Residencia Residencia);
    public record Condutor(string Cpf, DateTime DataNascimento, Residencia Residencia);
    public record Residencia(string Estado, string Localidade, string Bairro, string Logradouro, string? Numero, string? Complemento);
}