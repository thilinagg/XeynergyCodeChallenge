using System;
using XeynergyCodeChallenge.Domain.Common;

namespace XeynergyCodeChallenge.Domain.Entities
{
    public class Admin: BaseEntity
    {
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public string Privilage { get; set; }
    }
}
