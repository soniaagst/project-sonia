using Microsoft.EntityFrameworkCore;
using ParkingSystemLibrary.Data;
using ParkingSystemLibrary.Models;

namespace ParkingSystemLibrary.Repositories;

public class UserRepository : IUserRepository
{
    private ParkingDb _parkingDb;

    public UserRepository(ParkingDb parkingDb)
    {
        _parkingDb = parkingDb;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _parkingDb.Users.ToListAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _parkingDb.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    public async Task AddUserAsync(User user)
    {
        _parkingDb.Users.Add(user);
        await _parkingDb.SaveChangesAsync();
    }

    public async Task RemoveUserAsync(User user)
    {
        _parkingDb.Users.Remove(user);
        await _parkingDb.SaveChangesAsync();
    }
}