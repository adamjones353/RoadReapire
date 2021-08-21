using System;

namespace RoadRepair
{
    /// <summary>
    /// A resurface is where you fix the whole road, not just the parts that are damaged.
    /// </summary>
    public class Resurfacing : ISurfaceRepair
    {
        public Resurfacing(Road road)
        {
            if(road.Width == 0 || road.Length == 0)
            {
                throw new ArgumentException("Road width or length cannot be zero");
            }

            Road = road;         
            Depth = 0.1;
        }

        public Road Road { get; }
        public double Depth { get; }

        public double GetVolume()
        {
            var volume = Road.Width * Road.Length * Depth;
            return volume;
        }
    }
}