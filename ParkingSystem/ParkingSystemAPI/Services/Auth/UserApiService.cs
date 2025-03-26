using AutoMapper;
using ParkingSystemAPI.DTOs;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Services;

namespace ParkingSystemAPI.Services.Auth;

public class UserApiService
{
    private UserService _userService;
    private IMapper _mapper;

    public UserApiService(UserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        List<User> users = await _userService.GetUsers();
        List<UserDto> userDtos = [];

        foreach(var user in users)
            userDtos.Add(_mapper.Map<UserDto>(user));

        return userDtos;
    }

    public async Task<UserDto?> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsername(username);

        if (user is null) return null;
        return _mapper.Map<UserDto>(user);
    }

    public async Task AddUser(string username, string password)
    {
        await _userService.AddUser(username, password);
    }

    public async Task<bool> RemoveUser(string username, string password)
    {
        return await _userService.RemoveUser(username, password);
    }
}