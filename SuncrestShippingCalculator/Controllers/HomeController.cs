using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
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

        public ActionResult Calculation(string zipCode, decimal weight)
        {
            var result = new ShippingCalculation(zipCode, weight);
            var model = result.Results;
            return View("Calculation", model);
        }

        public ActionResult CostsTable()
        {
            IShippingCostsServiceClient costClient = ServiceFactory.GetCostsClient();
            var model = costClient.GetAll();
            return PartialView("_CostsTable", model);
        }

        public ActionResult ZonesTable()
        {
            IShippingZonesServiceClient zonesClient = ServiceFactory.GetZonesClient();
            var model = zonesClient.GetAll();
            return PartialView("_ZonesTable", model);
        }

        public ActionResult ClearZonesTable()
        {
            var model = new List<ShippingZone>();
            return PartialView("_ZonesTable", model);
        }

        public ActionResult ClearCostsTable()
        {
            var model = new List<ShippingCost>();
            return PartialView("_CostsTable", model);
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