using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BuisnessLogic.DataContext
{
    public class CrfDesignContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrfDesignContext(DbContextOptions<CrfDesignContext> options, 
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<CrfPage> CrfPages { get; set; }
        public DbSet<CrfPageComponent> CrfPageComponents { get; set; }
        public DbSet<CrfOption> CrfOptions { get; set; }
        public DbSet<CrfOptionCategory> CrfOptionCategories { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is IPersistantEntity);

            foreach (var entry in modifiedEntries)
            {
                var entity = (IPersistantEntity)entry.Entity;

                // Prevent changes if locked
                if (entity.IsLockedForChanges)
                    throw new InvalidOperationException("This record is locked and cannot be modified.");

                // Log who made the update
                entity.ModifiedDateTime = DateTime.UtcNow;

                // Access user ID
                var httpContextAccessor = this.GetService<IHttpContextAccessor>();
                var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                entity.LastUpdatorUserId = userId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

