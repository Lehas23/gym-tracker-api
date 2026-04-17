public class ExerciseResponseDTO
{
    public string ExerciseName { get; set; } = null!;
    public List<SetResponseDTO> Sets { get; set; } = new();
}