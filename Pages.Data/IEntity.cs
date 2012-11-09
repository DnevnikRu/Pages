using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pages.Data
{
    public interface IEntity<TIdentifier> where TIdentifier: class
    {
        TIdentifier Id { get; set; }
    }
}