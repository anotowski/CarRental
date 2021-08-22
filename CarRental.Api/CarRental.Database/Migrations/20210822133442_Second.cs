using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Database.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BaseDayRentalFee", "Brand", "Category", "IsAvailable", "KilometerFee", "ModelName", "PlateNumber" },
                values: new object[] { 1, 120m, "Volvo", 2, true, 5m, "S90", "CarRental001" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
