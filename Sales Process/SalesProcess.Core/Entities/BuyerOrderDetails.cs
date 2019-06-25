using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class BuyerOrderDetails : BaseEntity
    {
        public virtual long OrderId { get; set; }

        public virtual long BuyersId { get; set; }

        public virtual double PayablePCT { get; set; }

        public virtual decimal AmountPayable { get; set; }

        public virtual decimal PaidAmount { get; set; }

    }
}
