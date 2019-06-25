using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProcess.Core.Entities
{
    public class BaseEntity
    {
        public virtual long Id { get; set; }

        public virtual Nullable<DateTime> CreatedOn { get; set; }
    }
}
