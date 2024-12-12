using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPostsAndCommentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ReadingTime = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    CommunityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CommunityName = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    Likes = table.Column<int>(type: "integer", nullable: false),
                    CommentsCount = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    SubComments = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

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
    }
}
