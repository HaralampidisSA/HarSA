﻿using HarSA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HarSA.EntityFrameworkCore
{
    public class HarSADbContext : DbContext, IDbContext
    {
        public HarSADbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeFinder = new AppDomainTypeFinder();
            foreach (var typeConfiguration in typeFinder.FindClassesOfType(typeof(IEntityTypeConfiguration<>)))
            {
                dynamic config = Activator.CreateInstance(typeConfiguration);
                modelBuilder.ApplyConfiguration(config);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Added))
            {
                item.CurrentValues["DateCreated"] = DateTime.Now;
                item.CurrentValues["IsDeleted"] = 0;
            }

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Modified))
            {
                item.CurrentValues["DateUpdated"] = DateTime.Now;
            }

            foreach (var item in ChangeTracker.Entries().Where(w => w.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["DateDeleted"] = DateTime.Now;
                item.CurrentValues["IsDeleted"] = 1;
            }

            return base.SaveChanges();
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
