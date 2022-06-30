using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Migrations
{
    public partial class AuditEnable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "task_item",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "task_item",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "task_item",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "task_item",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "region",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "region",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "region",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "region",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "location",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "location",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "location",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "location",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "job_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "job_history",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "job_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "job_history",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "job",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "job",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "job",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "job",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "employee",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "employee",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "department",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "department",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "country",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "country",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "task_item");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "task_item");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "task_item");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "task_item");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "region");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "region");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "region");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "region");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "location");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "location");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "location");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "location");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "job_history");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "job_history");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "job_history");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "job_history");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "job");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "job");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "job");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "job");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "department");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "department");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "department");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "department");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "country");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "country");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "country");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "country");
        }
    }
}
