public class WorkoutTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int userId { get; set; }
    public List<TemplateExercise> templateExercises { get; set; } = new();
}