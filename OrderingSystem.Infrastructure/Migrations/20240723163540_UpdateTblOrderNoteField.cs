using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTblOrderNoteField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tbl_menu_group_name",
                table: "tbl_menu_group");

            migrationBuilder.DropIndex(
                name: "ix_tbl_menu_name",
                table: "tbl_menu");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "tbl_order",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_menu_group_name",
                table: "tbl_menu_group",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_menu_name",
                table: "tbl_menu",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tbl_menu_group_name",
                table: "tbl_menu_group");

            migrationBuilder.DropIndex(
                name: "ix_tbl_menu_name",
                table: "tbl_menu");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "tbl_order",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_menu_group_name",
                table: "tbl_menu_group",
                column: "name",
                unique: true,
                filter: "is_deleted = false");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_menu_name",
                table: "tbl_menu",
                column: "name",
                unique: true,
                filter: "is_deleted = false");
        }
    }
}
