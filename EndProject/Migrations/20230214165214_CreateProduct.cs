using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProject.Migrations
{
    /// <inheritdoc />
    public partial class CreateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProductFeatures");

            migrationBuilder.AddColumn<int>(
                name: "PFeatureId",
                table: "ProductFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PFeatures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_PFeatureId",
                table: "ProductFeatures",
                column: "PFeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_PFeatures_PFeatureId",
                table: "ProductFeatures",
                column: "PFeatureId",
                principalTable: "PFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_PFeatures_PFeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "PFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_PFeatureId",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "PFeatureId",
                table: "ProductFeatures");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProductFeatures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
