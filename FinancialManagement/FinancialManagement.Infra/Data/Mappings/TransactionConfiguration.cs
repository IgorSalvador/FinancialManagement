using FinancialManagement.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManagement.Infra.Data.Mappings
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(x => x.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(2500)");

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.BankAccountId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x=> x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.BankAccount)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.BankAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
