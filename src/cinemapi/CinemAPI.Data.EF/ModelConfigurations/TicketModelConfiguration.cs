using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class TicketModelConfiguration : IModelConfiguration

    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Ticket> ReservationModel = modelBuilder.Entity<Ticket>();
            ReservationModel.HasKey(model => model.Id);
            ReservationModel.Property(model => model.ProjectionId).IsRequired();
            ReservationModel.Property(model => model.Row).IsRequired();
            ReservationModel.Property(model => model.Column).IsRequired();
        }
    }
}
