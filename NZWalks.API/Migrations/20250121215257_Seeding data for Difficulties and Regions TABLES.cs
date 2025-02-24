using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegionsTABLES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("16ce75fa-b98a-44a8-b3ff-d8c121848be7"), "Easy" },
                    { new Guid("375f6c9b-7659-4612-aa80-fbfe65d0a6db"), "Medium" },
                    { new Guid("d7b1a4b0-6b1f-4c63-844d-44651e6252d8"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2bad346f-5492-40a4-9b4e-c4ae34707f88"), "STL", "Southland", null },
                    { new Guid("5853099e-7e9b-45fd-b396-b73c33f89ef9"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("5ec4bb99-7d90-435f-bedb-0cebfaa812ca"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("617447e0-f0b9-4766-a477-dcf700eda8cf"), "NTL", "Northland", null },
                    { new Guid("68b5d2a4-4eb6-4564-80fb-6a25556ffd90"), "BOP", "Bay Of Plenty", null },
                    { new Guid("6a0c23b0-f9b3-4b78-ba3c-8b1ab61eee3b"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("16ce75fa-b98a-44a8-b3ff-d8c121848be7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("375f6c9b-7659-4612-aa80-fbfe65d0a6db"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d7b1a4b0-6b1f-4c63-844d-44651e6252d8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2bad346f-5492-40a4-9b4e-c4ae34707f88"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5853099e-7e9b-45fd-b396-b73c33f89ef9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5ec4bb99-7d90-435f-bedb-0cebfaa812ca"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("617447e0-f0b9-4766-a477-dcf700eda8cf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("68b5d2a4-4eb6-4564-80fb-6a25556ffd90"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6a0c23b0-f9b3-4b78-ba3c-8b1ab61eee3b"));
        }
    }
}
