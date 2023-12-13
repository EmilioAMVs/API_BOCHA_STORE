using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_BOCHA_STORE.Migrations
{
    /// <inheritdoc />
    public partial class CorrecionMarcas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idMarca",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 1,
                column: "idMarca",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Producto",
                keyColumn: "idProducto",
                keyValue: 2,
                column: "idMarca",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idMarca",
                table: "Producto");
        }
    }
}
