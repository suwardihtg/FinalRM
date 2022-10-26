using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class initiaal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Payee",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "Approver",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CommentFinace",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NIK",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Start_Date",
                table: "Forms",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Receipt_Date",
                table: "Forms",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "End_Date",
                table: "Forms",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "Submitted",
                table: "Expenses",
                newName: "SubmittedDate");

            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "Expenses",
                newName: "CommentFinance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Forms",
                newName: "Start_Date");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Forms",
                newName: "Receipt_Date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Forms",
                newName: "End_Date");

            migrationBuilder.RenameColumn(
                name: "SubmittedDate",
                table: "Expenses",
                newName: "Submitted");

            migrationBuilder.RenameColumn(
                name: "CommentFinance",
                table: "Expenses",
                newName: "Purpose");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payee",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Approver",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentFinace",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
