using Microsoft.EntityFrameworkCore.Migrations;

namespace DojoMeetUP.Migrations
{
    public partial class NewClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Meet_EventId",
                table: "Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Users_JoinedUserId",
                table: "Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Meet_Users_UserId",
                table: "Meet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meet",
                table: "Meet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Meet",
                newName: "Meets");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_Meet_UserId",
                table: "Meets",
                newName: "IX_Meets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_JoinedUserId",
                table: "Guests",
                newName: "IX_Guests_JoinedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_EventId",
                table: "Guests",
                newName: "IX_Guests_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meets",
                table: "Meets",
                column: "MeetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Meets_EventId",
                table: "Guests",
                column: "EventId",
                principalTable: "Meets",
                principalColumn: "MeetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Users_JoinedUserId",
                table: "Guests",
                column: "JoinedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meets_Users_UserId",
                table: "Meets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Meets_EventId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Users_JoinedUserId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Meets_Users_UserId",
                table: "Meets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meets",
                table: "Meets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.RenameTable(
                name: "Meets",
                newName: "Meet");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Meets_UserId",
                table: "Meet",
                newName: "IX_Meet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_JoinedUserId",
                table: "Guest",
                newName: "IX_Guest_JoinedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_EventId",
                table: "Guest",
                newName: "IX_Guest_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meet",
                table: "Meet",
                column: "MeetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Meet_EventId",
                table: "Guest",
                column: "EventId",
                principalTable: "Meet",
                principalColumn: "MeetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Users_JoinedUserId",
                table: "Guest",
                column: "JoinedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meet_Users_UserId",
                table: "Meet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
