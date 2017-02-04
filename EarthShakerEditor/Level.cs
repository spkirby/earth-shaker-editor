using System;
using System.Drawing;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor
{
    #region SetElementResult enumeration
    /// <summary>
    /// Specifies constants for the result of the SetElement() method
    /// </summary>
    public enum SetElementResult
    {
        Success = 0,
        PlayerMoved = 1
    }
    #endregion

    public class Level
    {
        #region Public properties
        /// <summary>
        /// Gets or sets the Spectrum memory address of the Wall graphic.
        /// </summary>
        public int WallGraphicAddress  { get; set; }

        /// <summary>
        /// Gets or sets the Spectrum memory address of the Earth graphic.
        /// </summary>
        public int EarthGraphicAddress { get; set; }

        /// <summary>
        /// Gets or sets the Spectrum colour attribute of the Wall graphic.
        /// </summary>
        public SpectrumAttribute WallAttribute  { get; set; }

        /// <summary>
        /// Gets or sets the Spectrum colour attribute of the Earth graphic.
        /// </summary>
        public SpectrumAttribute EarthAttribute { get; set; }

        /// <summary>
        /// Gets or sets the Spectrum colour attribute of the Rock graphic.
        /// </summary>
        public SpectrumAttribute RockAttribute  { get; set; }

        /// <summary>
        /// Gets or sets the Spectrum colour attribute of the Door graphic.
        /// </summary>
        public SpectrumAttribute DoorAttribute  { get; set; }

        /// <summary>
        /// Gets or sets the level's map data.
        /// </summary>
        public Element[,] MapData
        {
            get { return (Element[,])mapData.Clone(); }
            
            set
            {
                for(int y=0; y < Height; y++)
                    for(int x=0; x < Width; x++)
                        SetElement(x, y, value[y,x]);
            }
        }

        /// <summary>
        /// Gets or sets a flag specifying whether or not all the diamonds are required to
        /// complete the level.
        /// </summary>
        public bool AllDiamondsRequired
        {
            get { return properties.AllDiamondsRequired;  }
            set { properties.AllDiamondsRequired = value; }
        }

        /// <summary>
        /// Gets or sets the number of diamonds required to finish the level. This value only
        /// has meaning if AllDiamondsRequired is false.
        /// </summary>
        public int DiamondsRequired
        {
            get { return properties.DiamondsRequired;  }
            set { properties.DiamondsRequired = value; }
            
        }

        /// <summary>
        /// Gets or sets the number of points awarded per diamond collected.
        /// </summary>
        public int PointsPerDiamond
        {
            get { return properties.PointsPerDiamond;  }
            set { properties.PointsPerDiamond = value; }
        }

        /// <summary>
        /// Gets the player's start position for this level. Returns Level.InvalidStartPosition if
        /// there is no player.
        /// </summary>
        public Point StartPosition
        {
            get { return startPosition; }
            private set
            {
                if(     (value.X < 0 || value.X >= Width || value.Y < 0 || value.Y >= Height)
                    && !(value.X == -1 && value.Y == -1))
                    throw new ArgumentException("Invalid Point for StartPosition. Must be in the range (0,0)-(" + (Width - 1) + "," + (Height - 1) + ")");

                startPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the level's name.
        /// </summary>
        public string Name
        {
            get { return properties.LevelName;  }
            set { properties.LevelName = value; }
        }

        /// <summary>
        /// Gets or sets this level's editable properties using an EditableLevelProperties object.
        /// </summary>
        public EditableLevelProperties LevelProperties
        {
            get { return (EditableLevelProperties)properties.Clone(); }
            set { properties = (EditableLevelProperties)value.Clone(); }
        }
        #endregion

        #region Public constants
        /// <summary>
        /// The width of the map, in tiles.
        /// </summary>
        public const int Width  = 30;

        /// <summary>
        /// The height of the map, in tiles.
        /// </summary>
        public const int Height = 20;

        /// <summary>
        /// The maximum length of the level's name.
        /// </summary>
        public const int NameLength = 20;

        /// <summary>
        /// A Point representing an invalid start position.
        /// </summary>
        public static readonly Point InvalidStartPosition = new Point(-1, -1);
        #endregion

        #region Private variables
        private Element[,] mapData = new Element[Height, Width];
        private Point startPosition;
        private EditableLevelProperties properties;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Level class.
        /// </summary>
        public Level()
        {
            LevelProperties = new EditableLevelProperties();
            StartPosition = InvalidStartPosition;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Clears the level's entire map to Element.Earth.
        /// </summary>
        public void ClearMap()
        {
            for(int y=0; y < Height; y++)
                for(int x=0; x < Width; x++)
                    mapData[y, x] = Element.Earth;
            
            StartPosition = InvalidStartPosition;
        }

        /// <summary>
        /// Gets the Element at the specified coordinates of this level's map.
        /// </summary>
        /// <param name="pos">A Point containing the coordinates of the Element to return.</param>
        /// <returns>The Element at the specified Point.</returns>
        public Element GetElement(Point pos)
        {
            return GetElement(pos.X, pos.Y);
        }

        /// <summary>
        /// Gets the Element at the specified coordinates of this level's map.
        /// </summary>
        /// <param name="x">The x-coordinate of the Element to return.</param>
        /// <param name="y">The y-coordinate of the Element to return.</param>
        /// <returns>The Element at point (x, y).</returns>
        public Element GetElement(int x, int y)
        {
            return mapData[y, x];
        }

        /// <summary>
        /// Sets the Element at the specified coordinates of this level's map.
        /// </summary>
        /// <param name="position">A Point containing the coordinates of the Element to set.</param>
        /// <param name="element">The type of Element to be set.</param>
        /// <returns>A SetElementResult signifying any side effects of the Element being set.</returns>
        public SetElementResult SetElement(Point position, Element element)
        {
            return SetElement(position.X, position.Y, element);
        }

        /// <summary>
        /// Sets the Element at the specified coordinates of this level's map.
        /// </summary>
        /// <param name="x">The x-coordinate of the Element to set.</param>
        /// <param name="y">The y-coordinate of the Element to set.</param>
        /// <param name="element">The type of Element to be set.</param>
        /// <returns>A SetElementResult signifying any side effects of the Element being set.</returns>
        public SetElementResult SetElement(int x, int y, Element element)
        {
            SetElementResult result = SetElementResult.Success;

            // If overwriting the player then delete start position
            Element currElem = GetElement(x, y);
            if(currElem == Element.PlayerA || currElem == Element.PlayerB)
            {
                StartPosition = InvalidStartPosition;
                result = SetElementResult.PlayerMoved;
            }

            // If drawing player, delete old player and set new start position
            if(element == Element.PlayerA || element == Element.PlayerB)
            {
                if(StartPosition != InvalidStartPosition)
                    SetElement(StartPosition, Element.Space);

                StartPosition = new Point(x, y);
                result = SetElementResult.PlayerMoved;
            }

            mapData[y, x] = element;

            return result;
        }
        #endregion
    }
}