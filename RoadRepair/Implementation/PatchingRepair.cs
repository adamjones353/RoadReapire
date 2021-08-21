namespace RoadRepair
{
    /// <summary>
    /// A patch is where you fix just the part of the road that is damaged.
    /// </summary>
    public class PatchingRepair : ISurfaceRepair
    {
        public PatchingRepair(Road road)
        {
            Road = road;
            Depth = 0.1;
        }

        public Road Road { get; }

        public double Depth { get; }

        public double GetVolume()
        {
            // Assume that every patch has a 1 x 1 area
            var volume = Road.Potholes * Depth;
            return volume;
        }

    }
}
