using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("104f3fa5-91e6-4ec8-a6df-e3feb4f91c15"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("28fdc22d-b086-48f3-b2b9-2f8ccd8b59d5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2fe8d803-0e70-4495-9f02-e9f349f4916f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("83dc1c3a-3a7a-4208-b365-d443016a90ff"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a448c084-3edd-4e6f-8094-b9d7e3b42ae2"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b67f0e4b-4c01-4c4e-80db-c279bb195aa0"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("bf45aa62-c243-44e5-8205-84c0b34a3534"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("cb712a1f-8f6b-4167-9c4b-7afcc9f0a3a7"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e39cd64e-7fe8-4bae-9ffd-9868beeb8dd5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ee2f915c-b7ce-4eb3-965a-87b4695fd790"));

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Likes",
                table: "Users",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Posts",
                table: "Users",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("17fd6c73-254a-44f9-8f56-4a622265f14a"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4854), "косплей" },
                    { new Guid("1e5242c6-2ee8-491c-adab-9b3ebe8cbdda"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4855), "преступление" },
                    { new Guid("2a5957e7-debc-4b4a-a027-786d0ea1594e"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4835), "еда" },
                    { new Guid("6980ec58-cd35-4ebf-8e50-16ba83de0efe"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4852), "соцсети" },
                    { new Guid("6cce1ee5-b116-4276-b0e9-cbf2a64b928b"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4832), "история" },
                    { new Guid("a1175ed4-7923-40ac-a0a8-c1a0ec5631b4"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4840), "интернет" },
                    { new Guid("bcd2c859-398a-4ce5-ba3e-156af3f5bb0e"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4839), "it" },
                    { new Guid("c1c763bc-b752-4943-81cb-4a3e4d485745"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4836), "18+" },
                    { new Guid("e9e5a0b8-8684-4344-a72a-195f501aeeeb"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4841), "теория_заговора" },
                    { new Guid("f66851c3-7c17-4ce0-9493-906ce404572f"), new DateTime(2024, 12, 13, 8, 26, 58, 189, DateTimeKind.Utc).AddTicks(4837), "приколы" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("17fd6c73-254a-44f9-8f56-4a622265f14a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1e5242c6-2ee8-491c-adab-9b3ebe8cbdda"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2a5957e7-debc-4b4a-a027-786d0ea1594e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6980ec58-cd35-4ebf-8e50-16ba83de0efe"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6cce1ee5-b116-4276-b0e9-cbf2a64b928b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a1175ed4-7923-40ac-a0a8-c1a0ec5631b4"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("bcd2c859-398a-4ce5-ba3e-156af3f5bb0e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c1c763bc-b752-4943-81cb-4a3e4d485745"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e9e5a0b8-8684-4344-a72a-195f501aeeeb"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f66851c3-7c17-4ce0-9493-906ce404572f"));

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Posts",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("104f3fa5-91e6-4ec8-a6df-e3feb4f91c15"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2169), "история" },
                    { new Guid("28fdc22d-b086-48f3-b2b9-2f8ccd8b59d5"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2172), "еда" },
                    { new Guid("2fe8d803-0e70-4495-9f02-e9f349f4916f"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2173), "18+" },
                    { new Guid("83dc1c3a-3a7a-4208-b365-d443016a90ff"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2176), "it" },
                    { new Guid("a448c084-3edd-4e6f-8094-b9d7e3b42ae2"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2188), "соцсети" },
                    { new Guid("b67f0e4b-4c01-4c4e-80db-c279bb195aa0"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2190), "косплей" },
                    { new Guid("bf45aa62-c243-44e5-8205-84c0b34a3534"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2187), "теория_заговора" },
                    { new Guid("cb712a1f-8f6b-4167-9c4b-7afcc9f0a3a7"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2191), "преступление" },
                    { new Guid("e39cd64e-7fe8-4bae-9ffd-9868beeb8dd5"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2175), "приколы" },
                    { new Guid("ee2f915c-b7ce-4eb3-965a-87b4695fd790"), new DateTime(2024, 12, 12, 14, 21, 29, 775, DateTimeKind.Utc).AddTicks(2186), "интернет" }
                });
        }
    }
}
