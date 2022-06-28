using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioLaNacion.Migrations
{
    public partial class Migratoin3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "contact_record",
                newName: "first_name");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "contact_record",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_name",
                table: "contact_record");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "contact_record",
                newName: "name");
        }
    }
}
