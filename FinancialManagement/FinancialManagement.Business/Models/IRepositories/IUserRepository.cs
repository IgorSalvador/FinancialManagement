using System.Security.Claims;
using FinancialManagement.Business.Core.Data;

namespace FinancialManagement.Business.Models.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserAuth(string username);
    }
}
