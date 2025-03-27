using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services.Auth;

public class UserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user is null) return null;

        return user;
    }

    public async Task AddUserAsync(string username, string password)
    {
        User user = new(username, password);

        await _userRepository.AddUserAsync(user);
    }

    public async Task<bool> RemoveUserAsync(string username, string password)
    {
        var user = await GetUserByUsernameAsync(username);

        if (user is not null && user.Password == password)
        {
            await _userRepository.RemoveUserAsync(user);
            return true;
        }
        return false;
    }
}