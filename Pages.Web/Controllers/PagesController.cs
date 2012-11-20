using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pages.Data.Entities;
using Pages.Data.Repositories;

namespace Pages.Controllers
{
    public class PagesController : ApiController<Page, String>
    {
        public PagesController(IRepository<Page,String> repo) : base(repo)
        {
            this.repo.Save(new Page { Name = "index", Content = " Empty index page \n Create [new](#pages/new) ?" });
        }
    }
}