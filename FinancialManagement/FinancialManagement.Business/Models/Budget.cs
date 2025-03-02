using FinancialManagement.Business.Core.Models;

namespace FinancialManagement.Business.Models;

public class Budget : Entity
{
    public decimal LimitAmount { get; set; }
    public DateTime StartDate { get; set; } // Can be monthly, weekly, etc.
    public DateTime EndDate { get; set; } // Can be monthly, weekly, etc.
    public DateTime CreatedOn { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}