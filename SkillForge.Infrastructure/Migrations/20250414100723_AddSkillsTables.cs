using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 14, 10, 7, 22, 588, DateTimeKind.Utc), "$2a$12$uRghXwlZ16cOP/HxJ8eV8OR4R9LJgra5NwAN4SDaG5d/pIymxswMu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 14, 9, 59, 3, 767, DateTimeKind.Utc), "$2a$11$EcSoUPX.WJMPHdS6/zRydeUZHiz69JAmxkvKI9LGtFhEMEHNEJkwi" });
        }
    }
}
