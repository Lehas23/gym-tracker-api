using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateExercises : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    DefaultWeight = table.Column<double>(type: "float", nullable: false),
                    DefaultSets = table.Column<int>(type: "int", nullable: false),
                    DefaultReps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateExercise_workoutTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "workoutTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateExercise_ExerciseId",
                table: "TemplateExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateExercise_TemplateId",
                table: "TemplateExercise",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateExercise");
        }
    }
}
