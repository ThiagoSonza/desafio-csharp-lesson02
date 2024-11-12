using CSharpFunctionalExtensions;
using Seguro.Api.Comum;
using Seguro.Api.Domain.Proposta.Features.CadastrarProposta;

namespace Seguro.Api.Domain.Proposta.Model
{
    public class PropostaDominio : Entity<int>
    {
        private PropostaDominio(int id, string veiculoMarca, string veiculoModelo, string veiculoAno, decimal veiculoValor,
                                string condutorCpf, DateTime condutorDtNascimento,
                                string condutorEstado, string condutorLocalidade, string condutorBairro, string condutorLogradouro, string? condutorNumero, string? condutorComplemento,
                                string proprietarioCpf, string proprietarioNome, DateTime proprietarioDtNascimento,
                                string proprietarioEstado, string proprietarioLocalidade, string proprietarioBairro, string proprietarioLogradouro, string? proprietarioNumero, string? proprietarioComplemento,
                                int statusProposta, int historicoAcidentes, int nivelRisco, int pontosNivelRisco, decimal valorApolice
                                ) : base(id)
        {
            Data = DateTime.Now;
            Veiculo = new(veiculoMarca, veiculoModelo, veiculoAno, veiculoValor);

            Condutor = new(
                condutorCpf,
                condutorDtNascimento,
                new(
                    condutorEstado,
                    condutorLocalidade,
                    condutorBairro,
                    condutorLogradouro,
                    condutorNumero,
                    condutorComplemento
                )
            );

            Proprietario = new(
                proprietarioCpf,
                proprietarioNome,
                proprietarioDtNascimento,
                new(
                    proprietarioEstado,
                    proprietarioLocalidade,
                    proprietarioBairro,
                    proprietarioLogradouro,
                    proprietarioNumero,
                    proprietarioComplemento
                )
            );

            Status = statusProposta;
            HistoricoAcidentes = historicoAcidentes;
            NivelRisco = nivelRisco;
            PontuacaoNivelRisco = pontosNivelRisco;
            ValorTotalApolice = valorApolice;
        }

        public DateTime Data { get; }
        public VeiculoDominio Veiculo { get; }
        public CondutorDominio Condutor { get; }
        public ProprietarioDominio Proprietario { get; }
        public int Status { get; private set; }
        public int HistoricoAcidentes { get; }
        public int NivelRisco { get; }
        public int PontuacaoNivelRisco { get; }
        public decimal ValorTotalApolice { get; }

        public static Result<PropostaDominio> Criar(VeiculoCommand veiculo, ProprietarioCommand proprietario, CondutorCommand condutor,
                                                    IEnumerable<int> coberturas, int historicoAcidentes, int nivelRisco, int pontosNivelRisco,
                                                    decimal valorApolice)
        {
            var proposta = new PropostaDominio(0, veiculo.Marca, veiculo.Modelo, veiculo.Ano, veiculo.Valor.Value,
                                                condutor.Cpf, condutor.DataNascimento,
                                                condutor.Residencia.Estado, condutor.Residencia.Localidade, condutor.Residencia.Bairro, condutor.Residencia.Logradouro, condutor.Residencia.Numero, condutor.Residencia.Complemento,
                                                proprietario.Cpf, proprietario.Nome, proprietario.DataNascimento,
                                                proprietario.Residencia.Estado, proprietario.Residencia.Localidade, proprietario.Residencia.Bairro, proprietario.Residencia.Logradouro, proprietario.Residencia.Numero, proprietario.Residencia.Complemento,
                                                (int)EStatusProposta.EmAprovacao, historicoAcidentes, nivelRisco, pontosNivelRisco, valorApolice
                                                );
            return Result.Success(proposta);
        }

        public void Aprovar()
            => Status = (int)EStatusProposta.Aprovada;

        public void Rejeitar()
            => Status = (int)EStatusProposta.Rejeitada;
    }

    public record VeiculoDominio(string Marca, string Modelo, string Ano, decimal Valor);
    public record ProprietarioDominio(string Cpf, string Nome, DateTime DataNascimento, ResidenciaDominio Residencia);
    public record CondutorDominio(string Cpf, DateTime DataNascimento, ResidenciaDominio Residencia);
    public record ResidenciaDominio(string Estado, string Localidade, string Bairro, string Logradouro, string? Numero, string? Complemento);
}