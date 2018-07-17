using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CollegeInternshipAPI.Migrations
{
    public partial class longitudelatitudedoubleprecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Jobs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Jobs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Candidates",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Candidates",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Candidates");
        }
    }
}
