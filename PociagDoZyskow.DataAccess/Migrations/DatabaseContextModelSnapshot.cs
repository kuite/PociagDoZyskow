﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PociagDoZyskow.DataAccess.Contexts;

namespace PociagDoZyskow.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PociagDoZyskow.DataAccess.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExchangeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnName("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExchangeId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("PociagDoZyskow.DataAccess.Entities.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnName("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exchanges");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gielda Papierow Wartosciowych",
                            ShortName = "GPW"
                        },
                        new
                        {
                            Id = 2,
                            Name = "NewConnect",
                            ShortName = "NC"
                        });
                });

            modelBuilder.Entity("PociagDoZyskow.DataAccess.Entities.Company", b =>
                {
                    b.HasOne("PociagDoZyskow.DataAccess.Entities.Exchange", "Exchange")
                        .WithMany("Companies")
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
