using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemLibrary.Services;
public class UserService
{
    private UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.GetUsersAsync();
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<User> AddUser(string username, string password)
    {
        User user = new(username, password);

        await _userRepository.AddUserAsync(user);

        return user;
    }

    public async Task<bool> RemoveUser(string username, string password)
    {
        var user = await GetUserByUsername(username);
        if (user is not null && user.Password == password)
        {
            await _userRepository.RemoveUserAsync(user);
            return true;
        }
        return false;
    }
}