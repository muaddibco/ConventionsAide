﻿// <auto-generated />
using System;
using ConventionsAide.Venues.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConventionsAide.Venues.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(VenuesDbContext))]
    [Migration("20220710123721_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ConventionsAide.Venues.Domain.Venue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address1")
                        .HasColumnType("text");

                    b.Property<string>("Address2")
                        .HasColumnType("text");

                    b.Property<string>("Address3")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("Latitude")
                        .HasColumnType("bigint");

                    b.Property<long?>("Longitude")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("VenueType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Venues", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}