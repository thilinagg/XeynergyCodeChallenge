using System;
using System.Collections.Generic;
using XeynergyCodeChallenge.Domain.Common;

namespace XeynergyCodeChallenge.Domain.Entities
{
    public class UserGroup: BaseEntity
    {
        public string GroupName { get; set; }
        public ICollection<NormalUser> Users { get; set; }
        public  Guid AccessRuleId { get; set; }
        public virtual AccessRule AccessRule { get; set; }
    }
}
