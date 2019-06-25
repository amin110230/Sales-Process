using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class Products : BaseEntity
    {
        public virtual string ProductName { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual string ProductUnit { get; set; }

        public virtual double ProductPrice { get; set; }

        public virtual int StockQuantity { get; set; }

        public virtual bool IsAvailable { get; set; }

    }
}
