using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "AnimalModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Dog_Breed",
                table: "AnimalModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Dog_Weight",
                table: "AnimalModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AnimalModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "AnimalModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "Dog_Breed",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "Dog_Weight",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "color",
                table: "AnimalModel");
        }
    }
}
