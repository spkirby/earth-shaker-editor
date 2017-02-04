using System;
using System.Collections.Generic;

namespace EarthShakerEditor
{
    /// <summary>
    /// Provides an enumerable collection of Level objects.
    /// </summary>
    public class LevelCollection : System.Collections.IEnumerable
    {
        /// <summary>
        /// The number of Levels in a LevelCollection.
        /// </summary>
        public const int LevelCount = 32;


        /// <summary>
        /// Gets the Level with the specified number.
        /// </summary>
        /// <param name="number">The number of the level to return, from 1 to 32.</param>
        /// <returns>The specified Level.</returns>
        public Level this[int number]
        {
            get
            {
                if(number < 1 || number > LevelCount)
                    throw new ArgumentException("Invalid level number");

                return levels[number-1];
            }

            set
            {
                levels[number-1] = value;
            }
        }


        private Level[] levels = new Level[LevelCount];


        public LevelCollection()
        {
        }


        /// <summary>
        /// Returns an enumerator that iterates through this LevelCollection.
        /// </summary>
        /// <returns>An IEnumerator for this collection.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            for(int index = 0; index < LevelCount; index++)
                yield return levels[index];
        }
    }
}
