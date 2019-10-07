using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiIdentityDemo.Controllers
{
    public class FrontEndController : ApiController
    {
        DataAccessLayer.CategoryLayer categoryLayer = new DataAccessLayer.CategoryLayer();
        DataAccessLayer.ProductLayer productLayer = new DataAccessLayer.ProductLayer();

        [HttpGet]
        [Route("api/FrontEnd/GetCategory")]
        public IHttpActionResult GetCategory()
        {
            DataTable dt = categoryLayer.getCategory();
            return Ok(dt);
        }

        [HttpGet]
        [Route("api/FrontEnd/GetProduct")]
        public IHttpActionResult GetProduct()
        {
            DataTable dt = productLayer.getProduct();
            return Ok(dt);
        }

        [HttpGet]
        [Route("api/FrontEnd/GetProduct/{id}")]
        public IHttpActionResult GetProduct(int id)
        {
            DataTable dt = productLayer.getProductByCategoryId(id);
            return Ok(dt);
        }
    }
}
