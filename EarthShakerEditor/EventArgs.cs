using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using EarthShakerEditor.Editor;

namespace EarthShakerEditor
{
    /// <summary>
    /// Provides Point data for events.
    /// </summary>
    public class PointEventArgs : EventArgs
    {
        public Point Point { get; private set; }
        public int   X     { get { return Point.X; } }
        public int   Y     { get { return Point.Y; } }
        
        public PointEventArgs(int x, int y)
        {
            Point = new Point(x, y);
        }

        public PointEventArgs(Point point)
        {
            Point = point;
        }
    }

    /// <summary>
    /// Provides the position and buttons involved in a mouse event on the editor map.
    /// </summary>
    public class MapMouseEventArgs : EventArgs
    {
        public Point Point { get; private set; }
        public MouseButtons MouseButtons { get; private set; }

        public MapMouseEventArgs(Point point, MouseButtons mouseButtons)
        {
            Point = point;
            MouseButtons = mouseButtons;
        }
    }

    /// <summary>
    /// Provides Element data for events.
    /// </summary>
    public class ElementEventArgs : EventArgs
    {
        public Element Element { get; private set; }

        public ElementEventArgs(Element element)
        {
            Element = element;
        }
    }

    /// <summary>
    /// Provides Tool data for events.
    /// </summary>
    public class ToolEventArgs : EventArgs
    {
        public Tool Tool { get; private set; }

        public ToolEventArgs(Tool tool)
        {
            Tool = tool;
        }
    }

    /// <summary>
    /// Provides integer index data for events.
    /// </summary>
    public class IndexEventArgs : EventArgs
    {
        public int Index { get; private set; }

        public IndexEventArgs(int index)
        {
            Index = index;
        }
    }

    /// <summary>
    /// Provides zoom level data for events.
    /// </summary>
    public class ZoomEventArgs : EventArgs
    {
        public int ZoomLevel { get; private set; }

        public ZoomEventArgs(int zoomLevel)
        {
            ZoomLevel = zoomLevel;
        }
    }
    
    /// <summary>
    /// Provides Sprite data for events.
    /// </summary>
    public class SpriteEventArgs : EventArgs
    {
        public Sprite Sprite { get; private set; }

        public SpriteEventArgs(Sprite sprite)
        {
            Sprite = sprite;
        }
    }

    /// <summary>
    /// Provides Element and Sprite data for events.
    /// </summary>
    public class ElementSpriteEventArgs : EventArgs
    {
        public Element Element { get; private set; }
        public Sprite  Sprite  { get; private set; }

        public ElementSpriteEventArgs(Element element, Sprite sprite)
        {
            Element = element;
            Sprite  = sprite;
        }
    }

    /// <summary>
    /// Provides filename data for events.
    /// </summary>
    public class FilenameEventArgs : EventArgs
    {
        public string Filename { get; private set; }

        public FilenameEventArgs(string filename)
        {
            Filename = filename;
        }
    }

    /// <summary>
    /// Provides LevelProperties data for events.
    /// </summary>
    public class SaveLevelPropertiesEventArgs : EventArgs
    {
        public EditableLevelProperties Properties { get; private set; }

        public SaveLevelPropertiesEventArgs(EditableLevelProperties properties)
        {
            Properties = properties;
        }
    }
}
