﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PociagDoZyskow.DataAccess.Entities;

namespace PociagDoZyskow.DataAccess.Mapping
{
    public class ExchangeMap : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.ShortName).HasColumnName("ShortName");

            builder.HasData(
                new Exchange
                {
                    Id = 1,
                    Name = "Gielda Papierow Wartosciowych",
                    ShortName = "GPW"
                },
                new Exchange
                {
                    Id = 2,
                    Name = "NewConnect",
                    ShortName = "NC"
                }
            );
        }
    }
}
