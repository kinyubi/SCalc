using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suncrest.ShippingCalculator.Controllers;
using Suncrest.ShippingCalculator.Models;
using System.Web.Mvc;

namespace Suncrest.ShippingCalculator.Tests.Controllers
{
    [TestClass]
    public abstract class BaseHomeControllerTest
    {
        protected HomeController controller;

        [TestMethod]
        public void TestHomeController_IndexAction()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_InputEntryAction()
        {
            ViewResult result = controller.InputEntry() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_CalculationAction()
        {
            ViewResult result = controller.Calculation("55555", 2m) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_CostsTableAction()
        {
            PartialViewResult result = controller.CostsTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_ZonesTableAction()
        {
            PartialViewResult result = controller.ZonesTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_ClearCostsTableAction()
        {
            PartialViewResult result = controller.ClearCostsTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_ClearZonesTableAction()
        {
            PartialViewResult result = controller.ClearZonesTable() as PartialViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestHomeController_AboutAction()
        {
            ViewResult result = controller.About() as ViewResult;
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
            
        [TestMethod]
        public void TestHomeController_ContactAction()
        {
            ViewResult result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }

    }

    [TestClass]
    public class DefaultHomeControllerTest : BaseHomeControllerTest
    {

        public DefaultHomeControllerTest()
        {
            IConfigurationWrapper config = new DefaultConfigurationWrapper();
            IShippingCostsLookup costsLookup = new DefaultShippingCostLookup(config);
            IShippingZonesLookup zonesLookup = new DefaultShippingZoneLookup(config);
            controller = new HomeController(costsLookup, zonesLookup, config);
        }
    }

    [TestClass]
    public class AlternateHomeControllerTest : BaseHomeControllerTest
    {

        public AlternateHomeControllerTest()
        {
            IConfigurationWrapper config = new DefaultConfigurationWrapper();
            IShippingCostsLookup costsLookup = new AlternateShippingCostsLookup();
            IShippingZonesLookup zonesLookup = new AlternateShippingZonesLookup();
            controller = new HomeController(costsLookup, zonesLookup, config);
        }
    }
}
