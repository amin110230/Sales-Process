using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.DataModels
{
    public class PaymentDetailsDataModel
    {
        public virtual string OrderId { get; set; }

        public virtual decimal AmountPayable { get; set; }

        public virtual decimal PaidAmount { get; set; }

        public virtual DateTime CreatedOn { get; set; }
    }
}
