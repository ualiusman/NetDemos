﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMediate.Data;

namespace PMediate.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201228000505_Initials")]
    partial class Initials
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("PMediate.Model.Consult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Consults");
                });

            modelBuilder.Entity("PMediate.Model.Consult", b =>
                {
                    b.OwnsOne("PMediate.Model.DateRange", "DateRange", b1 =>
                        {
                            b1.Property<long>("ConsultId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .UseIdentityColumn();

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("datetime2");

                            b1.HasKey("ConsultId");

                            b1.ToTable("Consults");

                            b1.WithOwner()
                                .HasForeignKey("ConsultId");
                        });

                    b.Navigation("DateRange");
                });
#pragma warning restore 612, 618
        }
    }
}