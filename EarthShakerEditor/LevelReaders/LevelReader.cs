using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor.LevelReaders
{
    /// <summary>
    /// An abstract base class that provides functionality to read and write
    /// Earth Shaker level data from and to subclasses of SpectrumFile.
    /// </summary>
    public abstract class LevelReader
    {
        #region Note on Earth Shaker binary data
        // Note: Earth Shaker's binary data is split into two separate blocks:
        //       a block of 32 level data structures (314 bytes each) and a block
        //       of 32 level names (20 bytes each), hence why there are separate
        //       methods for converting each block.
        #endregion

        #region Constants
        protected const int BytesPerLevel = 314;
        protected const int BytesPerName  = 20;
        protected const int LevelDataBlockLength = 10048;
        protected const int NameDataBlockLength  = 640;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the SpectrumFile used for reading and writing.
        /// </summary>
        public SpectrumFile SpectrumFile { get; protected set; }
        #endregion

        #region Abstract properties
        /// <summary>
        /// Gets or sets the file's level data block. The subclass must make whatever conversions
        /// are necessary to supply this data in basic, uncompressed form.
        /// </summary>
        protected abstract byte[] LevelData  { get; set; }
        
        /// <summary>
        /// Gets or sets the file's name data block. The subclass must make whatever conversions
        /// are necessary to supply this data in basic, uncompressed form.
        /// </summary>
        protected abstract byte[] NameData   { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates and returns a LevelReader for the provided SpectrumFile.
        /// </summary>
        /// <param name="file">The SpectrumFile to read.</param>
        /// <returns>A LevelReader capable of reading the specified SpectrumFile.</returns>
        public static LevelReader Create(SpectrumFile file)
        {
            if(file is SnaFile)
                return new SnaLevelReader(file as SnaFile);
            else
                throw new InvalidSpectrumFileException("Unsupported SpectrumFile type");
        }

        /// <summary>
        /// Reads the level data from the file and returns it as a LevelCollection.
        /// </summary>
        /// <returns>A LevelCollection created from this reader's file.</returns>
        public LevelCollection ReadLevels()
        {
            LevelCollection levels = new LevelCollection();

            // Cache LevelData and NameData (supplied by subclass)
            byte[] levelData = LevelData;
            byte[] nameData  = NameData;

            // Buffers for one level and one name
            byte[] levelBuffer = new byte[BytesPerLevel];
            byte[] nameBuffer  = new byte[BytesPerName];

            for(int i=0; i < LevelCollection.LevelCount; i++)
            {
                Array.Copy(levelData, (i * BytesPerLevel), levelBuffer, 0, BytesPerLevel);
                Array.Copy(nameData, (i * BytesPerName), nameBuffer, 0, BytesPerName);
                levels[i+1] = createLevel(levelBuffer, nameBuffer);
            }

            return levels;
        }

        /// <summary>
        /// Writes the level data from a LevelCollection into this reader's file.
        /// </summary>
        /// <param name="levels">The LevelCollection to write.</param>
        /// <remarks>The SpectrumFile's Save() method must be invoked to actually write the data to disk.</remarks>
        public void WriteLevels(LevelCollection levels)
        {
            // Cache LevelData and NameData (supplied by subclass)
            byte[] workingLevelData = LevelData;
            byte[] workingNameData  = NameData;

            // Buffers for one level and one name
            byte[] levelBuffer;
            byte[] nameBuffer;

            for(int i=0; i < LevelCollection.LevelCount; i++)
            {
                levelBuffer = createLevelBinary(levels[i+1]);
                nameBuffer  = createNameBinary(levels[i+1]);

                Array.Copy(levelBuffer, 0, workingLevelData, (i * BytesPerLevel), BytesPerLevel);
                Array.Copy(nameBuffer, 0, workingNameData, (i * BytesPerName), BytesPerName);
            }

            // Save changes
            NameData  = workingNameData;
            LevelData = workingLevelData;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Returns a Level object created from the raw binary level and name data.
        /// </summary>
        /// <param name="rawLevelData">The raw binary level data for the level.</param>
        /// <param name="rawLevelName">The raw binary name data for the level.</param>
        /// <returns>A Level object representing the level.</returns>
        protected Level createLevel(byte[] rawLevelData, byte[] rawLevelName)
        {
            Level level = new Level();
            int totalDiamonds = 0;

            // Bytes 0-299: Map data
            // Each byte is two elements, four bits each
            // Lower four bits = left-hand element
            // Upper four bits = right-hand element
            int offset = 0;
            for(int y = 0; y < Level.Height; y++)
            {
                for(int x = 0; x < Level.Width; x += 2)
                {
                    Element elemLeft  = (Element)(rawLevelData[offset] & 0x0F);
                    Element elemRight = (Element)((rawLevelData[offset] & 0xF0) >> 4);
                    level.SetElement(x, y, elemLeft);
                    level.SetElement(x+1, y, elemRight);

                    if(elemLeft == Element.Diamond)
                        totalDiamonds++;
                    if(elemRight == Element.Diamond)
                        totalDiamonds++;

                    offset++;
                }
            }

            // Bytes 300-313: Extra data
            // 300 = Diamonds Required
            // 301 = Points Per Diamond
            // 302, 303 = Offset of player start position (([303] * 256) + [302])
            // 304 = Diamonds Required (BCD: tens ) - Not used here
            // 305 = Diamonds Required (BCD: units) - Not used here
            // 306, 307 = Earth Graphic Address (([307] * 256) + [306])
            // 308, 309 = Wall  Graphic Address (([309] * 256) + [308])
            // 310 = Door Attribute
            // 311 = Rock Attribute
            // 312 = Wall Attribute
            // 313 = Earth Attribute
            level.DiamondsRequired = (int)rawLevelData[300];
            level.PointsPerDiamond = (int)rawLevelData[301];
            
            // We don't have to manually set StartPosition. Setting the Player Element above
            // does this for us.
            /*int playerTile = (int)rawLevelData[302] + ((int)rawLevelData[303] * 256);
            int playerX = playerTile % Level.Width;
            int playerY = playerTile / Level.Width;
            level.StartPosition    = new Point(playerX, playerY);*/

            level.EarthGraphicAddress = (int)rawLevelData[306] + ((int)rawLevelData[307] * 256);
            level.WallGraphicAddress  = (int)rawLevelData[308] + ((int)rawLevelData[309] * 256);

            level.DoorAttribute  = new SpectrumAttribute(rawLevelData[310]);
            level.RockAttribute  = new SpectrumAttribute(rawLevelData[311]);
            level.WallAttribute  = new SpectrumAttribute(rawLevelData[312]);
            level.EarthAttribute = new SpectrumAttribute(rawLevelData[313]);

            level.Name = System.Text.Encoding.ASCII.GetString(rawLevelName);
            level.AllDiamondsRequired = (level.DiamondsRequired == totalDiamonds);

            return level;
        }

        /// <summary>
        /// Returns the raw binary level data for the specified Level.
        /// </summary>
        /// <param name="level">The Level to create raw level data for.</param>
        /// <returns>The raw level data for the specified Level.</returns>
        protected byte[] createLevelBinary(Level level)
        {
            byte[] levelData = new byte[BytesPerLevel];
            int totalDiamonds = 0;

            // Bytes 0-299: Map data
            int offset = 0;
            for(int y = 0; y < Level.Height; y++)
            {
                for(int x = 0; x < Level.Width; x += 2)
                {
                    int  elemLeft  = (int)level.GetElement(x, y);
                    int  elemRight = (int)level.GetElement(x + 1, y);
                    byte elemByte  = (byte)(elemLeft + (elemRight << 4));

                    if(elemLeft == (int)Element.Diamond)
                        totalDiamonds++;
                    if(elemRight == (int)Element.Diamond)
                        totalDiamonds++;

                    levelData[offset] = elemByte;
                    offset++;
                }
            }

            // Bytes 300-313: Extra data
            if(level.AllDiamondsRequired)
                level.DiamondsRequired = Math.Min(totalDiamonds, 99);

            levelData[300] = (byte)level.DiamondsRequired;
            levelData[301] = (byte)level.PointsPerDiamond;
            
            Point startPos = level.StartPosition;
            int playerTile = (startPos == Level.InvalidStartPosition)
                                ? 0
                                : startPos.X + (startPos.Y * Level.Width);
            levelData[302] = (byte)(playerTile % 256);
            levelData[303] = (byte)(playerTile / 256);

            levelData[304] = (byte)(level.DiamondsRequired / 10);
            levelData[305] = (byte)(level.DiamondsRequired % 10);

            levelData[306] = (byte)(level.EarthGraphicAddress % 256);
            levelData[307] = (byte)(level.EarthGraphicAddress / 256);

            levelData[308] = (byte)(level.WallGraphicAddress % 256);
            levelData[309] = (byte)(level.WallGraphicAddress / 256);

            levelData[310] = level.DoorAttribute.AttributeByte;
            levelData[311] = level.RockAttribute.AttributeByte;
            levelData[312] = level.WallAttribute.AttributeByte;
            levelData[313] = level.EarthAttribute.AttributeByte;

            return levelData;
        }

        /// <summary>
        /// Returns the raw binary name data for the specified Level.
        /// </summary>
        /// <param name="level">The Level to create raw name data for.</param>
        /// <returns>The raw name data for the specified Level.</returns>
        protected byte[] createNameBinary(Level level)
        {
            string name = level.Name;
            // Names must be padded to 20 bytes with spaces (0x20)
            byte[] rawLevelName = Enumerable.Repeat<byte>(0x20, Level.NameLength).ToArray();

            if(name.Length > Level.NameLength)
                name = name.Substring(0, Level.NameLength);

            ASCIIEncoding.ASCII.GetBytes(name.ToUpper()).CopyTo(rawLevelName, 0);

            return rawLevelName;
        }
        #endregion
    }
}
