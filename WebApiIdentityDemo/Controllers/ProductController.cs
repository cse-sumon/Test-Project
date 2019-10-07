using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiIdentityDemo.DataAccessLayer;
using WebApiIdentityDemo.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WebApiIdentityDemo.Controllers
{
    
    public class ProductController : ApiController
    {
        ProductLayer productLayer = new ProductLayer();
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            DataTable dt = productLayer.getProduct();
            return Ok(dt);
            //var list = (from tr in dt.AsEnumerable()
            //            select new Product()
            //            {
            //                Id = tr.Field<int>("Id"),
            //                CategoryId = tr.Field<int>("CategoryId"),
            //                Name = tr.Field<string>("Name"),
            //                PurchasePrice = tr.Field<float>("PurchasePrice"),
            //                SalesPrice = tr.Field<float>("SalesPrice"),
            //                Description = tr.Field<string>("Description"),
            //                Image = tr.Field<string>("Image"),
            //                Status = tr.Field<int>("Status")
            //            }).ToList();
            //return Ok(list);

        }

        [HttpPost]   
        public HttpResponseMessage Post()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Content/File/" + imageName);
            postedFile.SaveAs(filePath);

            Product product = new Product()
            {
                CategoryId = Convert.ToInt32(httpRequest["CategoryId"]),
                Name = httpRequest["Name"].ToString(),
                PurchasePrice = float.Parse(httpRequest["PurchasePrice"]),
                SalesPrice = float.Parse(httpRequest["SalesPrice"]),
                Description = httpRequest["Description"],
                Status = Convert.ToInt32(httpRequest["Status"]),
                Image = "/Content/File/"+ imageName
            };

            productLayer.addProduct(product);
            return Request.CreateResponse(HttpStatusCode.Created);
        }


        [HttpGet]
        [Route("api/product/edit/{id}")]
        public IHttpActionResult Edit(int id)
        {
            DataTable dt = productLayer.editProduct(id);
            return Ok(dt);
        }

        [HttpPut]
        public IHttpActionResult Put()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Image"];
            var old = httpRequest["oldImage"].ToString();
            Product product = new Product();
            if (postedFile == null)
            {
                product.Image = httpRequest["oldImage"].ToString();
            }
            else
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Content/File/" + imageName);
                postedFile.SaveAs(filePath);
                product.Image = "/Content/File/" + imageName;

                //delete ol file
                string oldFileName = old.Substring(14);
                string path = HttpContext.Current.Server.MapPath("~/Content/File");
                var fullPath = Path.Combine(path, oldFileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

            }
            product.Id = Convert.ToInt32(httpRequest["Id"]);
            product.CategoryId = Convert.ToInt32(httpRequest["CategoryId"]);
            product.Name = httpRequest["Name"].ToString();
            product.PurchasePrice = float.Parse(httpRequest["PurchasePrice"]);
            product.SalesPrice = float.Parse(httpRequest["SalesPrice"]);
            product.Description = httpRequest["Description"];
            product.Status = Convert.ToInt32(httpRequest["Status"]);

            productLayer.updateProduct(product);
            return Ok(200);
            
           
        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string image = productLayer.getProductImage(id);
            string imageName = image.Substring(14);
            string path = HttpContext.Current.Server.MapPath("~/Content/File");
            var fullPath = Path.Combine(path, imageName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            productLayer.deleteProduct(id);
            return Ok(204);
        }

        [HttpGet]
        [Route("api/product/totalproduct")]

        public IHttpActionResult TotalProduct()
        {
            return Ok(productLayer.totalProductCount());

        }



        [HttpGet]
        [Route("api/product/RetrieveFile")]
        public HttpResponseMessage RetrieveFile()
        {
            var data = new[]{   //Suppose you filter out these data

                               new{ Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333" },
                               new{ Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596" },
                               new{ Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569" },
                               new{ Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333" },
                               new{ Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234" },
                               new{ Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333" }
                      };

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);


            result.Content = new StringContent(WriteTsv(data));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = "RecordExport.xls";


            return result;
        }


        public string WriteTsv<T>(IEnumerable<T> data)
        {
            StringBuilder output = new StringBuilder();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Append(prop.DisplayName); // header
                output.Append("\t");
            }
            output.AppendLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Append(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Append("\t");
                }
                output.AppendLine();
            }
            return output.ToString();
        }

        [HttpGet]
        [Route("api/product/getcsv/{id}")]
        public HttpResponseMessage Get(int id)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write("Hello, World!");
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            return result;

        }

    }
}
