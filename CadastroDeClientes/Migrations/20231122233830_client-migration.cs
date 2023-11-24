using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeClientes.Migrations
{
    /// <inheritdoc />
    public partial class clientmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Clients_ClientModelId",
                        column: x => x.ClientModelId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Clients_ClientModelId",
                        column: x => x.ClientModelId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DDD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Clients_ClientModelId",
                        column: x => x.ClientModelId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlternativeEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailModelId = table.Column<long>(type: "bigint", nullable: false),
                    AlternativeEmailModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternativeEmails_AlternativeEmails_AlternativeEmailModelId",
                        column: x => x.AlternativeEmailModelId,
                        principalTable: "AlternativeEmails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlternativeEmails_Emails_EmailModelId",
                        column: x => x.EmailModelId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ClientModelId",
                table: "Addresses",
                column: "ClientModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeEmails_AlternativeEmailModelId",
                table: "AlternativeEmails",
                column: "AlternativeEmailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeEmails_EmailModelId",
                table: "AlternativeEmails",
                column: "EmailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ClientModelId",
                table: "Emails",
                column: "ClientModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ClientModelId",
                table: "Phones",
                column: "ClientModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AlternativeEmails");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
