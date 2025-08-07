using CrfDesign.Server.WebAPI.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrfDesign.Server.WebAPI.Models.Backup
{
    public class LoadUserOptions
    {
        public bool IsOverwrite { get; set; }
        public bool IsAppend { get; set; }
        public string DB { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public async Task SaveToDb(ApplicationDbContext context)
        {
            if (string.IsNullOrWhiteSpace(DB))
                return; // no info given
            LoginUserViewModel dataBase = JsonConvert.DeserializeObject<LoginUserViewModel>(DB);
            if (dataBase == null)
                return;

            ImportDataIntoDatabase<Investigator>(context, dataBase);
            ImportDataIntoDatabase<IdentityRoleClaim<string>>(context, dataBase);
            ImportDataIntoDatabase<IdentityUserRole<string>>(context, dataBase);
            ImportDataIntoDatabase<IdentityUserClaim<string>>(context, dataBase);
            ImportDataIntoDatabase<IdentityUserLogin<string>>(context, dataBase);
            ImportDataIntoDatabase<IdentityUserToken<string>>(context, dataBase);
        }

        private void ImportDataIntoDatabase<T>(ApplicationDbContext context, LoginUserViewModel dataBase)
            where T : class
        {
            List<T> list = null;
            if (typeof(T) == typeof(Investigator))
                list = dataBase.Investigators as List<T>;
            if (typeof(T) == typeof(IdentityRoleClaim<string>))
                list = dataBase.IdentityRoleClaims as List<T>;
            if (typeof(T) == typeof(IdentityUserRole<string>))
                list = dataBase.IdentityRoles as List<T>;
            if (typeof(T) == typeof(IdentityUserClaim<string>))
                list = dataBase.IdentityUserClaims as List<T>;
            if (typeof(T) == typeof(IdentityUserLogin<string>))
                list = dataBase.IdentityUserLogins as List<T>;
            if (typeof(T) == typeof(IdentityUserToken<string>))
                list = dataBase.IdentityUserTokens as List<T>;

            foreach (var t in list)
            {
                var existing = context.Set<T>().Attach(t);
                var isExisting = existing.State == EntityState.Added;
                if (!isExisting && IsOverwrite == true)
                {
                    existing.State = EntityState.Modified;
                    context.SaveChanges();
                }
                else if (!isExisting && IsAppend == true)
                {
                    existing.State = EntityState.Added;
                    context.SaveChanges();
                }
               
            }
        }
    }
}
