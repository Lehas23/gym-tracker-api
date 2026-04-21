public class WorkoutSession
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int? TemplateId { get; set; }
    public int UserId { get; set; }
    public List<Set> Sets { get; set; } = new();
    public WorkoutTemplate? Template { get; set; }
}