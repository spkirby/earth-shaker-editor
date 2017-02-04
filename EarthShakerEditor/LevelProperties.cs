using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthShakerEditor
{
    /// <summary>
    /// A set of properties to define the name, number of diamonds, and points per
    /// diamond for a level.
    /// </summary>
    public class LevelProperties : ICloneable
    {
        #region Public properties
        /// <summary>
        /// Gets or sets a flag specifying whether or not all the diamonds are required to
        /// complete the level.
        /// </summary>
        public bool AllDiamondsRequired { get; set; }
        
        /// <summary>
        /// Gets or sets the number of diamonds required to finish the level. This value only
        /// has meaning if AllDiamondsRequired is false.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the level's name.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the number of points awarded per diamond collected.
        /// </summary>
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
        #endregion

        #region Private variables
        private int    diamondsRequired;
        private int    pointsPerDiamond;
        private string levelName;
        #endregion

        #region Public methods
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}
