using System;
using XeynergyCodeChallenge.Domain.Common;

namespace XeynergyCodeChallenge.Domain.Entities
{
    public class NormalUser: BaseEntity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public Guid AttachedCustomerId { get; set; }
        public virtual Customer AttachedCustomer { get; set; }
        public Guid UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}
