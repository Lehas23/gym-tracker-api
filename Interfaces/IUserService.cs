public interface IUserService
{
    Task<UserResponseDTO?> UserRegister(UserRegisterDTO registeDTO);
    Task<LoginResponseDTO?> UserLogin(UserLoginDTO loginDTO);
    Task<User?> UpdateUser(int userId, string name);
    Task<bool> UpdatePassword(int userId, string currentPassword, string newPassword);
    Task<User?> DeleteUser(int userId);
}