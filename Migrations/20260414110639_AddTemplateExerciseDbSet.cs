using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateExerciseDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateExercise_Exercises_ExerciseId",
                table: "TemplateExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateExercise_workoutTemplates_TemplateId",
                table: "TemplateExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateExercise",
                table: "TemplateExercise");

            migrationBuilder.RenameTable(
                name: "TemplateExercise",
                newName: "templateExercises");

            migrationBuilder.RenameIndex(
                name: "IX_TemplateExercise_TemplateId",
                table: "templateExercises",
                newName: "IX_templateExercises_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_TemplateExercise_ExerciseId",
                table: "templateExercises",
                newName: "IX_templateExercises_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_templateExercises",
                table: "templateExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_templateExercises_Exercises_ExerciseId",
                table: "templateExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_templateExercises_workoutTemplates_TemplateId",
                table: "templateExercises",
                column: "TemplateId",
                principalTable: "workoutTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_templateExercises_Exercises_ExerciseId",
                table: "templateExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_templateExercises_workoutTemplates_TemplateId",
                table: "templateExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_templateExercises",
                table: "templateExercises");

            migrationBuilder.RenameTable(
                name: "templateExercises",
                newName: "TemplateExercise");

            migrationBuilder.RenameIndex(
                name: "IX_templateExercises_TemplateId",
                table: "TemplateExercise",
                newName: "IX_TemplateExercise_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_templateExercises_ExerciseId",
                table: "TemplateExercise",
                newName: "IX_TemplateExercise_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateExercise",
                table: "TemplateExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateExercise_Exercises_ExerciseId",
                table: "TemplateExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateExercise_workoutTemplates_TemplateId",
                table: "TemplateExercise",
                column: "TemplateId",
                principalTable: "workoutTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
