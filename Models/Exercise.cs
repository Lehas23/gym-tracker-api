public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string MuscleGroup { get; set; } = null!;
    public List<Set> Sets { get; set; } = new();
}