using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ExerciseService
{
    private readonly AppDbContext _context;

    public ExerciseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExerciseDTO>> GetExercises(string? search)
    {
        var query = _context.Exercises.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(e => e.Name.Contains(search));
        }

        return await query
            .Select(e => new ExerciseDTO
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup
            })
            .ToListAsync();
    }


}