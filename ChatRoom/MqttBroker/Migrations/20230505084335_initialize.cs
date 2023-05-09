using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MqttBroker.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chat_room");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "chat_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "系統id")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "建立日期"),
                    user_id = table.Column<string>(type: "varchar(50)", nullable: false, comment: "使用者帳號"),
                    pwd = table.Column<string>(type: "varchar(50)", nullable: false, comment: "密碼")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "chat_room",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "系統id")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "建立日期"),
                    message = table.Column<string>(type: "text", nullable: false, comment: "訊息"),
                    topic = table.Column<string>(type: "varchar(255)", nullable: false, comment: "主題"),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "Users表的id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "chat_room",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "chat_room",
                table: "users",
                columns: new[] { "id", "create_date", "pwd", "user_id" },
                values: new object[] { 1L, new DateTime(2023, 5, 5, 8, 43, 35, 94, DateTimeKind.Utc).AddTicks(5074), "1234", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_messages_user_id",
                schema: "chat_room",
                table: "messages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_id",
                schema: "chat_room",
                table: "users",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "chat_room");

            migrationBuilder.DropTable(
                name: "users",
                schema: "chat_room");
        }
    }
}
