using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeOrderEngine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionIdToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: new Guid("f9162985-783a-441d-9e1e-257a07565432"));

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "Reviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "CreatedAt", "IsActive", "IsDeleted", "Name", "PinCode", "Role", "UpdatedAt" },
                values: new object[] { new Guid("f9162985-783a-441d-9e1e-257a07565432"), new DateTime(2026, 3, 6, 5, 50, 15, 914, DateTimeKind.Utc).AddTicks(80), true, false, "Admin Staff", "1234", "Admin", null });
        }
    }
}
