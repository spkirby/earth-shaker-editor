using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthShakerEditor
{
    public class EditableLevelProperties : ICloneable
    {
        private int    diamondsRequired;
        private int    pointsPerDiamond;
        private string levelName;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool AllDiamondsRequired { get; set; }
        
        public int DiamondsRequired
        {
            get { return diamondsRequired; }

            set
            {
                if(value < 0 || value > 99)
                    throw new ArgumentException("Invalid number for DiamondsRequired. Must be in the range 0-99");

                diamondsRequired = value;
            }
        }

        public string LevelName
        {
            get { return levelName; }

            set
            {
                string tempName = value.Trim();
                if(tempName.Length > Level.NameLength)
                    tempName = tempName.Substring(0, Level.NameLength);

                levelName = tempName.ToUpper();
            }
        }

        public int PointsPerDiamond
        {
            get { return pointsPerDiamond; }
            
            set
            {
                if(value < 0 || value > 255)
                    throw new ArgumentException("Invalid number for PointsPerDiamond. Must be in the range 0-255");
                
                pointsPerDiamond = value;
            }
        }
    }
}
