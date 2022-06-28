using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioLaNacion.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactRecords",
                table: "ContactRecords");

            migrationBuilder.RenameTable(
                name: "ContactRecords",
                newName: "contact_record");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "contact_record",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_record",
                table: "contact_record",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_record",
                table: "contact_record");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "contact_record");

            migrationBuilder.RenameTable(
                name: "contact_record",
                newName: "ContactRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactRecords",
                table: "ContactRecords",
                column: "id");
        }
    }
}
