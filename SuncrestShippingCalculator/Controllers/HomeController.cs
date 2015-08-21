using Suncrest.ShippingCalculator.Models;
using System.Web.Mvc;

namespace Suncrest.ShippingCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InputEntry()
        {
            return View();
        }

        public ActionResult LookupCost(string zipCode, decimal weight)
        {
            var result = new ShippingCalculation(zipCode, weight);
            var model = result.Results;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CalculationResult", model);
            }
            return View("Calculation", model);
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