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
        private IShippingCostsLookup _shippingCostsLookup;
        private IShippingZonesLookup _shippingZonesLookup;
        private IConfigurationWrapper _configuration;
        private Calculator _calculator;

        public AjaxController(IShippingCostsLookup shippingCostsLookup, IShippingZonesLookup shippingZonesLookup, IConfigurationWrapper configuration)
        {
            _shippingCostsLookup = shippingCostsLookup;
            _shippingZonesLookup = shippingZonesLookup;
            _configuration = configuration;
            _calculator = new Calculator(_shippingZonesLookup, _shippingCostsLookup);
        }

        // GET: Ajax
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Clear()
        {
            return PartialView("_Empty");
        }

        public ActionResult _CalculationResult(string zipCode, decimal weight)
        {
            ICalculationResults results = _calculator.Compute(zipCode, weight);
            var model = results;
            return PartialView("_CalculationResult", model);
        }
    }
}