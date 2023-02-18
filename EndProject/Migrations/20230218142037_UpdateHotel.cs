using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MealDescruption",
                table: "Hotels",
                newName: "MealDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MealDescription",
                table: "Hotels",
                newName: "MealDescruption");
        }
    }
}
