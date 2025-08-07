using BuisnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.DataContext;
using CrfDesign.Server.WebAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace CrfDesign.Server.WebAPI.Models.Backup
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel()
        {

        }
        public LoginUserViewModel(ApplicationDbContext _context)
        {
            IdentityRoleClaims = _context.RoleClaims.ToList();
            Investigators = _context.Users.ToList();
            IdentityUserClaims = _context.UserClaims.ToList();
            IdentityUserLogins = _context.UserLogins.ToList();
            IdentityUserRoles = _context.UserRoles.ToList();
            IdentityRoles = _context.Roles.ToList();
            IdentityUserTokens = _context.UserTokens.ToList();
        }

        public List<IdentityRoleClaim<string>> IdentityRoleClaims { get; set; }
        
        public List<Investigator> Investigators {get; set;}

        public List<IdentityUserRole<string>> IdentityUserRoles { get; set; }
        public List<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
        public List<IdentityUserLogin<string>> IdentityUserLogins { get; set; }
        public List<IdentityRole> IdentityRoles { get; set; }
        public List<IdentityUserToken<string>> IdentityUserTokens { get; set; }
    }
}
