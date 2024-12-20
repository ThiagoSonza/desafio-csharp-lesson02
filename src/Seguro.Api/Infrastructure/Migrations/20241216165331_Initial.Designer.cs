﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Seguro.Api.Infrastructure;

#nullable disable

namespace Seguro.Api.Migrations
{
    [DbContext(typeof(SeguroDbContext))]
    [Migration("20241216165331_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Seguro.Api.Domain.Proposta.Model.PropostaDominio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("HistoricoAcidentes")
                        .HasColumnType("int");

                    b.Property<int>("NivelRisco")
                        .HasColumnType("int");

                    b.Property<int>("PontuacaoNivelRisco")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotalApolice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Propostas", (string)null);
                });

            modelBuilder.Entity("Seguro.Api.Domain.Proposta.Model.PropostaDominio", b =>
                {
                    b.OwnsOne("Seguro.Api.Domain.Proposta.Model.CondutorDominio", "Condutor", b1 =>
                        {
                            b1.Property<int>("PropostaDominioId")
                                .HasColumnType("int");

                            b1.Property<string>("Cpf")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)");

                            b1.Property<DateTime>("DataNascimento")
                                .HasColumnType("datetime2");

                            b1.HasKey("PropostaDominioId");

                            b1.ToTable("Propostas");

                            b1.WithOwner()
                                .HasForeignKey("PropostaDominioId");

                            b1.OwnsOne("Seguro.Api.Domain.Proposta.Model.ResidenciaDominio", "Residencia", b2 =>
                                {
                                    b2.Property<int>("CondutorDominioPropostaDominioId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Bairro")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorBairro");

                                    b2.Property<string>("Complemento")
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorComplemento");

                                    b2.Property<string>("Estado")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorEstado");

                                    b2.Property<string>("Localidade")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorLocalidade");

                                    b2.Property<string>("Logradouro")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorLogradouro");

                                    b2.Property<string>("Numero")
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("CondutorNumero");

                                    b2.HasKey("CondutorDominioPropostaDominioId");

                                    b2.ToTable("Propostas");

                                    b2.WithOwner()
                                        .HasForeignKey("CondutorDominioPropostaDominioId");
                                });

                            b1.Navigation("Residencia")
                                .IsRequired();
                        });

                    b.OwnsOne("Seguro.Api.Domain.Proposta.Model.ProprietarioDominio", "Proprietario", b1 =>
                        {
                            b1.Property<int>("PropostaDominioId")
                                .HasColumnType("int");

                            b1.Property<string>("Cpf")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)")
                                .HasColumnName("ProprietarioCpf");

                            b1.Property<DateTime>("DataNascimento")
                                .HasColumnType("datetime2")
                                .HasColumnName("ProprietarioDataNascimento");

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)")
                                .HasColumnName("ProprietarioNome");

                            b1.HasKey("PropostaDominioId");

                            b1.ToTable("Propostas");

                            b1.WithOwner()
                                .HasForeignKey("PropostaDominioId");

                            b1.OwnsOne("Seguro.Api.Domain.Proposta.Model.ResidenciaDominio", "Residencia", b2 =>
                                {
                                    b2.Property<int>("ProprietarioDominioPropostaDominioId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Bairro")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioBairro");

                                    b2.Property<string>("Complemento")
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioComplemento");

                                    b2.Property<string>("Estado")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioEstado");

                                    b2.Property<string>("Localidade")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioLocalidade");

                                    b2.Property<string>("Logradouro")
                                        .IsRequired()
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioLogradouro");

                                    b2.Property<string>("Numero")
                                        .HasMaxLength(500)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(500)")
                                        .HasColumnName("ProprietarioNumero");

                                    b2.HasKey("ProprietarioDominioPropostaDominioId");

                                    b2.ToTable("Propostas");

                                    b2.WithOwner()
                                        .HasForeignKey("ProprietarioDominioPropostaDominioId");
                                });

                            b1.Navigation("Residencia")
                                .IsRequired();
                        });

                    b.OwnsOne("Seguro.Api.Domain.Proposta.Model.VeiculoDominio", "Veiculo", b1 =>
                        {
                            b1.Property<int>("PropostaDominioId")
                                .HasColumnType("int");

                            b1.Property<string>("Ano")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)")
                                .HasColumnName("VeiculoAno");

                            b1.Property<string>("Marca")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)")
                                .HasColumnName("VeiculoMarca");

                            b1.Property<string>("Modelo")
                                .IsRequired()
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)")
                                .HasColumnName("VeiculoModelo");

                            b1.Property<decimal>("Valor")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("VeiculoValor");

                            b1.HasKey("PropostaDominioId");

                            b1.ToTable("Propostas");

                            b1.WithOwner()
                                .HasForeignKey("PropostaDominioId");
                        });

                    b.Navigation("Condutor")
                        .IsRequired();

                    b.Navigation("Proprietario")
                        .IsRequired();

                    b.Navigation("Veiculo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
