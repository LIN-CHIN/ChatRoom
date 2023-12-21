using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MqttBroker.Migrations
{
    /// <inheritdoc />
    public partial class adduserseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "chat_room",
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "create_date",
                value: new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3829));

            migrationBuilder.InsertData(
                schema: "chat_room",
                table: "users",
                columns: new[] { "id", "create_date", "pwd", "user_id" },
                values: new object[,]
                {
                    { 2L, new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3832), "1234", "user1" },
                    { 3L, new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3833), "1234", "user2" },
                    { 4L, new DateTime(2023, 12, 21, 2, 22, 58, 963, DateTimeKind.Utc).AddTicks(3834), "1234", "user3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "chat_room",
                table: "users",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "chat_room",
                table: "users",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "chat_room",
                table: "users",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                schema: "chat_room",
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                column: "create_date",
                value: new DateTime(2023, 5, 5, 8, 43, 35, 94, DateTimeKind.Utc).AddTicks(5074));
        }
    }
}
