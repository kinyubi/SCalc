using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Controllers;
using Suncrest.ShippingCalculator.Models;
using System.Web.Mvc;
using System;

namespace Suncrest.ShippingCalculator.Tests.Controllers
{
    [TestClass]
    public abstract class BaseAjaxControllerTest
    {
        protected AjaxController controller;

        [TestMethod]
        public void TestAjaxController_IndexAction()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAjaxController_CostsTableAction()
        {
            PartialViewResult result = controller.CostsTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAjaxController_ZonesTableAction()
        {
            PartialViewResult result = controller.ZonesTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAjaxController_ClearAction()
        {
            PartialViewResult result = controller.Clear() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAjaxController_CalculationResultAction()
        {
            PartialViewResult result = controller._CalculationResult("55555", 2m) as PartialViewResult;
            Assert.IsNotNull(result);
        }
    }

    [TestClass]
    public class DefaultAjaxControllerTest : BaseAjaxControllerTest
    {
        
        public  DefaultAjaxControllerTest()
        {
            IConfigurationWrapper config = new DefaultConfigurationWrapper();
            IShippingCostsLookup costsLookup = new DefaultShippingCostLookup(config);
            IShippingZonesLookup zonesLookup = new DefaultShippingZoneLookup(config);
            controller = new AjaxController(costsLookup, zonesLookup, config);
        }
    }

    [TestClass]
    public class AlternateAjaxControllerTest : BaseAjaxControllerTest
    {

        public AlternateAjaxControllerTest()
        {
            IConfigurationWrapper config = new DefaultConfigurationWrapper();
            IShippingCostsLookup costsLookup = new AlternateShippingCostsLookup();
            IShippingZonesLookup zonesLookup = new AlternateShippingZonesLookup();
            controller = new AjaxController(costsLookup, zonesLookup, config);
        }
    }
}
