using FinancialManagement.Business.Core;
using FinancialManagement.Business.Models.Enums;

namespace FinancialManagement.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Profile Profile { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
