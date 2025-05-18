using BuisnessLogic.DataContext;
using BuisnessLogic.Filters;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Interfaces.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLogic.Models.Managers
{
    public class Manager<T> : IEntityManger<CrfDesignContext, T>
        where T : class, IPersistantEntity, new()
    {
        CrfDesignContext _context { get; set; }

        public Manager(CrfDesignContext context)
        {
            _context = context;
        }

    public async Task<bool> Delete(int id)
    {
        var existingEntity = await GetById(id);
        if (existingEntity == null)
            return false;
        else existingEntity.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<CrfOption>> GetCrfOptionsAsync(CrfOptionFilter crfOptionFilter)
    {
        return await _context.CrfOptions
            .Where(x => x.Name.Contains(crfOptionFilter.PartialName))
            .ToListAsync();
    }

    public async Task<bool> TryInsert(T entity)
    {
        try
        {
            return await Insert(entity);
        }
        catch (System.Exception)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Task.Delay(i * 5000).Wait();
                    return await Insert(entity);
                }
                catch (System.Exception)
                {

                }
            }
        }
        return false;
    }

    private async Task<bool> Insert(T entity)
    {
        entity.Id = _context.Set<T>().Any() ?
            _context.CrfOptions.Max(x => x.Id) + 1 :
            1;
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryUndelete(int id)
    {
        var existingEntity = await GetById(id);
        if (existingEntity == null)
            return false;
        else existingEntity.IsDeleted = false;
        await _context.SaveChangesAsync();
        return true;
    }

        public async Task<bool> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        { 
            try
            {
                return await Update(entity);
            }
            catch (System.Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        Task.Delay(i * 5000).Wait();
                        return await Update(entity);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            return false;
        }

        public void Duplicate(T entity)
        {
            entity.Id = 0;
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
    }
}
