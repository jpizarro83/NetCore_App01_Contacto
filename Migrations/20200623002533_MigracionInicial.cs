﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contactos.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    fNacimiento = table.Column<DateTime>(nullable: true),
                    mensaje = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");
        }
    }
}
