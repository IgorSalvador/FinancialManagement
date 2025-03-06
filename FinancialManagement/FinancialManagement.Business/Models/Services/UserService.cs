using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinancialManagement.Business.Core.Notifications;
using FinancialManagement.Business.Core.Services;
using FinancialManagement.Business.Core.Utils;
using FinancialManagement.Business.Models.IRepositories;
using FinancialManagement.Business.Models.Services.IServices;

namespace FinancialManagement.Business.Models.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
                           INotificator notificator) : base(notificator)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Claim>?> Auth(string username, string password)
        {
            var user = await _userRepository.GetUserAuth(username);

            if (!ValidateUser(user, password)) return null;

            var claims = this.GenerateClaims(user);

            return claims;
        }

        private bool ValidateUser(User user, string password)
        {
            if (!user.IsActive)
            {
                Notify("Usuário inátivo, contate seus administradores!");
                return false;
            }

            if (Crypto.VerifyHashedPassword(user.Password, password)) return true;

            Notify("Usuário ou senha inválidos!");
            return false;

        }

        private List<Claim> GenerateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, EnumsLists.GetProfile(user.Profile)),
            };

            return claims;
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
