using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigrationAdminFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "d304c3ce-9db8-4ba0-9b03-fde5a997644a", "admin@gmail.com", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEDS8giyqt0y3Rf6R+lVr/VevH4sRU33CsecFaZiprH0QQT5a4vuHwlLU+ADNn/+8qg==", "Admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "a59758c1-c5b5-4264-8056-653926b4b129", "supportadmin@gmail.com", true, false, null, "SUPPORTADMIN@GMAIL.COM", "SUPPORTADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEF7t0UV/7sZBVvoNrPZdt4UBMBsSYm81tIVDgbykOY/A4Rhm9yuaTbpjOd10pLWdVA==", null, false, "fac61df7-d285-4aa5-b862-d3acaadb4eab", false, "SupportAdmin@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "61f3f34c-be8a-4578-8119-6886ab3e337a", "ryhab@gmail.com", "RYHAB@GMAIL.COM", "RYHAB@GMAIL.COM", "AQAAAAIAAYagAAAAELP7Nflp1ZT6eQpcH33F/+jBiD8wPyPCVUrZuBtsPckTySBRsU9GKzSskakLxBayUw==", "Ryhab@gmail.com" });
        }
    }
}
