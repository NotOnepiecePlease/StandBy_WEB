using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace standby_data.Models.ProcedureModels
{
    public static class ProcedureModel
    {
        public static ModelBuilder CarregarProcedures(ModelBuilder model)
        {
            model.Entity<ServicosUltimos7DiasV2>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            model.Entity<BuscarLucroValorUltimaSemana>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            model.Entity<BuscarServicoValorUltimaSemana>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            model.Entity<BuscarPrejuizoValorUltimaSemana>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            model.Entity<BuscarPecasValorUltimaSemana>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });
            return model;
        }
    }
}