public interface IExerciseService
{
    Task<List<ExerciseDTO>> GetExercises(string? search);
}