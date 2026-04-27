public class UpdateTemplateDTO
{
    public string Name { get; set; } = null!;
    public List<CreateTemplateExerciseDTO> Exercises { get; set; } = new();
}