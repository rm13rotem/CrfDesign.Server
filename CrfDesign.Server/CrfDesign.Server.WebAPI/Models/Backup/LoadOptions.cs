using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.Backup
{
    public class LoadOptions
    {
        public bool IsOverwrite { get; set; }
        public bool IsAppend { get; set; }
        public string DB { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public async Task SaveToDb(CrfDesignContext context)
        {
            if (string.IsNullOrWhiteSpace(DB))
                return; // no info given
            DataBaseViewModel dataBase = JsonConvert.DeserializeObject<DataBaseViewModel>(DB);
            if (dataBase == null)
                return;

            ImportDataIntoDatabase<QuestionType>(context, dataBase);
            ImportDataIntoDatabase<CrfOptionCategory>(context, dataBase);
            ImportDataIntoDatabase<CrfOption>(context, dataBase);
            ImportDataIntoDatabase<CrfPage>(context, dataBase);
            ImportDataIntoDatabase<CrfPageComponent>(context, dataBase);
        }

        private void ImportDataIntoDatabase<T>(CrfDesignContext context, DataBaseViewModel dataBase)
            where T : class, IPersistantEntity
        {
            List<T> list = null;
            if (typeof(T) == typeof(CrfPage))
                list = dataBase.CrfPages as List<T>;
            if (typeof(T) == typeof(CrfPageComponent))
                list = dataBase.CrfPageComponents as List<T>;
            if (typeof(T) == typeof(CrfOption))
                list = dataBase.CrfOptions as List<T>;
            if (typeof(T) == typeof(CrfOptionCategory))
                list = dataBase.CrfOptionCategories as List<T>;
            if (typeof(T) == typeof(QuestionType))
                list = dataBase.QuestionTypes as List<T>;

            foreach (var t in list.OrderBy(x=>x.Id))
            {
                var existing = context.Set<T>().FirstOrDefault(x => x.Id == t.Id);
                if (existing != null && IsOverwrite == true)
                { 
                    context.Set<T>().Attach(t);
                    context.SaveChanges();
                }
                if (existing == null && IsAppend == true)
                {
                    T r = t.ToNewEntity() as T;
                    if (r != null)
                    {
                        context.Set<T>().Add(r);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
