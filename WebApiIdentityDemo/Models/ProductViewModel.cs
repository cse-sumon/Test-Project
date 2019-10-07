using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiIdentityDemo.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float PurchasePrice { get; set; }
        [Required]
        public float SalesPrice { get; set; }
        [Required]
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        [Required]
        public int Status { get; set; }
    }
}