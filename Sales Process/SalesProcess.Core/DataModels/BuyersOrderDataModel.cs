using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.DataModels
{
    public class BuyersOrderDataModel
    {
        public virtual long Id { get; set; }

        public virtual string BuyersName { get; set; }

        public virtual decimal TotalAmount { get; set; }

        public virtual decimal PaidAmount { get; set; }

        public virtual decimal DueAmount { get; set; }

        public virtual double PayablePCT { get; set; }
    }
}
