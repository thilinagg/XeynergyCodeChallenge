
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Person> Persons { get; }
        public DbSet<NormalUser> NormalUsers { get; }
        public DbSet<Admin> Admins { get; }
        public DbSet<UserGroup> UserGroups { get; }
        public DbSet<AccessRule> AccessRules { get; }
        public DbSet<Customer> Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
