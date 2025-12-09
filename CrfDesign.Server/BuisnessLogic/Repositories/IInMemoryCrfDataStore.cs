using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BuisnessLogic.Repositories
{
    public interface IInMemoryCrfDataStore
    {
        List<CrfOptionCategory> CrfOptionCategories { get; }
        List<CrfOption> CrfOptions { get; }
        List<CrfPageComponent> CrfPageComponents { get; }
        List<CrfPage> CrfPages { get; }
        List<QuestionType> QuestionTypes { get; }

        bool Add<T>(T entity) where T : class, IPersistantEntity;
        bool Delete<T>(int id) where T : class, IPersistantEntity;
        bool Undelete<T>(int id) where T : class, IPersistantEntity;
        bool Update<T>(T entity) where T : class;

        public List<T> GetList<T>() where T : class, IPersistantEntity;
        public void Refresh();

    }
}