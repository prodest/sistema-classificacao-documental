﻿using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Persistence.Model;
using System;

namespace Prodest.Scd.Infrastructure.Mapping
{
    public partial class ScdContext : DbContext
    {
        public virtual DbSet<CriterioRestricao> CriterioRestricao { get; set; }
        public virtual DbSet<CriterioRestricaoDocumento> CriterioRestricaoDocumento { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<FundamentoLegal> FundamentoLegal { get; set; }
        public virtual DbSet<ItemPlanoClassificacao> ItemPlanoClassificacao { get; set; }
        public virtual DbSet<NivelClassificacao> NivelClassificacao { get; set; }
        public virtual DbSet<Organizacao> Organizacao { get; set; }
        public virtual DbSet<PlanoClassificacao> PlanoClassificacao { get; set; }
        public virtual DbSet<Temporalidade> Temporalidade { get; set; }
        public virtual DbSet<TermoClassificacaoInformacao> TermoClassificacaoInformacao { get; set; }
        public virtual DbSet<TipoDocumental> TipoDocumental { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ScdConnectionString"));

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    optionsBuilder.UseLoggerFactory(new ProcessoEletronicoLoggerFactory());
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CriterioRestricao>(entity =>
            {
                entity.ToTable("CriterioRestricao", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Classificavel).HasColumnName("classificavel");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EventoFim)
                    .HasColumnName("eventoFim")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdFundamentoLegal).HasColumnName("idFundamentoLegal");

                entity.Property(e => e.IdPlanoClassificacao).HasColumnName("idPlanoClassificacao");

                entity.Property(e => e.IdUnidadePrazoTermino).HasColumnName("idUnidadePrazoTermino");

                entity.Property(e => e.Justificativa)
                    .IsRequired()
                    .HasColumnName("justificativa")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PrazoTermino).HasColumnName("prazoTermino");

                entity.HasOne(d => d.FundamentoLegal)
                    .WithMany(p => p.CriteriosRestricao)
                    .HasForeignKey(d => d.IdFundamentoLegal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriterioRestricao_FundamentoLegal");

                entity.HasOne(d => d.PlanoClassificacao)
                    .WithMany(p => p.CriteriosRestricao)
                    .HasForeignKey(d => d.IdPlanoClassificacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriterioRestricao_PlanoClassificacao");
            });

            modelBuilder.Entity<CriterioRestricaoDocumento>(entity =>
            {
                entity.ToTable("CriterioRestricaoDocumento", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCriterioRestricao).HasColumnName("idCriterioRestricao");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.HasOne(d => d.CriterioRestricao)
                    .WithMany(p => p.CriteriosRestricaoDocumento)
                    .HasForeignKey(d => d.IdCriterioRestricao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriterioRestricaoDocumento_CriterioRestricao");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.CriteriosRestricaoDocumento)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriterioRestricaoDocumento_Documento");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.ToTable("Documento", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdItemPlanoClassificacao).HasColumnName("idItemPlanoClassificacao");

                entity.Property(e => e.IdTipoDocumental).HasColumnName("idTipoDocumental");

                entity.HasOne(d => d.ItemPlanoClassificacao)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.IdItemPlanoClassificacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documento_ItemPlanoClassificacao");

                entity.HasOne(d => d.TipoDocumental)
                    .WithMany(p => p.Documentos)
                    .HasForeignKey(d => d.IdTipoDocumental)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documento_TipoDocumental");
            });

            modelBuilder.Entity<FundamentoLegal>(entity =>
            {
                entity.ToTable("FundamentoLegal", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ativo).HasColumnName("ativo");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.FundamentosLegais)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FundamentoLegal_Organizacao");
            });

