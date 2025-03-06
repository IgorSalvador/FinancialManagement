using FinancialManagement.Business.Models;
using FinancialManagement.Business.Models.IRepositories;
using FinancialManagement.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<User> GetUserAuth(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(username));
        }
    }
}
