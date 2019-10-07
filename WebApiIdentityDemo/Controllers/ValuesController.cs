using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using WebApiIdentityDemo.Models;
using Microsoft.AspNet.Identity;

namespace WebApiIdentityDemo.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        
        DataAccessLayer.CategoryLayer categoryLayer = new DataAccessLayer.CategoryLayer();
       
        
        public IHttpActionResult Get()
        {
            DataTable dt = categoryLayer.getCategory();
            var list = (from tr in dt.AsEnumerable()
                        select new Category()
                        {
                            Id = tr.Field<int>("Id"),
                            Name = tr.Field<string>("Name"),
                            Status = tr.Field<int>("Status")
                        }).ToList();
            return Ok(list);
        }

        // GET api/values/5

        public string Get(int id)
        {
            var user = RequestContext.Principal.Identity.GetUserName();
            return "value"+user;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }


    }
}
