using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthShakerEditor
{
    /// <summary>
    /// Specifies constants for Earth Shaker's game elements.
    /// </summary>
    public enum Element
    {
        Space        = 0,
        Door         = 1,
        Rock         = 2,
        PlayerA      = 3,
        PlayerB      = 4,
        Wall         = 5,
        Earth        = 6,
        Diamond      = 7,
        JellyBean    = 8,
        Forcefield   = 9,
        GravityStick = 10,
        Monitor      = 11,
        Elixir       = 12,
        Teleport     = 13,
        Bubble       = 14,
        Fire         = 15,
        Last         = 15  // Last element, used in for loops
    };
}
