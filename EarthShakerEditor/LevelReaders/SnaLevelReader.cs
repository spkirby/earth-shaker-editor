using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor.LevelReaders
{
    /// <summary>
    /// Reads and writes Earth Shaker level data from and to a SnaFile.
    /// </summary>
    class SnaLevelReader : LevelReader
    {
        protected static int LevelDataBlockOffset = 32795;
        protected static int LevelNameBlockOffset = 22299;

        protected SnaFile snaFile;

        protected override byte[] LevelData
        {
            get
            {
                byte[] levelData = new byte[LevelDataBlockLength];
                Array.Copy(snaFile.RawData, LevelDataBlockOffset, levelData, 0, LevelDataBlockLength);
                return levelData;
            }

            set
            {
                if(value.Length != LevelDataBlockLength)
                    throw new InvalidDataException("Invalid level data");

                byte[] data = snaFile.RawData;
                Array.Copy(value, 0, data, LevelDataBlockOffset, LevelDataBlockLength);
                snaFile.RawData = data;
            }
        }

        protected override byte[] NameData
        {
            get
            {
                byte[] nameData = new byte[NameDataBlockLength];
                Array.Copy(snaFile.RawData, LevelNameBlockOffset, nameData, 0, NameDataBlockLength);
                return nameData;
            }

            set
            {
                if(value.Length != NameDataBlockLength)
                    throw new InvalidDataException("Invalid name data");

                byte[] data = snaFile.RawData;
                Array.Copy(value, 0, data, LevelNameBlockOffset, NameDataBlockLength);
                snaFile.RawData = data;
            }
        }


        /// <summary>
        /// Initializes a new instance of the LevelReaders.LevelReader class using the
        /// specified file.
        /// </summary>
        /// <param name="file">A Spectrum.SnaFile with the level data to be read or written.</param>
        public SnaLevelReader(SnaFile file)
        {
            SpectrumFile = file;
            snaFile = file;
        }
    }
}
