using FinancialManagement.Business.Core.Models;
using FinancialManagement.Business.Models.Enums;

namespace FinancialManagement.Business.Models;

public class Transaction : Entity
{
    public decimal Amount { get; set; }
    public DateTime CreatedOn { get; set; }
    public TransactionType Type { get; set; }
    public string Description { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

}