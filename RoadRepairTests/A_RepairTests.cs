using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadRepair;
using System;

namespace RoadRepairTests
{
    [TestClass]
    public class A_RepairTests
    {
        [TestMethod]
        public void CalculatePatchVolume()
        {
            var road = new Road { Length = 3, Width = 1.5, Potholes = 4};
            var patch = new PatchingRepair(road);
            var volume = patch.GetVolume();
            Assert.AreEqual(0.4, volume);
        }
        
        [TestMethod]
        public void CalculateResurfaceVolume()
        {
            var road = new Road { Length = 3, Width = 1.5, Potholes = 4 };
            var resurface = new Resurfacing(road);
            var volume = resurface.GetVolume();
            Assert.AreEqual(0.45, volume);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Road width or length can not be zero or null")]
        public void ZeroDataHandledCorrectly()
        {
            var road = new Road { Length = 0, Width = 0, Potholes = 0 };
            new Resurfacing(road);
        }
    }
}
