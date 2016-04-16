using DapperDriven.Model;
using DapperDriven.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperDriven.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Repository repository = new Repository();

            HomeModel model = new HomeModel();
            model.Invoice = repository.GetInvoice(5);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}