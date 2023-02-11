﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using standby_data.Models;
using standby_data.Models.ProcedureModels;

namespace standby_data.Context
{
  public partial class standby_orgContext : DbContext
  {
    public standby_orgContext()
    {
    }

    public standby_orgContext(DbContextOptions<standby_orgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tb_checklist> tb_checklist { get; set; }
    public virtual DbSet<tb_clientes> tb_clientes { get; set; }
    public virtual DbSet<tb_comp_items> tb_comp_items { get; set; }
    public virtual DbSet<tb_compras> tb_compras { get; set; }
    public virtual DbSet<tb_condicoes_fisicas> tb_condicoes_fisicas { get; set; }
    public virtual DbSet<tb_garantias> tb_garantias { get; set; }
    public virtual DbSet<tb_gastos> tb_gastos { get; set; }
    public virtual DbSet<tb_log> tb_log { get; set; }
    public virtual DbSet<tb_login> tb_login { get; set; }
    public virtual DbSet<tb_orcamento> tb_orcamento { get; set; }
    public virtual DbSet<tb_pagamento> tb_pagamento { get; set; }
    public virtual DbSet<tb_servicos> tb_servicos { get; set; }
    public virtual DbSet<ServicosUltimaSemana> ServicosUltimaSemana { get; set; }
    public virtual DbSet<BuscarLucroValorUltimaSemana> BuscarLucroValorUltimaSemana { get; set; }
    public virtual DbSet<BuscarServicoValorUltimaSemana> BuscarServicoValorUltimaSemana { get; set; }
    public virtual DbSet<BuscarPrejuizoValorUltimaSemana> BuscarPrejuizoValorUltimaSemana { get; set; }
    public virtual DbSet<BuscarPecasValorUltimaSemana> BuscarPecasValorUltimaSemana { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=standby_org;Persist Security Info=True;User ID=sa;Password=123adr;TrustServerCertificate=True");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<tb_checklist>(entity =>
      {
        entity.HasKey(e => e.ch_id)
                  .HasName("PK_TB_CHECKLIST");

        entity.HasOne(d => d.ch_sv_idservicoNavigation)
                  .WithMany(p => p.tb_checklist)
                  .HasForeignKey(d => d.ch_sv_idservico)
                  .HasConstraintName("tb_checklist_fk0");
      });

      modelBuilder.Entity<tb_clientes>(entity =>
      {
        entity.HasKey(e => e.cl_id)
                  .HasName("pk_tb_clientes");

        entity.Property(e => e.cl_cpf).HasDefaultValueSql("('SEM CPF')");

        entity.Property(e => e.cl_sexo).IsFixedLength();

        entity.Property(e => e.cl_telefone).HasDefaultValueSql("('------------')");

        entity.Property(e => e.cl_telefone_recado).HasDefaultValueSql("('------------')");
      });

      modelBuilder.Entity<tb_comp_items>(entity =>
      {
        entity.HasKey(e => e.item_id)
                  .HasName("PK_TB_COMP_ITEMS");
      });

      modelBuilder.Entity<tb_compras>(entity =>
      {
        entity.HasOne(d => d.cp_sv)
                  .WithMany(p => p.tb_compras)
                  .HasForeignKey(d => d.cp_sv_id)
                  .HasConstraintName("FK_tb_compras_tb_servico");
      });

      modelBuilder.Entity<tb_condicoes_fisicas>(entity =>
      {
        entity.HasKey(e => e.cf_id)
                  .HasName("PK_TB_CONDICOES_FISICAS");

        entity.HasOne(d => d.cf_sv_idservicoNavigation)
                  .WithMany(p => p.tb_condicoes_fisicas)
                  .HasForeignKey(d => d.cf_sv_idservico)
                  .HasConstraintName("tb_condicoes_fisicas_fk0");
      });

      modelBuilder.Entity<tb_garantias>(entity =>
      {
        entity.Property(e => e.gar_id).ValueGeneratedOnAdd();
      });

      modelBuilder.Entity<tb_gastos>(entity =>
      {
        entity.HasKey(e => e.gst_id)
                  .HasName("pk_tb_gastos");

        entity.Property(e => e.gst_ativo).HasDefaultValueSql("((1))");

        entity.Property(e => e.gst_valor).HasDefaultValueSql("((0.00))");
      });

      modelBuilder.Entity<tb_orcamento>(entity =>
      {
        entity.HasKey(e => e.orc_id)
                  .HasName("pk_tb_orcamento");

        entity.Property(e => e.orc_valor).HasDefaultValueSql("((0.00))");

        entity.Property(e => e.total).HasDefaultValueSql("((0.00))");
      });

      modelBuilder.Entity<tb_pagamento>(entity =>
      {
        entity.HasOne(d => d.pag_sv)
                  .WithMany(p => p.tb_pagamento)
                  .HasForeignKey(d => d.pag_sv_id)
                  .HasConstraintName("FK_tb_pagamento_tb_servico");
      });

      modelBuilder.Entity<tb_servicos>(entity =>
      {
        entity.ToTable("tb_servicos", tb => tb.HasTrigger("TGR_BuscarUltimaOrdemServicoID"));

        entity.HasKey(e => e.sv_id)
                  .HasName("pk_tb_servicos");

        entity.Property(e => e.sv_acessorios).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_aparelho).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_ativo).HasDefaultValueSql("((1))");

        entity.Property(e => e.sv_defeito).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_lucro).HasDefaultValueSql("((0.00))");

        entity.Property(e => e.sv_senha).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_servico).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_situacao).HasDefaultValueSql("('-----------------')");

        entity.Property(e => e.sv_status).HasDefaultValueSql("((1))");

        entity.Property(e => e.sv_valorpeca).HasDefaultValueSql("((0.00))");

        entity.Property(e => e.sv_valorservico).HasDefaultValueSql("((0.00))");

        entity.HasOne(d => d.sv_cl_idclienteNavigation)
                  .WithMany(p => p.tb_servicos)
                  .HasForeignKey(d => d.sv_cl_idcliente)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("tb_servicos_fk0");
      });
      ProcedureModel.CarregarProcedures(modelBuilder);
      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}