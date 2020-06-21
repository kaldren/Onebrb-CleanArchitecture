using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onebrb.Infrastructure.Migrations
{
    public partial class AddedMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorUserName = table.Column<string>(nullable: true),
                    RecipientId = table.Column<int>(nullable: false),
                    RecipientUserName = table.Column<string>(nullable: true),
                    IsDeletedForAuthor = table.Column<bool>(nullable: false),
                    IsArchivedForAuthor = table.Column<bool>(nullable: false),
                    IsDeletedForRecipient = table.Column<bool>(nullable: false),
                    IsArchivedForRecipient = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMessages", x => new { x.MessageId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMessages");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
