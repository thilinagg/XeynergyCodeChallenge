using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeynergyCodeChallenge.Domain.Constants;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedInitialDataAsync(ApplicationDbContext context)
        {
            await SeedInitialAccessRules(context);
            await SeedInitialUserGroups(context);
            await SeedInitialCustomers(context);
        }

        private static async Task SeedInitialAccessRules(ApplicationDbContext context)
        {
            if (await context.AccessRules.AnyAsync())
                return;

            await context.AccessRules.AddRangeAsync(new List<AccessRule>
            {
                new AccessRule{ RuleName = AccessRuleNames.RuleOne, Permission = true },
                new AccessRule{ RuleName = AccessRuleNames.RuleTwo, Permission = true }
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedInitialUserGroups(ApplicationDbContext context)
        {
            if (await context.UserGroups.AnyAsync())
                return;

            await context.UserGroups.AddRangeAsync(new List<UserGroup>
            {
                new UserGroup { GroupName = UserGroupNames.GroupOne, AccessRuleId = (await context.AccessRules.SingleAsync(r=> r.RuleName == AccessRuleNames.RuleOne)).Id },
                new UserGroup { GroupName = UserGroupNames.GroupTwo, AccessRuleId = (await context.AccessRules.SingleAsync(r=> r.RuleName == AccessRuleNames.RuleTwo)).Id }
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedInitialCustomers(ApplicationDbContext context)
        {
            if (await context.Customers.AnyAsync())
                return;

            await context.Customers.AddAsync(new Customer());
            await context.SaveChangesAsync();
        }
    }
}
