using CrfDesign.Server.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrfDesign.Server.WebAPI.Data
{
    public class CrfDesignContext : DbContext
    {
        public CrfDesignContext(DbContextOptions<CrfDesignContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<CrfPage> CrfPages { get; set; }
        public DbSet<CrfPageComponent> CrfPageComponents { get; set; }
        public DbSet<CrfOption> CrfOptions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
