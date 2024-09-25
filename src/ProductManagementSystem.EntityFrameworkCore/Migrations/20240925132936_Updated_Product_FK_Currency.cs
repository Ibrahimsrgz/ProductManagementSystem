using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Product_FK_Currency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "AppProduct",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProduct_CurrencyId",
                table: "AppProduct",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProduct_AppCurrency_CurrencyId",
                table: "AppProduct",
                column: "CurrencyId",
                principalTable: "AppCurrency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProduct_AppCurrency_CurrencyId",
                table: "AppProduct");

            migrationBuilder.DropIndex(
                name: "IX_AppProduct_CurrencyId",
                table: "AppProduct");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "AppProduct");
        }
    }
}
