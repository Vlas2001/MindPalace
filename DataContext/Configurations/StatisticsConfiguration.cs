using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Configurations;

public class StatisticsConfiguration: IEntityTypeConfiguration<Statistics>
{
    public void Configure(EntityTypeBuilder<Statistics> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.CorrectAnswersPercentage).IsRequired();

    }
}