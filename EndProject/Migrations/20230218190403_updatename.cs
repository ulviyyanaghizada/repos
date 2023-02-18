using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Migrations
{
    /// <inheritdoc />
    public partial class updatename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourFacilities_TFacilities_TFacilitieId",
                table: "tourFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_tourFacilities_Tours_TourId",
                table: "tourFacilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourFacilities",
                table: "tourFacilities");

            migrationBuilder.RenameTable(
                name: "tourFacilities",
                newName: "TourFacilities");

            migrationBuilder.RenameIndex(
                name: "IX_tourFacilities_TourId",
                table: "TourFacilities",
                newName: "IX_TourFacilities_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_tourFacilities_TFacilitieId",
                table: "TourFacilities",
                newName: "IX_TourFacilities_TFacilitieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourFacilities",
                table: "TourFacilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourFacilities_TFacilities_TFacilitieId",
                table: "TourFacilities",
                column: "TFacilitieId",
                principalTable: "TFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourFacilities_Tours_TourId",
                table: "TourFacilities",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourFacilities_TFacilities_TFacilitieId",
                table: "TourFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_TourFacilities_Tours_TourId",
                table: "TourFacilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourFacilities",
                table: "TourFacilities");

            migrationBuilder.RenameTable(
                name: "TourFacilities",
                newName: "tourFacilities");

            migrationBuilder.RenameIndex(
                name: "IX_TourFacilities_TourId",
                table: "tourFacilities",
                newName: "IX_tourFacilities_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_TourFacilities_TFacilitieId",
                table: "tourFacilities",
                newName: "IX_tourFacilities_TFacilitieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourFacilities",
                table: "tourFacilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourFacilities_TFacilities_TFacilitieId",
                table: "tourFacilities",
                column: "TFacilitieId",
                principalTable: "TFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourFacilities_Tours_TourId",
                table: "tourFacilities",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
