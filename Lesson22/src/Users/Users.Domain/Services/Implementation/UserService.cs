using Microsoft.EntityFrameworkCore;

namespace Users.Domain.Services.Implementation;

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
    public async Task<Models.User> CreateAsync(Models.User item)
    {
        var user = (await _dbContext.Users.AddAsync(item)).Entity;
        await _dbContext.SaveChangesAsync();

        return user;
    }
    
    /// <inheritdoc />
    public async Task<Models.User> GetAsync(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.AccountId == id);
        if (user == null)
        {
            throw new KeyNotFoundException();
        }

        return user;
    }
    
    /// <inheritdoc />
    public async Task UpdateAsync(Models.User item)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(g => g.AccountId == item.AccountId);
        
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