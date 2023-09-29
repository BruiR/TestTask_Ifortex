using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> GetUser()
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(user => user.Orders)
                .OrderByDescending(user => user.Orders.Count)
                .FirstOrDefaultAsync();
            user.Orders = null;
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .Where(order => order.Status == UserStatus.Inactive)
                .ToListAsync();

            return users;
        }
    }
}
