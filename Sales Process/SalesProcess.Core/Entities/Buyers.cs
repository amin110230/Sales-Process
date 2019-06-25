using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class Buyers : BaseEntity
    {
        public virtual string BuyersName { get; set; }

        public virtual string BuyersCode { get; set; }

        public virtual string BuyersRegion { get; set; }

        public virtual string BuyersMobile { get; set; }

        public virtual string BuyersEmail { get; set; }

        public virtual bool IsActive { get; set; }

    }
}
