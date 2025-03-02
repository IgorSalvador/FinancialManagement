using FinancialManagement.Business.Core.Models;
using FinancialManagement.Business.Models.Enums;

namespace FinancialManagement.Business.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public TransactionType Type { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
