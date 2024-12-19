using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedTagsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("08223cb9-46a6-4c83-a1c5-058d843ffe35"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7654), "преступление" },
                    { new Guid("18dfd1a8-8211-4a8a-8914-3bf99a177dda"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7653), "косплей" },
                    { new Guid("1bd54b9b-aacb-4f88-bc37-898306a58109"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7638), "it" },
                    { new Guid("1bf56b60-74ff-4306-99ef-630350cf0522"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7640), "интернет" },
                    { new Guid("2b8d96d9-2096-4f6a-934e-2ad3f363e1d8"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7637), "приколы" },
                    { new Guid("80cd7185-ee19-4fa9-9e4f-17097e849a98"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7651), "теория_заговора" },
                    { new Guid("9a0be9d0-2d82-492a-bb8c-7a526725365b"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7636), "18+" },
                    { new Guid("b4042001-5d95-40f7-8b67-79d551169044"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7652), "соцсети" },
                    { new Guid("c44998c2-5e85-460c-9a9f-2664fa273bcf"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7631), "история" },
                    { new Guid("e2c4cbaf-7741-4e48-9513-fcb67accd901"), new DateTime(2024, 12, 12, 10, 2, 3, 401, DateTimeKind.Utc).AddTicks(7635), "еда" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("08223cb9-46a6-4c83-a1c5-058d843ffe35"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("18dfd1a8-8211-4a8a-8914-3bf99a177dda"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1bd54b9b-aacb-4f88-bc37-898306a58109"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1bf56b60-74ff-4306-99ef-630350cf0522"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2b8d96d9-2096-4f6a-934e-2ad3f363e1d8"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("80cd7185-ee19-4fa9-9e4f-17097e849a98"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9a0be9d0-2d82-492a-bb8c-7a526725365b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b4042001-5d95-40f7-8b67-79d551169044"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c44998c2-5e85-460c-9a9f-2664fa273bcf"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e2c4cbaf-7741-4e48-9513-fcb67accd901"));
        }
    }
}
