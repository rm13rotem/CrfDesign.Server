using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLogic.Repositories
{
    public interface IInMemoryCrfDataStore
    {
        List<CrfOptionCategory> CrfOptionCategories { get; }
        List<CrfOption> CrfOptions { get; }
        List<CrfPageComponent> CrfPageComponents { get; }
        List<CrfPage> CrfPages { get; }
        List<QuestionType> QuestionTypes { get; }

        Task<bool> AddAsync<T>(T entity) where T : class, IPersistantEntity;
        Task<bool> DeleteAsync<T>(int id) where T : class, IPersistantEntity;
        Task<bool> UndeleteAsync<T>(int id) where T : class, IPersistantEntity;
        Task<bool> UpdateAsync<T>(T entity) where T : class;

        List<T> GetList<T>() where T : class, IPersistantEntity;
        void Refresh();
        void LoadData(string typeName);
    }
}