            modelBuilder.Entity<ItemPlanoClassificacao>(entity =>
            {
                entity.ToTable("ItemPlanoClassificacao", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdItemPlanoClassificacaoParent).HasColumnName("idItemPlanoClassificacaoPai");

                entity.Property(e => e.IdNivelClassificacao).HasColumnName("idNivelClassificacao");

                entity.Property(e => e.IdPlanoClassificacao).HasColumnName("idPlanoClassificacao");

                entity.HasOne(d => d.ItemPlanoClassificacaoParent)
                    .WithMany(p => p.ItensPlanoClassificacaoChildren)
                    .HasForeignKey(d => d.IdItemPlanoClassificacaoParent)
                    .HasConstraintName("FK_ItemPlanoClassificacaoPai");

                entity.HasOne(d => d.NivelClassificacao)
                    .WithMany(p => p.ItensPlanoClassificacao)
                    .HasForeignKey(d => d.IdNivelClassificacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPlanoClassificacao_NivelClassificacao");

                entity.HasOne(d => d.PlanoClassificacao)
                    .WithMany(p => p.ItensPlanoClassificacao)
                    .HasForeignKey(d => d.IdPlanoClassificacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPlanoClassificacao_PlanoClassificacao");
            });

            modelBuilder.Entity<NivelClassificacao>(entity =>
            {
                entity.ToTable("NivelClassificacao", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ativo).HasColumnName("ativo");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.NiveisClassificacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NivelClassificacao_Organizacao");
            });

            modelBuilder.Entity<Organizacao>(entity =>
            {
                entity.ToTable("Organizacao", "dbo");

                entity.HasIndex(e => e.GuidOrganizacao)
                    .HasName("UK_GuidOrganizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");
            });

            modelBuilder.Entity<PlanoClassificacao>(entity =>
            {
                entity.ToTable("PlanoClassificacao", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aprovacao)
                    .HasColumnName("aprovacao")
                    .HasColumnType("date");

                entity.Property(e => e.AreaFim).HasColumnName("areaFim");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FimVigencia)
                    .HasColumnName("fimVigencia")
                    .HasColumnType("date");

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.Property(e => e.InicioVigencia)
                    .HasColumnName("inicioVigencia")
                    .HasColumnType("date");

                entity.Property(e => e.Publicacao)
                    .HasColumnName("publicacao")
                    .HasColumnType("date");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.PlanosClassificacao)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanoClassificacao_Organizacao");
            });

            modelBuilder.Entity<Temporalidade>(entity =>
            {
                entity.ToTable("Temporalidade", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EventoFimFaseCorrente)
                    .HasColumnName("eventoFimFaseCorrente")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EventoFimFaseIntermediaria)
                    .HasColumnName("eventoFimFaseIntermediaria")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.IdDestinacaoFinal).HasColumnName("idDestinacaoFinal");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.IdUnidadePrazoGuardaFaseCorrente).HasColumnName("idUnidadePrazoGuardaFaseCorrente");

                entity.Property(e => e.IdUnidadePrazoGuardaFaseIntermediaria).HasColumnName("idUnidadePrazoGuardaFaseIntermediaria");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PrazoGuardaFaseCorrente).HasColumnName("prazoGuardaFaseCorrente");

                entity.Property(e => e.PrazoGuardaFaseIntermediaria).HasColumnName("prazoGuardaFaseIntermediaria");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.Temporalidades)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Temporalidade_Documento");
            });

            modelBuilder.Entity<TermoClassificacaoInformacao>(entity =>
            {
                entity.ToTable("TermoClassificacaoInformacao", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConteudoSigilo)
                    .IsRequired()
                    .HasColumnName("conteudoSigilo")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CpfIndicacaoAprovador)
                    .IsRequired()
                    .HasColumnName("cpfIndicacaoAprovador")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CpfUsuario)
                    .IsRequired()
                    .HasColumnName("cpfUsuario")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DataClassificacao)
                    .HasColumnName("dataClassificacao")
                    .HasColumnType("date");

                entity.Property(e => e.DataProducaoDocumento)
                    .HasColumnName("dataProducaoDocumento")
                    .HasColumnType("date");

                entity.Property(e => e.FundamentoLegal)
                    .IsRequired()
                    .HasColumnName("fundamentoLegal")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");

                entity.Property(e => e.IdCriterioRestricao).HasColumnName("idCriterioRestricao");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.IdGrauSigilo).HasColumnName("idGrauSigilo");

                entity.Property(e => e.IdTipoSigilo).HasColumnName("idTipoSigilo");

                entity.Property(e => e.IdUnidadePrazoSigilo).HasColumnName("idUnidadePrazoSigilo");

                entity.Property(e => e.IdentificadorDocumento)
                    .IsRequired()
                    .HasColumnName("identificadorDocumento")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Justificativa)
                    .IsRequired()
                    .HasColumnName("justificativa")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PrazoSigilo).HasColumnName("prazoSigilo");

                entity.HasOne(d => d.CriterioRestricao)
                    .WithMany(p => p.TermosClassificacaoInformacao)
                    .HasForeignKey(d => d.IdCriterioRestricao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TermoClassificacaoInformacao_CriterioRestricao");


                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.TermosClassificacaoInformacao)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TermoClassificacaoInformacao_Documento");
            });

            modelBuilder.Entity<TipoDocumental>(entity =>
            {
                entity.ToTable("TipoDocumental", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ativo).HasColumnName("ativo");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdOrganizacao).HasColumnName("idOrganizacao");

                entity.HasOne(d => d.Organizacao)
                    .WithMany(p => p.TiposDocumentais)
                    .HasForeignKey(d => d.IdOrganizacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoDocumental_Organizacao");
            });
        }
    }
}
