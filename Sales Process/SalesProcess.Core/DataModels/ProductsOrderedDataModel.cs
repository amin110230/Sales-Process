using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.DataModels
{
    public class ProductsOrderedDataModel
    {
        public virtual string ProductName { get; set; }

        public virtual int Quantity { get; set; }

        public virtual Decimal Price { get; set; }
    }
}
