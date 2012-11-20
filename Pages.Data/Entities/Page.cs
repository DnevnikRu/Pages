using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pages.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Pages.Data.Entities
{
    [DataContract(Name = "page")]
    public class Page : IEntity<String>
    {
        public String Id
        {
            get { return this.Name;  }
            set { this.Name = value; }
        }

        [DataMember(Name = "name")]
        public String Name { get; set; }

        [DataMember(Name = "content")]
        public String Content { get; set; }

        public DateTime Modified { get; set; }
    }
}