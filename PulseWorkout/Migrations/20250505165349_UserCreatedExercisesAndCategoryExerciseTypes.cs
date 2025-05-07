using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseWorkout.Migrations
{
    /// <inheritdoc />
    public partial class UserCreatedExercisesAndCategoryExerciseTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Exercises",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Exercises",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId1",
                table: "Exercises",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserCreated",
                table: "Exercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseType",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CreatedByUserId1",
                table: "Exercises",
                column: "CreatedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_CreatedByUserId1",
                table: "Exercises",
                column: "CreatedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_CreatedByUserId1",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_CreatedByUserId1",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId1",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "IsUserCreated",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseType",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "IconUrl",
                table: "Exercises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
