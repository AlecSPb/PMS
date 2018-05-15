using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMSWeb.ExtraService;

namespace PMSWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ToDoList()
        {

            try
            {
                using (var service = new ToDoServiceClient())
                {
                    var dataList = service.GetToDo("", "", 0, 50).ToList();
                    return View(dataList);
                }
            }
            catch (Exception)
            {

            }
            return HttpNotFound();
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