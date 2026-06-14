public interface ISessionService
{
    Task<SessionResponseDTO?> GetSessionById(int userId, int sessionId);
    Task<List<SessionResponseDTO>> GetLast10Sessions(int userId);
    Task<int> GetSessionCount(int userId);
    Task<WorkoutSession?> CreateSession(int userId, int templateId);
    Task<Set?> AddSetDuringSession(int userId, int sessionId, AddSetDTO dto);
    Task<Set?> UpdateSetDuringSession(int userId, int sessionId, int setId, UpdateSetDTO dto);
    Task<Set?> DeleteSetDuringSession(int userId, int sessionId, int setId);
}