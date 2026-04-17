public class User
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public List<WorkoutTemplate> workoutTemplates { get; set; } = new();
    public List<WorkoutSession> workoutSessions { get; set; } = new();
}
