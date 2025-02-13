﻿using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context) , IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects
            .Include(x => x.Status)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        return await _context.Projects
            .Include (p => p.Customer)
            .Include (p => p.Product)
            .Include (p => p.Status)
            .Include (p => p.User)
            .FirstOrDefaultAsync(expression) ?? null!;
    }
}
