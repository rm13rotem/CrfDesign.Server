using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Models.LoginModels
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Investigator",
                "InternationalReviewBoard",
                "ClinicalTrialLeader", "DataMonitor","SiteManager" };

            foreach (var role in roleNames)
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var newRole = new IdentityRole(role);
                        await roleManager.CreateAsync(newRole);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Role {role} Not added");
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
