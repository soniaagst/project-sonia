using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Repositories;

namespace ParkingSystemAPI.Services.Auth;

public class UserApiService
{
    private UserRepository _userRepository;
    private IMapper _mapper;

    public UserApiService(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        List<User> users = await _userRepository.GetUsersAsync();
        List<UserDto> userDtos = [];

        foreach(var user in users)
            userDtos.Add(_mapper.Map<UserDto>(user));

        return userDtos;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user is null) return null;

        return user;
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