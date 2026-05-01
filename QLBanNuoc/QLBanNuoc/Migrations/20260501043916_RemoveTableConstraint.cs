using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBanNuoc.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TableId_ByType",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TableId_ByType",
                table: "Orders",
                sql: "([OrderType] = N'TaiQuan' AND [TableId] IS NOT NULL) OR ([OrderType] IN (N'MangDi', N'GiaoHang') AND [TableId] IS NULL)");
        }

    }
}
