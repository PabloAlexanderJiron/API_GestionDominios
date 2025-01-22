﻿// <auto-generated />
using System;
using Aplicacion.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_GestionDominios.Migrations
{
    [DbContext(typeof(ContextoDB))]
    [Migration("20241229130918_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Aplicacion.Dominio.Entidades.DominioInternet.DominioInternet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("FechaCompra")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("FechaCreacionLog")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("FechaModificacionLog")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("FechaRenovacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer");

                    b.Property<bool>("Inactivo")
                        .HasColumnType("boolean");

                    b.Property<double>("Precio")
                        .HasColumnType("double precision");

                    b.Property<string>("Proveedor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UsuarioCreacionLog")
                        .IsRequired()
                        .HasMaxLength(360)
                        .HasColumnType("character varying(360)");

                    b.HasKey("Id");

                    b.ToTable("DominioInternet");
                });
#pragma warning restore 612, 618
        }
    }
}
