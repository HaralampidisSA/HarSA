﻿using HarSA.Domain;
using HarSA.Infrastructure;
using System;

namespace HarSA.EntityFrameworkCore.Application
{
    [Obsolete]
    public interface ICrudService<TEntity> where TEntity : BaseEntity, new()
    {
        TEntity Get(int id);

        IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue);

        int Add(TEntity entity);

        int Update(TEntity entity);

        int Delete(TEntity entity);
    }
}
