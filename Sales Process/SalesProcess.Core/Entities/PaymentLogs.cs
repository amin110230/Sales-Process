using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class PaymentLogs : BaseEntity
    {
        public virtual long BuyerOrderDetailsId { get; set; }

        public virtual decimal PaidAmount { get; set; }

    }
}
