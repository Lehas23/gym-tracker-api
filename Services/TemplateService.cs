using Microsoft.EntityFrameworkCore;

public class TemplateService
{
    private readonly AppDbContext _context;

    public TemplateService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkoutTemplate>> GetTemplates(int userId)
    {
        return await _context.workoutTemplates
            .Where(t => t.userId == userId)
            .Include(t => t.templateExercises)
                .ThenInclude(te => te.Exercise)
            .ToListAsync();
    }

    public async Task<WorkoutTemplate?> GetTemplateById(int userId, int templateId)
    {
        return await _context.workoutTemplates
            .Where(t => t.userId == userId && t.Id == templateId)
            .Include(t => t.templateExercises)
                .ThenInclude(te => te.Exercise)
            .FirstOrDefaultAsync();
    }

    public async Task<WorkoutTemplate> AddTemplate(int userId, CreateTemplateDTO dto)
    {
        var template = new WorkoutTemplate
        {
            Name = dto.Name,
            userId = userId,
            templateExercises = dto.Exercises.Select(e => new TemplateExercise
            {
                ExerciseId = e.ExerciseId,
                DefaultWeight = e.DefaultWeight,
                DefaultSets = e.DefaultSets,
                DefaultReps = e.DefaultReps
            }).ToList()
        };

        _context.workoutTemplates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<TemplateExercise?> AddExerciseToTemplate(int userId, int templateId, CreateTemplateExerciseDTO dto)
    {
        var template = await _context.workoutTemplates
            .FirstOrDefaultAsync(t => t.Id == templateId && t.userId == userId);

        if (template == null)
            return null;

        var exercise = new TemplateExercise
        {
            TemplateId = templateId,
            ExerciseId = dto.ExerciseId,
            DefaultWeight = dto.DefaultWeight,
            DefaultSets = dto.DefaultSets,
            DefaultReps = dto.DefaultReps
        };
     
        _context.templateExercises.Add(exercise);
        await _context.SaveChangesAsync();
        return exercise;
    }

    public async Task<WorkoutTemplate?> UpdateTemplate(int userId, int id, UpdateTemplateDTO dto)
    {
        var template = await _context.workoutTemplates
            .Include(t => t.templateExercises)
            .FirstOrDefaultAsync(t => t.Id == id && t.userId == userId);

        if (template == null)
            return null;

        template.Name = dto.Name;

        _context.templateExercises.RemoveRange(template.templateExercises);

        template.templateExercises = dto.Exercises.Select(e => new TemplateExercise
        {
            ExerciseId = e.ExerciseId,
            DefaultSets = e.DefaultSets,
            DefaultReps = e.DefaultReps,
            DefaultWeight = e.DefaultWeight
        }).ToList();

        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<WorkoutTemplate?> DeleteTemplate(int userId, int id)
    {
        var template = await _context.workoutTemplates
            .FirstOrDefaultAsync(t => t.Id == id && t.userId == userId);

        if (template == null)
            return null;

        var sessions = _context.workoutSessions
            .Where(s => s.TemplateId == id);

        foreach (var session in sessions)
        {
            session.TemplateId = null;
        }

        await _context.SaveChangesAsync();

        var templateExercises = _context.templateExercises
            .Where(te => te.TemplateId == id);

        _context.templateExercises.RemoveRange(templateExercises);
        await _context.SaveChangesAsync();

        _context.workoutTemplates.Remove(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<TemplateExercise?> DeleteExerciseFromTemplate(int userId, int id, int exerciseId)
    {
        var template = await _context.workoutTemplates
            .FirstOrDefaultAsync(t => t.Id == id && t.userId == userId);

        if (template == null)
            return null;

        var exercise = await _context.templateExercises
            .FirstOrDefaultAsync(e => e.Id == exerciseId && e.TemplateId == id);

        if (exercise == null)
            return null;

        _context.templateExercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return exercise;
    }
}