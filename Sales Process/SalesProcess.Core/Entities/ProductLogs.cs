using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class ProductLogs : BaseEntity
    {
        public virtual long ProductId { get; set; }

        public virtual long OrderId { get; set; }

        public virtual int Quantity { get; set; }

        public virtual string Action { get; set; }

    }
}
