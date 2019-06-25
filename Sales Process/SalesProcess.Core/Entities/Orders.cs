using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class Orders : BaseEntity
    {
        public virtual string OrderId { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual decimal OrderTotalAmount { get; set; }

        public virtual bool IsPaid { get; set; }

    }
}
