using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor.Editor
{
    public class EditorModel
    {
        #region Public properties
        /// <summary>
        /// Gets or sets the currently-selected Element.
        /// </summary>
        public Element CurrentElement { get; set; }
        
        /// <summary>
        /// Gets the currently-selected Level.
        /// </summary>
        public Level CurrentLevel
        {
            get
            {
                return Levels[CurrentLevelNumber];
            }
        }

        /// <summary>
        /// Gets or sets the currently-active level by its number.
        /// </summary>
        public int CurrentLevelNumber { get; set; }

        /// <summary>
        /// Gets or sets the currently-selected editor Tool.
        /// </summary>
        public Tool CurrentTool { get; set; }

        /// <summary>
        /// Gets or sets the currently-loaded SpectrumFile.
        /// </summary>
        public SpectrumFile File { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether or not the file has changed.
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// Gets or sets the current collection of Levels.
        /// </summary>
        public LevelCollection Levels { get; set; }

        /// <summary>
        /// Gets or sets the current redo stack
        /// </summary>
        public Stack<Action> RedoStack { get; set; }

        /// <summary>
        /// Gets or sets the current undo stack
        /// </summary>
        public Stack<Action> UndoStack { get; set; }

        /// <summary>
        /// Gets or sets the current zoom level.
        /// </summary>
        public int ZoomLevel
        {
            get { return zoomLevel; }
            
            set
            {
                if(value != 1 && value != 2)
                    throw new ArgumentException("Invalid zoom level");

                zoomLevel = value;
            }
        }
        #endregion

        #region Private variables
        protected int zoomLevel;
        #endregion

        #region Constructor
        public EditorModel()
        {
            Levels             = new LevelCollection();
            CurrentLevelNumber = 1;
            CurrentElement     = Element.Wall;
            File               = null;
            ZoomLevel          = 1;
            UndoStack          = new Stack<Action>();
            RedoStack          = new Stack<Action>();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the Element at the specified coordinates of the current Level.
        /// </summary>
        /// <param name="pos">A Point containing the coordinates of the Element to return.</param>
        /// <returns>The Element at the specified Point.</returns>
        public Element GetElement(Point pos)
        {
            return CurrentLevel.GetElement(pos);
        }

        /// <summary>
        /// Gets the Element at the specified coordinates of the current Level.
        /// </summary>
        /// <param name="x">The x-coordinate of the Element to return.</param>
        /// <param name="y">The y-coordinate of the Element to return.</param>
        /// <returns>The Element at point (x, y).</returns>
        public Element GetElement(int x, int y)
        {
            return CurrentLevel.GetElement(x, y);
        }

        /// <summary>
        /// Sets the Element at the specified coordinates of the current Level.
        /// </summary>
        /// <param name="pos">A Point containing the coordinates of the Element to set.</param>
        /// <param name="element">The type of Element to be set.</param>
        /// <returns>A SetElementResult signifying any side effects of the Element being set.</returns>
        public SetElementResult SetElement(Point pos, Element element)
        {
            return CurrentLevel.SetElement(pos, element);
        }

        /// <summary>
        /// Sets the Element at the specified coordinates of the current Level.
        /// </summary>
        /// <param name="x">The x-coordinate of the Element to set.</param>
        /// <param name="y">The y-coordinate of the Element to set.</param>
        /// <param name="element">The type of Element to be set.</param>
        /// <returns>A SetElementResult signifying any side effects of the Element being set.</returns>
        public SetElementResult SetElement(int x, int y, Element element)
        {
            return CurrentLevel.SetElement(x, y, element);
        }
        #endregion
    }
}
