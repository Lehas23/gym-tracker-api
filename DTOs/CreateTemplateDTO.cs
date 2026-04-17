using System.ComponentModel.DataAnnotations;

public class CreateTemplateDTO
{
    [Required]
    public string Name { get; set; } = null!;
    public List<CreateTemplateExerciseDTO> Exercises { get; set; } = new();
}

public class CreateTemplateExerciseDTO
{
    public int ExerciseId { get; set; }
    public double DefaultWeight { get; set; }
    public int DefaultSets { get; set; }
    public int DefaultReps { get; set; }
}
