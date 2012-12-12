using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pages.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pages.Data.Entities
{
    [DataContract(Name = "page")]
    public class Page : IEntity<String>, IHasModifiedDatetime
    {
        [Key]
        [DataMember(Name = "name")]
        public String Id { get; set; }

        [DataMember(Name = "content")]
        public String Content { get; set; }

        [DataMember(Name="modifiedAt")]
        public DateTime ModifiedAt { get; set; }
    }
}