using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartStore.Infra.Migrations
{
    public partial class MigrationAddPropTagClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagClient",
                table: "Usuarios",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagClient",
                table: "Usuarios");
        }
    }
}
