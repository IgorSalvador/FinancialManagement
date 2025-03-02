using FinancialManagement.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManagement.Infra.Data.Mappings
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("Budgets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LimitAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
