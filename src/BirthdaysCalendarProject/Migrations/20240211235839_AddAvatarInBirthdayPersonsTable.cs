using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirthdaysCalendarProject.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarInBirthdayPersonsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "BirthdayPersons",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "BirthdayPersons");
        }
    }
}
