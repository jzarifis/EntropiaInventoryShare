using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntropiaInventoryShareWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraShareItemInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Shop",
                table: "SharedItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TIR",
                table: "SharedItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tier",
                table: "SharedItems",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shop",
                table: "SharedItems");

            migrationBuilder.DropColumn(
                name: "TIR",
                table: "SharedItems");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "SharedItems");
        }
    }
}
