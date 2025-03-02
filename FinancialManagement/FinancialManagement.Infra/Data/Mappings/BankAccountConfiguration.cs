using FinancialManagement.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialManagement.Infra.Data.Mappings
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Balance)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.BankId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.CreatedOn)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(x => x.Bank)
                .WithMany(x => x.BankAccounts)
                .HasForeignKey(x => x.BankId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
