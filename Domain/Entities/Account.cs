using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : AuditableEntity
    {
        public Guid AccountId { get; set; }
        public Guid BusinessUnitId { get; set; }
        public string Name { get; set; }
        public int AccountNumber { get; set; }
    }
}
