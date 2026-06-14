public interface ITemplateService
{
    Task<List<WorkoutTemplate>> GetTemplates(int userId);
    Task<WorkoutTemplate?> GetTemplateById(int userId, int templateId);
    Task<WorkoutTemplate> AddTemplate(int userId, CreateTemplateDTO dto);
    Task<TemplateExercise?> AddExerciseToTemplate(int userId, int templateId, CreateTemplateExerciseDTO dto);
    Task<WorkoutTemplate?> UpdateTemplate(int userId, int id, UpdateTemplateDTO dto);
    Task<WorkoutTemplate?> DeleteTemplate(int userId, int id);
    Task<TemplateExercise?> DeleteExerciseFromTemplate(int userId, int id, int exerciseId);
}