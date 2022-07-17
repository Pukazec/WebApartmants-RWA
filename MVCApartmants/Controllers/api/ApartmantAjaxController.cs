using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCApartmants.Controllers
{
    public class ApartmantAjaxController : ApiController
    {
        // GET: api/Ajax
        public IHttpActionResult Get()
        {
            return Json((System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartments());
        }

        // GET: api/Ajax/5
        public IHttpActionResult Get(int id)
        {
            return Json((System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartment(id));
        }

        // POST: api/Ajax
        public void Post([FromBody]Apartment value)
        {
        }

        // PUT: api/Ajax/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Ajax/5
        public void Delete(int id)
        {
        }
    }
}
