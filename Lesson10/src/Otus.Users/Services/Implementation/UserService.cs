using Microsoft.EntityFrameworkCore;
using Otus.Users.Models;

namespace Otus.Users.Services.Implementation;

public class UserService : IUserService
{
    private readonly UserDbContext _dbContext;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext"></param>
    public UserService(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<User> CreateAsync(User item)
    {
        var user = (await _dbContext.Users.AddAsync(item)).Entity;
        await _dbContext.SaveChangesAsync();

        return user;
    }
    
    /// <inheritdoc />
    public async Task<User> GetAsync(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new KeyNotFoundException();
        }

        return user;
    }
    
    /// <inheritdoc />
    public async Task UpdateAsync(User item)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(g => g.Id == item.Id);
        
        if (user == null)
        {
            throw new KeyNotFoundException();
        }

        user.LastName = item.LastName;
        user.FirstName = item.FirstName;
        user.Email = item.Email;
        user.Phone = item.Phone;
        user.UserName = item.UserName;

        _dbContext.Update(user);

        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoveAsync(long id)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null)
        {
            throw new KeyNotFoundException();
        }

        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}