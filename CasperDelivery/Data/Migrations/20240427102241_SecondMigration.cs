using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasperDelivery.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Address",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Address",
                newName: "ZipCode");
            
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
