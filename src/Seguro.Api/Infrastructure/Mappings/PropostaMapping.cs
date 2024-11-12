using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Infrastructure.Mappings
{
    public class PropostaMapping : IEntityTypeConfiguration<PropostaDominio>
    {
        public void Configure(EntityTypeBuilder<PropostaDominio> builder)
        {
            builder.ToTable("Propostas");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Data);
            builder.Property(p => p.Status);
            builder.Property(p => p.HistoricoAcidentes);
            builder.Property(p => p.ValorVeiculo);
            builder.Property(p => p.NivelRisco);
            builder.Property(p => p.PontuacaoNivelRisco);
            builder.Property(p => p.ValorTotalApolice);

            builder.OwnsOne(p => p.Veiculo, veiculo =>
            {
                veiculo.Property(p => p.Marca).HasColumnName("VeiculoMarca");
                veiculo.Property(p => p.Modelo).HasColumnName("VeiculoModelo");
                veiculo.Property(p => p.Ano).HasColumnName("VeiculoAno");
            });

            builder.OwnsOne(p => p.Condutor, condutor =>
            {
                condutor.Property(p => p.Cpf);
                condutor.Property(p => p.DataNascimento);

                condutor.OwnsOne(p => p.Residencia, residencia =>
                {
                    residencia.Property(p => p.Estado).HasColumnName("CondutorEstado");
                    residencia.Property(p => p.Localidade).HasColumnName("CondutorLocalidade");
                    residencia.Property(p => p.Bairro).HasColumnName("CondutorBairro");
                    residencia.Property(p => p.Logradouro).HasColumnName("CondutorLogradouro");
                    residencia.Property(p => p.Numero).HasColumnName("CondutorNumero");
                    residencia.Property(p => p.Complemento).HasColumnName("CondutorComplemento");
                });
            });

            builder.OwnsOne(p => p.Proprietario, proprietario =>
            {
                proprietario.Property(p => p.Cpf).HasColumnName("ProprietarioCpf");
                proprietario.Property(p => p.Nome).HasColumnName("ProprietarioNome");
                proprietario.Property(p => p.DataNascimento).HasColumnName("ProprietarioDataNascimento");

                proprietario.OwnsOne(p => p.Residencia, residencia =>
                {
                    residencia.Property(p => p.Estado).HasColumnName("ProprietarioEstado");
                    residencia.Property(p => p.Localidade).HasColumnName("ProprietarioLocalidade");
                    residencia.Property(p => p.Bairro).HasColumnName("ProprietarioBairro");
                    residencia.Property(p => p.Logradouro).HasColumnName("ProprietarioLogradouro");
                    residencia.Property(p => p.Numero).HasColumnName("ProprietarioNumero");
                    residencia.Property(p => p.Complemento).HasColumnName("ProprietarioComplemento");
                });
            });
        }
    }
}