using HarSA.Domain;
using Microsoft.EntityFrameworkCore;

namespace HarSA.EntityFrameworkCore
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();

        void Dispose();
    }
}
