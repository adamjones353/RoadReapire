namespace RoadRepair
{
    /// <summary>
    /// A resurface is where you fix the whole road, not just the parts that are damaged.
    /// </summary>
    public class Resurfacing : ISurfaceRepair
    {
        public Resurfacing(Road road)
        {
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