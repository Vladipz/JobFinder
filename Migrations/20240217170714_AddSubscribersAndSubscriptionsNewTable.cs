using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscribersAndSubscriptionsNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_SubscribersId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_AspNetUsers_SubscriptionsId",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser");

            migrationBuilder.RenameTable(
                name: "UserUser",
                newName: "UserSubscriptions");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_SubscriptionsId",
                table: "UserSubscriptions",
                newName: "IX_UserSubscriptions_SubscriptionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions",
                columns: new[] { "SubscribersId", "SubscriptionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_SubscribersId",
                table: "UserSubscriptions",
                column: "SubscribersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_SubscriptionsId",
                table: "UserSubscriptions",
                column: "SubscriptionsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_SubscribersId",
                table: "UserSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubscriptions_AspNetUsers_SubscriptionsId",
                table: "UserSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscriptions",
                table: "UserSubscriptions");

            migrationBuilder.RenameTable(
                name: "UserSubscriptions",
                newName: "UserUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubscriptions_SubscriptionsId",
                table: "UserUser",
                newName: "IX_UserUser_SubscriptionsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser",
                columns: new[] { "SubscribersId", "SubscriptionsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_SubscribersId",
                table: "UserUser",
                column: "SubscribersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_AspNetUsers_SubscriptionsId",
                table: "UserUser",
                column: "SubscriptionsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
