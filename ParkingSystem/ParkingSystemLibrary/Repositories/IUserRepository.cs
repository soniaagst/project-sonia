using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Repositories;

public interface IUserRepository
{
    public Task<List<User>> GetUsersAsync();
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task AddUserAsync(User user);
    public Task RemoveUserAsync(User user);
}