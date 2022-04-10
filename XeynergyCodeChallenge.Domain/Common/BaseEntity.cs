using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace XeynergyCodeChallenge.Domain.Common
{
    public class BaseEntity: AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
