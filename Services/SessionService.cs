using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Text.RegularExpressions;

public class SessionService
{
    private readonly AppDbContext _context;

    public SessionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SessionResponseDTO?> GetSessionById(int userId, int sessionId)
    {
        var session = await _context.workoutSessions
            .Include(s => s.Sets)
                .ThenInclude(s => s.Exercise)
            .Include(s => s.Template)
            .FirstOrDefaultAsync(s => s.UserId == userId && s.Id == sessionId);

        if (session == null)
            return null;

        return new SessionResponseDTO
        {
            Id = session.Id,
            Date = session.Date,
            TemplateName = session.Template?.Name ?? "Unknown Template",
            Exercises = session.Sets
                .GroupBy(set => set.Exercise.Name)
                .Select(group => new ExerciseResponseDTO
                {
                    ExerciseName = group.Key,
                    ExerciseId = group.First().ExerciseId,
                    Sets = group.Select(set => new SetResponseDTO
                    {
                        Id = set.Id,
                        SetNumber = set.SetNumber,
                        Reps = set.Reps,
                        Weight = set.Weight
                    }).ToList()
                }).ToList()
        };   
    }
    public async Task<List<SessionResponseDTO>> GetLast10Sessions(int userId)
    {
        var sessions = await _context.workoutSessions
            .Where(s => s.UserId == userId)
            .Include(s => s.Sets)
                .ThenInclude(s => s.Exercise)
            .Include(s => s.Template)
            .OrderByDescending(s => s.Id)
            .Take(10)
            .ToListAsync();

        return sessions.Select(s => new SessionResponseDTO
        {
            Id = s.Id,
            Date = s.Date,
            TemplateName = s.Template?.Name ?? "Unknown Template",
            Exercises = s.Sets
                .GroupBy(set => set.Exercise.Name)
                .Select(group => new ExerciseResponseDTO
                {
                    ExerciseName = group.Key,
                    ExerciseId = group.First().ExerciseId,
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

    public async Task<int> GetSessionCount(int userId)
    {
        return await _context.workoutSessions
            .Where(u => u.UserId == userId)
            .CountAsync();
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

    public async Task<Set?> AddSetDuringSession(int userId, int sessionId, AddSetDTO dto)
    {
        var session = await _context.workoutSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

        if (session == null)
            return null;

        var set = new Set
        {
            ExerciseId = dto.ExerciseId,
            Reps = dto.Reps,
            Weight = dto.Weight,
            SetNumber = dto.SetNumber,
            SessionId = sessionId
        };

        _context.Add(set);
        await _context.SaveChangesAsync();
        return set;
    }

    public async Task<Set?> UpdateSetDuringSession(int userId, int sessionId, int setId, UpdateSetDTO dto)
    {
        var session = await _context.workoutSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

        if (session == null)
            return null;

        var set = await _context.Sets
             .FirstOrDefaultAsync(s => s.Id == setId && s.SessionId == sessionId);

        if (set == null)
            return null;

        set.Reps = dto.Reps;
        set.Weight = dto.Weight;
        set.SetNumber = dto.SetNumber;

        await _context.SaveChangesAsync();
        return set;
    }

    public async Task<Set?> DeleteSetDuringSession(int userId, int sessionId, int setId)
    {
        var session = await _context.workoutSessions
            .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

        if (session == null)
            return null;

        var set = await _context.Sets
            .FirstOrDefaultAsync(s => s.Id == setId && s.SessionId == sessionId);

        if (set == null)
            return null;

        _context.Sets.Remove(set);
        await _context.SaveChangesAsync();
        return set;
    }
}