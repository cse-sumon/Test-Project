using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiIdentityDemo.DataAccessLayer;

namespace WebApiIdentityDemo.Controllers
{
    public class DashBoardController : Controller
    {
        ProductLayer productLayer = new ProductLayer();
        CategoryLayer categoryLayer = new CategoryLayer();

        public  ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        // GET: DashBoard
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult UnAuthorizedPage()
        {
            return View();
        }

        
    }
}