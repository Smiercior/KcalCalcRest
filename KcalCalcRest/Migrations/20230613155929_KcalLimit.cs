using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KcalCalcRest.Migrations
{
    /// <inheritdoc />
    public partial class KcalLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CarbohydrateLimit",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FatLimit",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "KcalLimit",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProteinLimit",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "AspNetUsers",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbohydrateLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FatLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KcalLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProteinLimit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");
        }
    }
}
