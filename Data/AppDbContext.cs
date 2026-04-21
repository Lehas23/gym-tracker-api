using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkoutSession>()
            .HasOne(ws => ws.Template)
            .WithMany()
            .HasForeignKey(ws => ws.TemplateId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<TemplateExercise>()
            .HasOne(te => te.Template)
            .WithMany(t => t.templateExercises)
            .HasForeignKey(te => te.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<WorkoutTemplate> workoutTemplates { get; set; }
    public DbSet<WorkoutSession> workoutSessions { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<TemplateExercise> templateExercises { get; set; }
}

