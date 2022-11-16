using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Curso.ComercioElectronico.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ClienteAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Clientes_ClienteId",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId1",
                table: "Ordenes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Ordenes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Clientes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Clientes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Clientes",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteId1",
                table: "Ordenes",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Clientes_ClienteId1",
                table: "Ordenes",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Clientes_ClienteId1",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_ClienteId1",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Clientes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ClienteId",
                table: "Ordenes",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Clientes_ClienteId",
                table: "Ordenes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
