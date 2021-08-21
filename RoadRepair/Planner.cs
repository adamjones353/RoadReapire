using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RoadRepair
{
    public class Planner
    {
        /// <summary>
        /// The total number of hours needed to complete the work.
        /// </summary>
        public double HoursOfWork { get; set; }

        /// <summary>
        /// The number of people available to do the work
        /// </summary>
        public double Workers { get; set; }

        /// <summary>
        /// The time to complete the work, using all the workers.
        /// </summary>
        /// <returns>The number of hours to complete the work.</returns>
        public double GetTime()
        {
            double time = HoursOfWork / Workers;
            return time;
        }

        /// <summary>
        /// Return the correct type of repair (either a patch or a resurface) depending on
        /// the density of potholes.
        /// </summary>
        /// <param name="road">A road needing repair</param>
        /// <returns>Either a PatchingRepair or a Resurfacing</returns>
        public ISurfaceRepair SelectRepairType(Road road)
        {
            double roadSurfaceArea = road.Width * road.Length;

            var potholeDensity = (road.Potholes / roadSurfaceArea) * 100;

            if (potholeDensity > 20)
            {
                return new Resurfacing(road);
            }

            return new PatchingRepair(road);
        }

        /// <summary>
        /// Calculate the total volume of all the repairs for a list of roads that need repairs.
        /// </summary>
        /// <param name="roads">A list of roads needing repairs</param>
        /// <returns>The total volume of all the repairs</returns>
        public double GetVolumeOfRepairs(List<Road> roads)
        {
            return GetVolumesOfRepaires(roads).Sum(x => x.GetVolume());
        }

        public List<ISurfaceRepair> GetVolumesOfRepaires(List<Road> roads)
        {
            return roads.Select(x => {
                return SelectRepairType(x);
            }).ToList();
        }

        /// <summary>
        /// When there is not enough material available to repair all the roads,
        /// select a subset of the roads so that the volume of repairs is less than or equal to the available material.
        /// </summary>
        /// <param name="roads">A list of roads needing repairs</param>
        /// <param name="availableMaterial">The volume of material available for repairs</param>
        /// <returns>A subset of roads that can be repaired with the available material</returns>
        public List<Road> SelectRoadsToRepair(List<Road> roads, double availableMaterial)
        {
            List<ISurfaceRepair> roadsToRepaire = new List<ISurfaceRepair>();

            var temp = GetVolumesOfRepaires(roads).OrderBy(x => x.Road.Potholes);

            //order by decs so that we fill in as many as possible. 
            foreach (ISurfaceRepair surfaceRepair in GetVolumesOfRepaires(roads).OrderByDescending(x => x.Road.Potholes))
            {
                roadsToRepaire.Add(surfaceRepair);

                if (roadsToRepaire.Sum(x => x.GetVolume()) > availableMaterial)
                {
                    roadsToRepaire.RemoveAt(roadsToRepaire.Count - 1);
                }
            }

            return roadsToRepaire.Select(x => x.Road).ToList();
        }
    }

}
