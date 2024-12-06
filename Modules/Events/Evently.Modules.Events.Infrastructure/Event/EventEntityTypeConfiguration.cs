using Evently.Modules.Events.Domain.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Events.Infrastructure.Event;
internal sealed class EventEntityTypeConfiguration : IEntityTypeConfiguration<Events.Domain.Event.Event>
{
    public void Configure(EntityTypeBuilder<Domain.Event.Event> builder)
    {
        builder.HasOne<Category>().WithMany();
    }
}
