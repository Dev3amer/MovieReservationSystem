using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReservationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakePeopleImageRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "People",
                type: "NVARCHAR(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "People",
                type: "NVARCHAR(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(max)");
        }
    }
}
