public class SessionResponseDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string TemplateName { get; set; } = null!;
    public List<ExerciseResponseDTO> Exercises { get; set; } = new();
}