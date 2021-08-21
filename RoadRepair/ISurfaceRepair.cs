using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RoadRepair
{
    public interface ISurfaceRepair
    {
        double GetVolume();
        Road Road { get; }
        double Depth { get; }
    }

}
