using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("682f7013-ff9b-435d-876a-23ae3ec3795e"), "Medium" },
                    { new Guid("7216fc77-5d75-4bf4-b874-64f8fcc2ada8"), "Easy" },
                    { new Guid("99ac3a26-df48-4f01-8f0b-d7954cd2e9e0"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("3be7090d-a168-46ae-a2f1-8ba1fb06407c"), "NTH", "Northland", "https://example.com/northland.jpg" },
                    { new Guid("72c44ed3-7203-4d17-adaf-96c9027a6906"), "WST", "Westland", null },
                    { new Guid("8d98bcb8-6671-4ca6-8712-9267383f903b"), "EST", "Eastland", "https://example.com/eastland.jpg" },
                    { new Guid("fbfed9f1-fdee-4ccc-826e-fcf0799c767a"), "STH", "Southland", "https://example.com/southland.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("682f7013-ff9b-435d-876a-23ae3ec3795e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7216fc77-5d75-4bf4-b874-64f8fcc2ada8"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("99ac3a26-df48-4f01-8f0b-d7954cd2e9e0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3be7090d-a168-46ae-a2f1-8ba1fb06407c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("72c44ed3-7203-4d17-adaf-96c9027a6906"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8d98bcb8-6671-4ca6-8712-9267383f903b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fbfed9f1-fdee-4ccc-826e-fcf0799c767a"));
        }
    }
}
