using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SagaStateMachineWorkerService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStateInstance",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Expiration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Cvv = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStateInstance", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStateInstance");
        }
    }
}
