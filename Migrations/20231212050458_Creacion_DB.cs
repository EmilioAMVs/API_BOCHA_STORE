using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_BOCHA_STORE.Migrations
{
    /// <inheritdoc />
    public partial class Creacion_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    idMarca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.idMarca);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProovedor = table.Column<int>(type: "int", nullable: false),
                    nombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcionProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<double>(type: "float", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.idProducto);
                });

            migrationBuilder.CreateTable(
                name: "Proovedor",
                columns: table => new
                {
                    idProovedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProovedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duracionContrato = table.Column<int>(type: "int", nullable: false),
                    precioImportacion = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proovedor", x => x.idProovedor);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "idMarca", "nombreMarca" },
                values: new object[,]
                {
                    { 1, "Adidas" },
                    { 2, "Nike" }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "idProducto", "descripcionProducto", "fechaCreacion", "idProovedor", "nombreProducto", "precio", "stock" },
                values: new object[,]
                {
                    { 1, "Camiseta Oversized", new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Camiseta Negra", 19.989999999999998, 0 },
                    { 2, "Pantalon Deportivo", new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pantalon Negro", 15.99, 0 }
                });

            migrationBuilder.InsertData(
                table: "Proovedor",
                columns: new[] { "idProovedor", "duracionContrato", "nombreProovedor", "precioImportacion" },
                values: new object[,]
                {
                    { 1, 3, "Fast & Good", 90.0 },
                    { 2, 3, "Import S.A", 70.0 },
                    { 3, 3, "Import Facil", 65.0 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "idUsuario", "password", "username" },
                values: new object[,]
                {
                    { 1, "admin123", "admin" },
                    { 2, "cliente123", "cliente" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Proovedor");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
