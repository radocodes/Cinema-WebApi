using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> ReservationModel = modelBuilder.Entity<Reservation>();
            ReservationModel.HasKey(model => model.Id);
            ReservationModel.Property(model => model.ProjectionId).IsRequired();
            ReservationModel.Property(model => model.Row).IsRequired();
            ReservationModel.Property(model => model.Column).IsRequired();
        }
    }
}
