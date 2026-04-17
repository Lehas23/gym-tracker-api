public class TemplateExercise
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public int ExerciseId { get; set; }
    public double DefaultWeight { get; set; }
    public int DefaultSets { get; set; }
    public int DefaultReps { get; set; }
    public WorkoutTemplate Template { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
