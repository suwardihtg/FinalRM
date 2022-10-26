using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class addbanknametoformtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Forms");
        }
    }
}
