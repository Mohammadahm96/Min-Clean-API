using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MyMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs");

            migrationBuilder.RenameTable(
                name: "Dogs",
                newName: "AnimalModel");

            migrationBuilder.AddColumn<bool>(
                name: "CanFly",
                table: "AnimalModel",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AnimalModel",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "LikesToPlay",
                table: "AnimalModel",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalModel",
                table: "AnimalModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsersAnimals",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AnimalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAnimals", x => new { x.UserId, x.AnimalId });
                    table.ForeignKey(
                        name: "FK_UsersAnimals_AnimalModel_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAnimals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnimals_AnimalId",
                table: "UsersAnimals",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalModel",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "CanFly",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AnimalModel");

            migrationBuilder.DropColumn(
                name: "LikesToPlay",
                table: "AnimalModel");

            migrationBuilder.RenameTable(
                name: "AnimalModel",
                newName: "Dogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CanFly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LikesToPlay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
