using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace Pages.Controllers
{
    class ResourceNotFoundException<TResource, TIdentifier> : HttpResponseException
    {
        public ResourceNotFoundException(TIdentifier id) : base(System.Net.HttpStatusCode.NotFound) { }
    }
}
