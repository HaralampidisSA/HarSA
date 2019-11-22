using HarSA.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApi.Models;

namespace SampleApi.Data
{
    public class ChatMessageMap : EntityTypeConfiguration<ChatMessage>
    {
        public override void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");

            base.Configure(builder);
        }
    }
}
