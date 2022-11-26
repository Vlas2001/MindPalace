using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataContext.Configurations;

public class MemorizeItemConfiguration: IEntityTypeConfiguration<MemorizeItem>
{
    public void Configure(EntityTypeBuilder<MemorizeItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Item).IsRequired();
    }
}