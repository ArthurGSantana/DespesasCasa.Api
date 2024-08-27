using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespesasCasa.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expanse");

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CollectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expense_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CollectionId",
                table: "Expense",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.CreateTable(
                name: "Expanse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Observation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expanse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expanse_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expanse_CollectionId",
                table: "Expanse",
                column: "CollectionId");
        }
    }
}
