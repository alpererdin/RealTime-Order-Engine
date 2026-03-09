using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeOrderEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceSessionIdWithOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Reviews",
                newName: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Reviews",
                newName: "SessionId");
        }
    }
}
