using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_GestionDominios.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacionLog",
                table: "DominioInternet",
                type: "character varying(360)",
                maxLength: 360,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCreacionLog",
                table: "DominioInternet");
        }
    }
}
