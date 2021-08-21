using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadRepair;
using System;

namespace RoadRepairTests
{
    [TestClass]
    public class B_PlannerTests
    {
        [TestMethod]
        public void CalculateTime()
        {
            var planner = new Planner();
            planner.HoursOfWork = 5;
            planner.Workers = 2;
            var time = planner.GetTime();
            Assert.AreEqual(2.5, time);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Road width or length can not be zero or null")]
        public void CalculateTimeHanldesZeroValues()
        {
            var planner = new Planner();
            planner.HoursOfWork = 0;
            planner.Workers = 0;
            var time = planner.GetTime();
        }

        [TestMethod]
        public void PlanRepairForRoadWithManyPotholes()
        {
            var road = new Road {Length = 10, Width = 5};
            road.Potholes = 15;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is Resurfacing, "The repair should be a Resurface");
        }

        [TestMethod]
        public void PlanRepairForRoadWithFewPotholes()
        {
            var road = new Road { Length = 10, Width = 5 };
            road.Potholes = 2;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is PatchingRepair, "The repair should be a Patch");
        }

        [TestMethod]
        public void PlannerReturnsCorrectRepaireFor20PercentVolumn()
        {
            var road = new Road { Length = 1, Width = 5 };
            road.Potholes = 1;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is PatchingRepair, "The repair should be a Patch");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Road width or length can not be zero or null")]
        public void PlannerHandlesZeroValuesCorrectly()
        {
            var road = new Road { Length = 0, Width = 0 };
            road.Potholes = 0;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is PatchingRepair, "The repair should be a Patch");
        }

        [TestMethod]
        public void PlannerReturnsNullForZeroPotholes()
        {
            var road = new Road { Length = 10, Width = 5 };
            road.Potholes = 0;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is null, "The road doesnt need to be repaired");
        }
    }
}
