using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeOrderEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReviewAllowedToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReviewAllowed",
                table: "Tables",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewAllowed",
                table: "Tables");
        }
    }
}
