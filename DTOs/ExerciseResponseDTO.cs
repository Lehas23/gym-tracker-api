public class ExerciseResponseDTO
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } = null!;
    public List<SetResponseDTO> Sets { get; set; } = new();
}