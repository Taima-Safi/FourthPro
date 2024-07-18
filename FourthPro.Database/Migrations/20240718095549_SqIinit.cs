using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FourthPro.Database.Migrations
{
    /// <inheritdoc />
    public partial class SqIinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_FifthProjectId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_FourthProjectId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_ProjectModelId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ProjectModelId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_FifthProjectId",
                table: "User",
                column: "FifthProjectId",
                principalTable: "Project",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_FourthProjectId",
                table: "User",
                column: "FourthProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_FifthProjectId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Project_FourthProjectId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "ProjectModelId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ProjectModelId",
                table: "User",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_FifthProjectId",
                table: "User",
                column: "FifthProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_FourthProjectId",
                table: "User",
                column: "FourthProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Project_ProjectModelId",
                table: "User",
                column: "ProjectModelId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
