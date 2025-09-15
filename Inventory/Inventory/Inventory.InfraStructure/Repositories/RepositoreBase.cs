﻿using Inventory.InfraStructure.Configure;
using Inventory.InfraStructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Inventory.InfraStructure.Repositories;

public class RepositoreBase<T>(DataContext context) : IRepositoryBase<T>
    where T : class
{
    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await context.Set<T>().ToListAsync();

    public virtual async Task<T?> GetByIdAsync(Guid id)
        => await Task.FromResult(context.Set<T>().Find(id));

    public virtual async Task<T?> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}