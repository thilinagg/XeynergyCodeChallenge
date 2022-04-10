using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Application.Common.Interfaces;
using XeynergyCodeChallenge.Domain.Common;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService) : base(options)
        {
            _dateTimeService = dateTimeService?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<NormalUser> NormalUsers => Set<NormalUser>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<UserGroup> UserGroups => Set<UserGroup>();
        public DbSet<AccessRule> AccessRules => Set<AccessRule>();
        public DbSet<Customer> Customers => Set<Customer>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified =_dateTimeService.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
