using Microsoft.EntityFrameworkCore;

public class SessionService
{
    private readonly AppDbContext _context;

    public SessionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SessionResponseDTO>> GetSessions(int userId)
    {
        var sessions = await _context.workoutSessions
            .Where(s => s.UserId == userId)
            .Include(s => s.Sets)
                .ThenInclude(s => s.Exercise)
            .Include(s => s.Template)
            .OrderByDescending(s => s.Date)
            .Take(10)
            .ToListAsync();

        return sessions.Select(s => new SessionResponseDTO
        {
            Id = s.Id,
            Date = s.Date,
            TemplateName = s.Template.Name,
            Exercises = s.Sets
                .GroupBy(set => set.Exercise.Name)
                .Select(group => new ExerciseResponseDTO
                {
                    ExerciseName = group.Key,
                    Sets = group.Select(set => new SetResponseDTO
                    {
                        Id = set.Id,
                        SetNumber = set.SetNumber,
                        Reps = set.Reps,
                        Weight = set.Weight
                    }).ToList()
                }).ToList()
        }).ToList();
    } 

    public async Task<WorkoutSession?> CreateSession(int userId, int templateId)
    {
        var template = await _context.workoutTemplates
            .Include(t => t.templateExercises)
            .FirstOrDefaultAsync(t => t.Id == templateId && t.userId == userId);

        if (template == null)
            return null;

        var session = new WorkoutSession
        {
            TemplateId = templateId,
            UserId = userId,
            Date = DateOnly.FromDateTime(DateTime.Now)
        };

        var sets = new List<Set>();

        foreach (var te in template.templateExercises)
        {
            for (int i = 1; i <= te.DefaultSets; i++)
            {
                sets.Add(new Set
                {
                    ExerciseId = te.ExerciseId,
                    Reps = te.DefaultReps,
                    Weight = te.DefaultWeight,
                    SetNumber = i
                });
            }
        }
        session.Sets = sets;

        _context.workoutSessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }
}