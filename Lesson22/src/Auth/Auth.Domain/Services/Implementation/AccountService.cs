using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Auth.Domain.Services.Implementation;

public class AccountService : IAccountService
{
    private readonly AuthDbContext _dbContext;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext"></param>
    public AccountService(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<Account> CreateAsync(string login, string password)
    {
        if (await _dbContext.Accounts.AnyAsync(u => u.Login.ToLower() == login.ToLower()))
        {
            throw new Exception("This user name has already existed");
        }

        var itemDb = new Account()
        {
            Hash = BCrypt.Net.BCrypt.HashPassword(password),
            Login = login.ToLower(),
            Created = DateTime.UtcNow
        };
        
        var account = (await _dbContext.Accounts.AddAsync(itemDb)).Entity;
        await _dbContext.SaveChangesAsync();

        return account;
    }

    public async Task<Account> LoginAsync(string login, string password)
    {
        var account = await _dbContext.Accounts.Where(x => x.Login.ToLower() == login.ToLower()).FirstOrDefaultAsync();
        if (account != null)
        {
            if (BCrypt.Net.BCrypt.Verify(password, account.Hash))
            {
                return account;
            }
        }

        return null;
    }
}