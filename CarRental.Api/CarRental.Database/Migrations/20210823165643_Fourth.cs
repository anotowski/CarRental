using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Database.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "MilageOnRentalStart",
                table: "RentalHistories",
                newName: "MileageOnRentalStart");

            migrationBuilder.RenameColumn(
                name: "MilageOnRentalEnd",
                table: "RentalHistories",
                newName: "MileageOnRentalEnd");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentMileage",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CurrentMileage",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "MileageOnRentalStart",
                table: "RentalHistories",
                newName: "MilageOnRentalStart");

            migrationBuilder.RenameColumn(
                name: "MileageOnRentalEnd",
                table: "RentalHistories",
                newName: "MilageOnRentalEnd");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
