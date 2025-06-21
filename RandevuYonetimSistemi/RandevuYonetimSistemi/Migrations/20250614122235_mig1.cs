using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandevuYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Aktiflik = table.Column<bool>(type: "bit", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Tc = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unvanı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Aktiflik = table.Column<bool>(type: "bit", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Tc = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SickPeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CezaDurumu = table.Column<bool>(type: "bit", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Tc = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickPeople", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HastaId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    RandevuGirisSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_SickPeople_HastaId",
                        column: x => x.HastaId,
                        principalTable: "SickPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Sifre" },
                values: new object[] { 1, "admin@gmail.com", "admin123" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Adi", "Aktiflik", "Email", "Sifre", "Soyadi", "Tc", "Telefon", "Unvan" },
                values: new object[] { 1, "Hamza", false, "hamza@gmail.com", "123456", "Çakmakçi", "12345678911", "05553654718", "dk." });

            migrationBuilder.InsertData(
                table: "SickPeople",
                columns: new[] { "Id", "Adi", "CezaDurumu", "Email", "Soyadi", "Tc", "Telefon" },
                values: new object[] { 1, "fırat", false, "fırat@gmail.com", "Eren", "12456789123", "05336649874" });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "DoctorId", "HastaId", "RandevuGirisSaati", "RandevuTarihi" },
                values: new object[] { 1, 1, 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HastaId",
                table: "Appointments",
                column: "HastaId");

            migrationBuilder.CreateIndex(
                name: "Index_Tc_Unique",
                table: "SickPeople",
                column: "Tc",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Employes");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "SickPeople");
        }
    }
}
