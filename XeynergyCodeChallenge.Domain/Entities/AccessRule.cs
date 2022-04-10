using System.Collections.Generic;
using XeynergyCodeChallenge.Domain.Common;

namespace XeynergyCodeChallenge.Domain.Entities
{
    public class AccessRule: BaseEntity
    {
        public string RuleName { get; set; }
        public bool Permission { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
