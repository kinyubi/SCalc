using Suncrest.ShippingCalculator.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace Suncrest.ShippingCalculator.Controllers
{
    public class HomeController : Controller
    {
        private IShippingCostsLookup _shippingCostsLookup;
        private IShippingZonesLookup _shippingZonesLookup;
        private IConfigurationWrapper _configuration;
        private Calculator _calculator;

        public HomeController(IShippingCostsLookup shippingCostsLookup, IShippingZonesLookup shippingZonesLookup, IConfigurationWrapper configuration)
        {
            _shippingCostsLookup = shippingCostsLookup;
            _shippingZonesLookup = shippingZonesLookup;
            _configuration = configuration;
            _calculator = new Calculator(_shippingZonesLookup, _shippingCostsLookup);
        }

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
            ICalculationResults results = _calculator.Compute(zipCode, weight);
            var view = View("Calculation", results);
            return view;
        }

        public ActionResult CostsTable()
        {
            var model = _shippingCostsLookup.GetAll();
            return PartialView("_CostsTable", model);
        }

        public ActionResult ZonesTable()
        {
            var model = _shippingZonesLookup.GetAll();
            return PartialView("_ZonesTable", model);
        }

        public ActionResult ClearZonesTable()
        {
            var model = new List<IShippingZoneLookupEntry>();
            return PartialView("_ZonesTable", model);
        }

        public ActionResult ClearCostsTable()
        {
            var model = new List<IShippingCostLookupEntry>();
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