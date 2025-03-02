using FinancialManagement.Business.Core.Models;

namespace FinancialManagement.Business.Models
{
    public class Bank : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
