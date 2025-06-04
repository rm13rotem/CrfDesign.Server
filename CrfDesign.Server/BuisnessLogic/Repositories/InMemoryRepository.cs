using BuisnessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : class, IPersistantEntity
    {
        private readonly IDbRepository<T> _repository;
        private List<T> _backingList;

        public InMemoryRepository(IDbRepository<T> dbRepository)
        {
            _repository = dbRepository;
            _backingList = dbRepository.GetAllAsync(false).Result;
        }
        public async Task<bool> AddAsync(T entity)
        {
            var isSuccess = await _repository.AddAsync(entity);
            if (isSuccess)
                _backingList.Add(entity);
            return isSuccess;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var existing = await GetByIdAsync(id); 
            if (existing == null)
                return false;
            var result = await _repository.DeleteByIdAsync(id); 
            if (result)
                existing.IsDeleted = false;
            return result;
        }

        public async Task<List<T>> GetAllAsync(bool tracked = true)
        {
            if (_backingList != null)
                return _backingList;
            else
            {
                _backingList = await _repository.GetAllAsync(false);
                return _backingList;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // Auto refresh if not found
            var existing = _backingList.Find(x => x.Id == id);
            if (existing != null)
                return existing;
            else
            {
                _backingList = await _repository.GetAllAsync(false);
                return _backingList.Find(x => x.Id == id);
            }
        }
        

        public async Task<bool> UpdateAsync(T entity)
        {
            var existing = await GetByIdAsync(entity.Id);
            if (existing == null)
                return false;
            // else
            var isSuccess = await _repository.UpdateAsync(entity);
            if (isSuccess)
            {
                _backingList.Remove(existing);
                _backingList.Add(entity);
                return true;
            }
            return isSuccess;
        }
    }
}
