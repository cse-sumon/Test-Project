using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using WebApiIdentityDemo.Models;
using System.Text;

namespace WebApiIdentityDemo.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        DataAccessLayer.CategoryLayer categoryLayer = new DataAccessLayer.CategoryLayer();
       [Authorize(Roles ="Admin")]
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


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IHttpActionResult Post(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Category category = new Category()
            {
                Name = model.Name,
                Status = model.Status,
            };
            categoryLayer.addCategory(category);
            return Ok(200);
        }

        [HttpPut]
        public IHttpActionResult Put(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Category category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Status = model.Status
            };
            categoryLayer.updateCategory(category);
            return Ok(200);
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            categoryLayer.deleteCategory(id);
            return Ok(204);
        }


        [HttpGet]
        [Route("api/category/totalcategory")]

        public IHttpActionResult TotalCategory()
        {
            return Ok(categoryLayer.totalCategoryCount());

        }

    }
}
