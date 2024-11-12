using CSharpFunctionalExtensions;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Seguro.Api.Domain.Proposta.Model;
using Seguro.Api.Infrastructure;

namespace Seguro.Api.Domain.Proposta.Infraestrutura
{
    public class PropostaRepository(SeguroDbContext seguroDbContext)
    {
        public async Task<Maybe<PropostaDominio>> BuscarProposta(int id)
        {
            return await seguroDbContext.Propostas.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Maybe<DadosVeiculo>> BuscarDadosTabelaFipe(string marca, string modelo, string ano)
        {
            var marcas = await "https://parallelum.com.br/fipe/api/v1/carros/marcas".GetJsonAsync<IEnumerable<MarcasCarros>>();
            var codigoMarca = marcas.FirstOrDefault(q => q.Nome == marca);

            var modelos = await $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos".GetJsonAsync<IEnumerable<ModeloCarros>>();
            var codigoModelo = modelos.FirstOrDefault(q => q.Nome == modelo);

            var anos = await $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos/{codigoModelo}/anos".GetJsonAsync<IEnumerable<AnosCarros>>();
            var codigoAno = anos.FirstOrDefault(q => q.Nome == ano);

            var valor = await $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos/{codigoModelo}/anos/{codigoAno}".GetJsonAsync<DadosVeiculo>();
            return valor;
        }

        public async Task<int> BuscarAcidentesCondutor(string cpf)
        {
            return await Task.FromResult(1);
        }

        public int BuscarPontuacaoPorLocalidadeCondutor(string estado)
        {
            IEnumerable<RegrasLocalidadeDominio> PontuacaoLocalidade =
            [
                new("RS", 5),
                new("SC", 5),
                new("PR", 5),
                new("SP", 20),
                new("RJ", 10),
                new("ES", 5),
                new("DF", 5),
                new("MT", 20),
                new("MS", 20),
            ];

            var dadosEstado = PontuacaoLocalidade.FirstOrDefault(q => q.Estado == estado);
            return dadosEstado?.Pontuacao ?? 0;
        }

        public int BuscarPontuacaoPorIdadeCondutor(DateTime dataNascimento)
        {
            IEnumerable<RegrasIdadeDominio> PontuacaoPorIdade =
            [
                new(18, 25, 15),
                new(26, 40, 5),
                new(41, 60, 3),
                new(61, 200, 10)
            ];

            var idade = DateTime.Now.Year - dataNascimento.Year;
            var dadosIdade = PontuacaoPorIdade.First(q => q.IdadeMin <= idade
                                                        && q.IdadeMax >= idade);

            return dadosIdade.Pontuacao;
        }

        public int BuscarPontuacaoPorAcidentes(int quantidadeAcidentes)
        {
            IEnumerable<PontuacaoAcidenteDominio> pontuacaoAcidente =
            [
                new(0, 0),
                new(1, 10),
                new(2, 10),
                new(3, 30)
            ];

            var pontuacao = pontuacaoAcidente.FirstOrDefault(q => q.Quantidade == quantidadeAcidentes);
            return pontuacao?.Pontuacao ?? 30;
        }

        public int BuscarClassificacaoPorPontuacao(int pontuacao)
        {
            IEnumerable<ClassificacaoNivelRiscoDominio> classificacaoNivelRisco =
            [
                new(PontuacaoIni: 0, PontuacaoFim: 10, Nivel: 1),
                new(PontuacaoIni: 11, PontuacaoFim: 25, Nivel: 2),
                new(PontuacaoIni: 26, PontuacaoFim: 40, Nivel: 3),
                new(PontuacaoIni: 41, PontuacaoFim: 55, Nivel: 4),
                new(PontuacaoIni: 56, PontuacaoFim: 99, Nivel: 5),
            ];

            var dadosNivelRisco = classificacaoNivelRisco.First(q => q.PontuacaoIni <= pontuacao
                                                                && q.PontuacaoFim >= pontuacao);

            return dadosNivelRisco.Nivel;
        }

        public IEnumerable<CustoCoberturasDominio> BuscarCustoPorCoberturas(IEnumerable<int> coberturas)
        {
            IEnumerable<CustoCoberturasDominio> custoCoberturas =
            [
                new(Cobertura: 1, Percentual: true, Custo: 3),
                new(Cobertura: 2, Percentual: true, Custo: 5),
                new(Cobertura: 3, Percentual: true, Custo: 1.5m),
                new(Cobertura: 4, Percentual: false, Custo: 100),
            ];

            var dadosCustosCoberturas = custoCoberturas.Where(q => coberturas.Contains(q.Cobertura));
            return dadosCustosCoberturas;
        }

        public int BuscarAjustePorNivelRisco(int nivelRisco)
        {
            IEnumerable<AjusteNivelRiscoDominio> ajusteNivelRisco =
            [
                new(NivelRisco: 1, PercentualAjuste: 0),
                new(NivelRisco: 2, PercentualAjuste: 5),
                new(NivelRisco: 3, PercentualAjuste: 10),
                new(NivelRisco: 4, PercentualAjuste: 20),
                new(NivelRisco: 5, PercentualAjuste: 30),
            ];

            var dadosAjuste = ajusteNivelRisco.First(q => q.NivelRisco == nivelRisco);
            return dadosAjuste.PercentualAjuste;
        }

        public async Task Adicionar(PropostaDominio proposta)
        {
            await seguroDbContext.Propostas.AddAsync(proposta);
        }

        public async Task Salvar()
        {
            await seguroDbContext.SaveChangesAsync();
        }
    }
}