using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoAsLessCodeAsPossible.Domain.Entity;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence.TableConfiguration;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable(nameof(ToDo));

        builder.HasKey(o => o.Id);
        builder.Property("_title").HasColumnName("Title").HasMaxLength(100).IsRequired();
        builder.Property("_isCompleted").HasColumnName("IsCompleted").IsRequired();
    }
}