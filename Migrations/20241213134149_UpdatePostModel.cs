using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

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
                name: "CommentId",
                table: "Comments");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Comments",
                table: "Posts",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("05bf90b0-9573-44b0-8dc9-5d21e36e501c"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9326), "теория_заговора" },
                    { new Guid("507d093c-2394-4c32-ae33-ecc545adc50d"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9327), "соцсети" },
                    { new Guid("9b5353e8-abef-45f2-bfe3-72439cf0c887"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9279), "еда" },
                    { new Guid("9d61168f-2e6a-4a38-ab4f-630d20aec741"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9325), "интернет" },
                    { new Guid("c91199e0-585c-4dc4-bd75-6e21e8e41f16"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9276), "история" },
                    { new Guid("d44ebbe4-2445-4004-87d6-c589f6e2bfbc"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9324), "it" },
                    { new Guid("d73eee01-eac0-4e17-9dee-f69d2b1b1a45"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9328), "косплей" },
                    { new Guid("e375d91f-d9e2-4066-b31a-72e139d044bd"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9321), "18+" },
                    { new Guid("e4def103-0fa5-4dc2-9ab2-5d1e929a250e"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9329), "преступление" },
                    { new Guid("ff81b283-2e81-4116-a161-6dd501517d14"), new DateTime(2024, 12, 13, 13, 41, 49, 671, DateTimeKind.Utc).AddTicks(9322), "приколы" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("05bf90b0-9573-44b0-8dc9-5d21e36e501c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("507d093c-2394-4c32-ae33-ecc545adc50d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9b5353e8-abef-45f2-bfe3-72439cf0c887"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9d61168f-2e6a-4a38-ab4f-630d20aec741"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c91199e0-585c-4dc4-bd75-6e21e8e41f16"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d44ebbe4-2445-4004-87d6-c589f6e2bfbc"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("d73eee01-eac0-4e17-9dee-f69d2b1b1a45"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e375d91f-d9e2-4066-b31a-72e139d044bd"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e4def103-0fa5-4dc2-9ab2-5d1e929a250e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("ff81b283-2e81-4116-a161-6dd501517d14"));

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentId",
                table: "Comments",
                type: "uuid",
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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId",
                table: "Comments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
