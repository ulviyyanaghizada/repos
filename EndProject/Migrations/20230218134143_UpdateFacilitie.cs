using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacilitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "TFacilities",
                newName: "IconUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "TFacilities",
                newName: "Icon");
        }
    }
}
