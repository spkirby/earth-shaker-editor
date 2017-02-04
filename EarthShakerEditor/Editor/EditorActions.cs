using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EarthShakerEditor.Editor
{
    /// <summary>
    /// Represents the undoable action of setting an Element.
    /// </summary>
    public class SetElementAction : Action
    {
        public Point   Point            { get; set; }
        public Element OldElement       { get; set; }
        public Element NewElement       { get; set; }
        public Point   OldStartPosition { get; set; }

        public SetElementAction(Point point, Point oldStart, Element oldElem, Element newElem)
        {
            Point            = point;
            OldStartPosition = oldStart;
            OldElement       = oldElem;
            NewElement       = newElem;
        }
    }

    /// <summary>
    /// Represents the undoable action of using the Fill tool.
    /// </summary>
    public class FillElementAction : Action
    {
        public Point              StartPoint { get; set; }
        public IEnumerable<Point> Points     { get; set; }
        public Element            OldElement { get; set; }
        public Element            NewElement { get; set; }

        public FillElementAction(Point startPoint, IEnumerable<Point> points, Element oldElem, Element newElem)
        {
            StartPoint = startPoint;
            Points     = points;
            OldElement = oldElem;
            NewElement = newElem;
        }
    }

    /// <summary>
    /// Represents the undoable action of setting an Element's Sprite.
    /// </summary>
    public class SetSpriteAction : Action
    {
        public Element Element   { get; set; }
        public Sprite  OldSprite { get; set; }
        public Sprite  NewSprite { get; set; }
        
        public SetSpriteAction(Element element, Sprite oldSprite, Sprite newSprite)
        {
            Element   = element;
            OldSprite = oldSprite;
            NewSprite = newSprite;
        }
    }

    /// <summary>
    /// Represents the undoable action of altering a Level's properties.
    /// </summary>
    public class SetLevelPropertiesAction : Action
    {
        public EditableLevelProperties OldLevelProperties { get; set; }
        public EditableLevelProperties NewLevelProperties { get; set; }

        public SetLevelPropertiesAction(EditableLevelProperties oldProps, EditableLevelProperties newProps)
        {
            OldLevelProperties = oldProps;
            NewLevelProperties = newProps;
        }
    }

    /// <summary>
    /// Represents the undoable action of changing the current Level.
    /// </summary>
    public class SetLevelAction : Action
    {
        public int OldLevelNumber { get; set; }
        public int NewLevelNumber { get; set; }

        public SetLevelAction(int oldLevel, int newLevel)
        {
            OldLevelNumber = oldLevel;
            NewLevelNumber = newLevel;
        }
    }

    /// <summary>
    /// Represents the undoable action of clearing a Level's map.
    /// </summary>
    public class ClearLevelAction : Action
    {
        public Element[,] OldMapData { get; set; }
        public Element[,] NewMapData { get; set; }

        public ClearLevelAction(Element[,] oldMapData, Element[,] newMapData)
        {
            OldMapData = oldMapData;
            NewMapData = newMapData;
        }
    }
}
