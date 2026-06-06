using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SockerServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMicragtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuisnessFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuisnessFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsOnRedList = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    BuisnessFieldId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true),
                    TeachingAssistant_PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_BuisnessFields_BuisnessFieldId",
                        column: x => x.BuisnessFieldId,
                        principalTable: "BuisnessFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BuisnessFields",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Informatique" },
                    { 2, "Sante" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Etudiant" },
                    { 2, "Professeur" },
                    { 3, "Auxiliaire d’enseignement" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BuisnessFieldId", "CategoryId", "EmailAddress", "FirstName", "IsOnRedList", "LastName", "Password", "ReferenceNumber" },
                values: new object[,]
                {
                    { 1, 1, 1, "AAA", "AAA", false, "BBB", "1234", "AAAA" },
                    { 2, 1, 1, "AA", "CCC", true, "BBB", "1234", "AAAA" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BuisnessFieldId", "CategoryId", "EmailAddress", "FirstName", "IsOnRedList", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 3, 1, 2, "AAA", "AAA", false, "BBB", "1234", "AAAA" },
                    { 4, 1, 2, "AA", "CCC", true, "BBB", "1234", "AAAA" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BuisnessFieldId", "CategoryId", "EmailAddress", "FirstName", "IsOnRedList", "LastName", "Password", "TeachingAssistant_PhoneNumber" },
                values: new object[,]
                {
                    { 5, 1, 3, "AAA", "AAA", false, "BBB", "1234", "AAAA" },
                    { 6, 1, 3, "AA", "CCC", true, "BBB", "1234", "AAAA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_BuisnessFieldId",
                table: "People",
                column: "BuisnessFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_People_CategoryId",
                table: "People",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "BuisnessFields");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
