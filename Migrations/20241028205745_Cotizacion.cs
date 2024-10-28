using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistrosTecnico.Migrations
{
    /// <inheritdoc />
    public partial class Cotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "WhatsApp",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    CotizacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Clientes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizaciones", x => x.CotizacionId);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_Clientes_Clientes",
                        column: x => x.Clientes,
                        principalTable: "Clientes",
                        principalColumn: "ClientesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CotizacionDetalles",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotizacionId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    ArticulosArticuloId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotizacionDetalles", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_CotizacionDetalles_Articulos_ArticulosArticuloId",
                        column: x => x.ArticulosArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "ArticuloId");
                    table.ForeignKey(
                        name: "FK_CotizacionDetalles_Cotizaciones_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "Cotizaciones",
                        principalColumn: "CotizacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 1,
                columns: new[] { "Costo", "Descripcion" },
                values: new object[] { 100.0, "Cable Lan" });

            migrationBuilder.CreateIndex(
                name: "IX_CotizacionDetalles_ArticulosArticuloId",
                table: "CotizacionDetalles",
                column: "ArticulosArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_CotizacionDetalles_CotizacionId",
                table: "CotizacionDetalles",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_Clientes",
                table: "Cotizaciones",
                column: "Clientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotizacionDetalles");

            migrationBuilder.DropTable(
                name: "Cotizaciones");

            migrationBuilder.AlterColumn<string>(
                name: "WhatsApp",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 1,
                columns: new[] { "Costo", "Descripcion" },
                values: new object[] { 30.0, "Pasta termica" });

            migrationBuilder.InsertData(
                table: "Articulos",
                columns: new[] { "ArticuloId", "Costo", "Descripcion", "Existencia", "Precio" },
                values: new object[,]
                {
                    { 2, 100.0, "Memoria RAM", 10, 150.0 },
                    { 3, 80.0, "Tarjeta grafica", 12, 130.0 }
                });
        }
    }
}
