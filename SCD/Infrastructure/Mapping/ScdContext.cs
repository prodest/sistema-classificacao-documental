using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Persistence.Model;
using System;

namespace Prodest.Scd.Infrastructure.Mapping
{
    public partial class ScdContext : DbContext
    {
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<ItemPlanoClassificacao> ItemPlanoClassificacao { get; set; }
        public virtual DbSet<NivelClassificacao> NivelClassificacao { get; set; }
        public virtual DbSet<Organizacao> Organizacao { get; set; }
        public virtual DbSet<PlanoClassificacao> PlanoClassificacao { get; set; }
        public virtual DbSet<Sigilo> Sigilo { get; set; }
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
            modelBuilder.Entity<Documento>(entity =>
            {
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

            modelBuilder.Entity<ItemPlanoClassificacao>(entity =>
            {
                entity.ToTable("ItemPlanoClassificacao");

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
                entity.ToTable("NivelClassificacao");

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
                entity.ToTable("Organizacao");

                entity.HasIndex(e => e.GuidOrganizacao)
                    .HasName("UK_GuidOrganizacao")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GuidOrganizacao).HasColumnName("guidOrganizacao");
            });

            modelBuilder.Entity<PlanoClassificacao>(entity =>
            {
                entity.ToTable("PlanoClassificacao");

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

            modelBuilder.Entity<Sigilo>(entity =>
            {
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

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.HasOne(d => d.Documento)
                    .WithMany(p => p.Sigilo)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sigilo_Documento");
            });

            modelBuilder.Entity<TipoDocumental>(entity =>
            {
                entity.ToTable("TipoDocumental");

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
