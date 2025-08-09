using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntropiaInventoryShareWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "License",
                table: "Avatars",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "License",
                table: "Avatars");
        }
    }
}
