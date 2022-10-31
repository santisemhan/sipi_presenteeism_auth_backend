﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SIPI_PRESENTEEISM.Data;

#nullable disable

namespace SIPI_PRESENTEEISM.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Activity", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '-03:00') AS DATETIME2(3))");

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("EmployeeId", "TimeStamp");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("ValidationCode")
                        .HasColumnType("int");

                    b.Property<Guid>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ZoneId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.ImageToIdentify", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmployeeId", "ImageURL");

                    b.ToTable("ImageToIdentify");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Zone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RadioKm")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Zone");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Activity", b =>
                {
                    b.HasOne("SIPI_PRESENTEEISM.Core.Domain.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Employee", b =>
                {
                    b.HasOne("SIPI_PRESENTEEISM.Core.Domain.Entities.Zone", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.ImageToIdentify", b =>
                {
                    b.HasOne("SIPI_PRESENTEEISM.Core.Domain.Entities.Employee", "Employee")
                        .WithMany("ImagesToIdentify")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SIPI_PRESENTEEISM.Core.Domain.Entities.Employee", b =>
                {
                    b.Navigation("ImagesToIdentify");
                });
#pragma warning restore 612, 618
        }
    }
}
