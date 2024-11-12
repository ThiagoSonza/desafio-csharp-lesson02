namespace Seguro.Api.Domain.Proposta.Model
{
    public record MarcasCarros(string Codigo, string Nome);
    public record ModeloCarros(int Codigo, string Nome);
    public record AnosCarros(string Codigo, string Nome);
    public record DadosVeiculo(int TipoVeiculo, string Valor, string Marca, string Modelo, int AnoModelo, string Combustivel, string CodigoFipe, string MesReferencia, string SiglaCombustivel);
}