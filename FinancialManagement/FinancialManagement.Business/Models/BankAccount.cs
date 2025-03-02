using FinancialManagement.Business.Core.Models;

namespace FinancialManagement.Business.Models;

public class BankAccount : Entity
{
    public string Name { get; set; } // Example: "Checking Account", "Savings"
    public decimal Balance { get; set; } = 0;
    public DateTime CreatedOn { get; set; }

    public int BankId { get; set; }
    public Bank Bank { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}