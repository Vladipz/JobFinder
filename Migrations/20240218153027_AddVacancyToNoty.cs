using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Migrations
{
    /// <inheritdoc />
    public partial class AddVacancyToNoty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_VacancyId",
                table: "Notifications",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Vacancies_VacancyId",
                table: "Notifications",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Vacancies_VacancyId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_VacancyId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Notifications");
        }
    }
}
