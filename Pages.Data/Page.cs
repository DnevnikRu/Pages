using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pages.Data;

namespace Pages.Models
{
    public class Page : IEntity<String>
    {
        public String Id
        {
            get { return this.Name;  }
            set { this.Name = value; }
        }

        public String Name { get; set; }
        public String Content { get; set; }
        public DateTime Modified { get; set; }
    }
}