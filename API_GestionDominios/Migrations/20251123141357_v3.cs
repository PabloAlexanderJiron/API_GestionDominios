using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API_GestionDominios.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenciaSoftware",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Proveedor = table.Column<string>(type: "text", nullable: false),
                    FechaCompra = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Precio = table.Column<double>(type: "double precision", nullable: false),
                    FechaRenovacion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IncluyeSoporte = table.Column<bool>(type: "boolean", nullable: false),
                    EmailSoporte = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Inactivo = table.Column<bool>(type: "boolean", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacionLog = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FechaModificacionLog = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreacionLog = table.Column<string>(type: "character varying(360)", maxLength: 360, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenciaSoftware", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenciaSoftware");
        }
    }
}
