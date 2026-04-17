public class Set
{
    public int Id { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public int SetNumber { get; set; }
    public int ExerciseId { get; set; }
    public int SessionId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public WorkoutSession Session { get; set; } = null!;
}