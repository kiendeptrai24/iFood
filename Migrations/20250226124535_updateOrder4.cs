using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iFood.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MomoInfos_Orders_OrderId",
                table: "MomoInfos");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "MomoInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "MomoInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MomoInfos_Orders_OrderId",
                table: "MomoInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MomoInfos_Orders_OrderId",
                table: "MomoInfos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "MomoInfos");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "MomoInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MomoInfos_Orders_OrderId",
                table: "MomoInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
