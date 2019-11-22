using HarSA.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleApi.Models;

namespace SampleApi.Data
{
    public class OnlineClientMap : EntityTypeConfiguration<OnlineClient>
    {
        public override void Configure(EntityTypeBuilder<OnlineClient> builder)
        {
            builder.ToTable("OnlineClients");

            base.Configure(builder);
        }
    }
}
