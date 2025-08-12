using BuisnessLogic.DataContext;
using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using BuisnessLogic.Repositories;
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

        public async Task SaveToDb(IInMemoryCrfDataStore dataStore)
        {
            if (string.IsNullOrWhiteSpace(DB))
                return; // no info given
            DataBaseViewModel dataBase = JsonConvert.DeserializeObject<DataBaseViewModel>(DB);
            if (dataBase == null)
                return;

            ImportDataIntoDatabase<QuestionType>(dataStore, dataBase);
            ImportDataIntoDatabase<CrfOptionCategory>(dataStore, dataBase);
            ImportDataIntoDatabase<CrfOption>(dataStore, dataBase);
            ImportDataIntoDatabase<CrfPage>(dataStore, dataBase);
        }

        private void ImportDataIntoDatabase<T>(IInMemoryCrfDataStore dataStore, DataBaseViewModel dataBase)
            where T : class, IPersistantEntity
        {
            List<T> list = null;
            if (typeof(T) == typeof(CrfPage))
                list = dataBase.CrfPages as List<T>;
            if (typeof(T) == typeof(CrfOption))
                list = dataBase.CrfOptions as List<T>;
            if (typeof(T) == typeof(CrfOptionCategory))
                list = dataBase.CrfOptionCategories as List<T>;
            if (typeof(T) == typeof(QuestionType))
                list = dataBase.QuestionTypes as List<T>;

            foreach (var t in list.OrderBy(x=>x.Id))
            {
                if (IsOverwrite == true)
                {
                    dataStore.Update(t);
                }
                if (IsAppend == true)
                {
                    T r = t.ToNewEntity() as T;
                    if (r != null)
                    {
                        dataStore.Add(r);
                    }
                }
            }
        }
    }
}
