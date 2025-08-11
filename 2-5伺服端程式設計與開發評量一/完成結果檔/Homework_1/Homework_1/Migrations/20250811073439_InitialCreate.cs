using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework_1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainContent",
                columns: table => new
                {
                    MainID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    MTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MPhoto = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MPhotoType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NAuthor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainID", x => x.MainID);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    ResponseID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    RContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RAuthor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MainID = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_Response_MainContent_MainID",
                        column: x => x.MainID,
                        principalTable: "MainContent",
                        principalColumn: "MainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Response_MainID",
                table: "Response",
                column: "MainID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "MainContent");
        }
    }
}
