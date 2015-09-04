using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Suncrest.ShippingCalculator.Models;

namespace Suncrest.ShippingCalculator.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Clear()
        {
            return PartialView("_Empty");
        }



        public ActionResult _CalculationResult(string zipCode, decimal weight)
        {
            var result = new ShippingCalculation(zipCode, weight);
            var model = result.Results;
            return PartialView("_CalculationResult", model);
        }
    }
}