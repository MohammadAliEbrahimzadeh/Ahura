using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahura.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ForgeSteps",
                table: "Forges",
                type: "json",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ForgeSteps",
                table: "Forges",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "json",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
