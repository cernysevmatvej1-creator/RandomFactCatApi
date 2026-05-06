using ApiDeepSeek.Common;
using ApiDeepSeek.Doamin.Interfais;
using ApiDeepSeek.models;
using GroupApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiDeepSeek.Infrastructure.Repotisory
{
    public class UserRepotisiory : IUserRepotisory
    {
        private readonly AppDbContext _context;
        public UserRepotisiory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Result.Ok(); 
            }
            catch (Exception ex) {
                return Result.Fail(ex.Message);
            }
            
        }

        public async Task<Result<User>> GetUserId(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);

            return Result<User>.Ok(user);
        }

    }
}
