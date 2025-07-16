using BuisnessLogic.Interfaces;
using BuisnessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            var user = _httpContextAccessor.HttpContext?.User;
            string? currentUserId = null;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Convert if Investigator.Id is an int (or parse Guid if it's Guid)
                if (!string.IsNullOrWhiteSpace(idClaim))
                {
                    currentUserId = idClaim;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is IPersistantEntity);

            foreach (var entry in modifiedEntries)
            {
                var entity = (IPersistantEntity)entry.Entity;
                entity.ModifiedDateTime = DateTime.UtcNow;
                entity.LastUpdatorUserId = currentUserId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

