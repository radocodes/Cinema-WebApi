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
            EntityTypeConfiguration<Ticket> TicketModel = modelBuilder.Entity<Ticket>();
            TicketModel.HasKey(model => model.Id);
            TicketModel.Property(model => model.ProjectionId).IsRequired();
            TicketModel.Property(model => model.Row).IsRequired();
            TicketModel.Property(model => model.Column).IsRequired();
        }
    }
}
