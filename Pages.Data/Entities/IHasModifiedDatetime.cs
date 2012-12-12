using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Entities
{
    public interface IHasModifiedDatetime
    {
        DateTime ModifiedAt { get; set; }
    }
}
