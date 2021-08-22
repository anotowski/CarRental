using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Database.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BaseDayRentalFee", "Brand", "Category", "IsAvailable", "KilometerFee", "ModelName", "PlateNumber" },
                values: new object[] { 2, 125m, "BMW", 2, true, 5.65m, "M8 Competition", "CarRental002" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BaseDayRentalFee", "Brand", "Category", "IsAvailable", "KilometerFee", "ModelName", "PlateNumber" },
                values: new object[] { 3, 55m, "Hyundai", 1, true, 3.99m, "i30", "CarRental003" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BaseDayRentalFee", "Brand", "Category", "IsAvailable", "KilometerFee", "ModelName", "PlateNumber" },
                values: new object[] { 4, 68.99m, "Volkswagen", 3, true, 3m, "T6 California", "CarRental004" });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PlateNumber",
                table: "Cars",
                column: "PlateNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_PlateNumber",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
