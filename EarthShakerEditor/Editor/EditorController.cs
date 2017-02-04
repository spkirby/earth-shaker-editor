using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using EarthShakerEditor.Spectrum;
using EarthShakerEditor.LevelReaders;

namespace EarthShakerEditor.Editor
{
    public class EditorController
    {
        #region Public properties
        /// <summary>
        /// Gets the EditorModel in use by the controller.
        /// </summary>
        public EditorModel Model { get; private set; }

        /// <summary>
        /// Gets the EditorView in use by the controller.
        /// </summary>
        public EditorView  View  { get; private set; }
        #endregion

        #region Private variables
        protected static string RegistryPath = @"Software\GooBunny\Earth Shaker Editor";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the EditorController class.
        /// </summary>
        /// <param name="model">An EditorModel that the controller will use.</param>
        /// <param name="view">An EditorView that the controller will use.</param>
        public EditorController(EditorModel model, EditorView view)
        {
            Model = model;
            View  = view;

            View.FormClosing += new FormClosingEventHandler(View_FormClosing);

            View.MapMouseClicked      += new EventHandler<MapMouseEventArgs>(View_MapMouseClicked);
            View.MapMouseMoved        += new EventHandler<MapMouseEventArgs>(View_MapMouseMoved);
            View.MapMouseDown         += new EventHandler<MapMouseEventArgs>(View_MapMouseDown);
            View.ElementChanged       += new EventHandler<ElementEventArgs>(View_ElementSelected);
            View.ToolChanged          += new EventHandler<ToolEventArgs>(View_ToolChanged);
            View.LevelChanged         += new EventHandler<IndexEventArgs>(View_LevelChanged);
            View.ElementSpriteChanged += new EventHandler<ElementSpriteEventArgs>(View_ElementSpriteChanged);
            View.ZoomLevelChanged     += new EventHandler<ZoomEventArgs>(View_ZoomLevelChanged);
            View.Undo                 += new EventHandler(View_Undo);
            View.Redo                 += new EventHandler(View_Redo);
            View.ClearLevel           += new EventHandler(View_ClearLevel);

            View.NewFile        += new EventHandler(View_NewFile);
            View.OpenFile       += new EventHandler<FilenameEventArgs>(View_OpenFile);
            View.SaveFile       += new EventHandler(View_SaveFile);
            View.SaveFileAs     += new EventHandler<FilenameEventArgs>(View_SaveFileAs);
            View.ShowProperties += new EventHandler(View_ShowProperties);
            View.SaveProperties += new EventHandler<SaveLevelPropertiesEventArgs>(View_SaveProperties);
            
            initializeModel();
            initializeView();

            applySavedSettings();
        }
        #endregion

        #region Initialization
        protected void applySavedSettings()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(RegistryPath, true);

            if(registryKey != null)
            {
                try
                {
                    setZoomLevel((int)registryKey.GetValue("Zoom", 1));
                    applySavedWindowSettings((string)registryKey.GetValue("Window"));
                }
                catch
                {
                    // If there are any problems with the saved data, just delete it so it
                    // can be recreated on exit.
                    registryKey.DeleteValue("Zoom");
                    registryKey.DeleteValue("Window");
                }
            }
        }

        protected void applySavedWindowSettings(string windowState)
        {
            if(windowState != null)
            {
                string[] parts = windowState.Split(new[] { ',' });
                if(parts.Length != 5)
                    throw new ArgumentException("Invalid window state");

                View.Location    = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
                View.Size        = new Size(int.Parse(parts[2]), int.Parse(parts[3]));
                        
                if(int.Parse(parts[4]) == 1)
                    View.WindowState = FormWindowState.Maximized;
            }
        }

        protected void saveSettings()
        {
            int windowMax = (View.WindowState == FormWindowState.Maximized ? 1 : 0);

            string windowState = String.Format("{0},{1},{2},{3},{4}", View.Location.X,
                                                                      View.Location.Y,
                                                                      View.Size.Width,
                                                                      View.Size.Height,
                                                                      windowMax);
            RegistryKey homeKey = Registry.CurrentUser.CreateSubKey(RegistryPath);
            homeKey.SetValue("Zoom", Model.ZoomLevel, RegistryValueKind.DWord);
            homeKey.SetValue("Window", windowState, RegistryValueKind.String);
        }

        protected void initializeModel()
        {
            newFile();
        }

        protected void initializeView()
        {
            resetView();
        }
        #endregion

        #region Protected methods
        protected void reset()
        {
            Model.CurrentLevelNumber = 1;
            Model.CurrentElement     = Element.Wall;
            Model.CurrentTool        = Tool.Draw;
            Model.HasChanged         = false;
            Model.UndoStack          = new Stack<Action>();
            Model.RedoStack          = new Stack<Action>();
            resetView();
        }

        protected void resetView()
        {
            int currentLevel = Model.CurrentLevelNumber;
            View.SetLevelNames(getLevelNamesList());
            View.SetSelectedElement(Model.CurrentElement);
            View.SetSelectedTool(Model.CurrentTool);
            doSetLevel(Model.CurrentLevelNumber); // Sets current level in View
            updateUndoRedoView();
        }

        protected void pickElement(Point point)
        {
            Model.CurrentElement = Model.GetElement(point);
            View.SetSelectedElement(Model.CurrentElement);
        }

        protected void setZoomLevel(int zoomLevel)
        {
            Model.ZoomLevel = zoomLevel;
            View.SetZoomLevel(Model.ZoomLevel);
            View.RedrawMap(Model.CurrentLevel);
        }

        /// <summary>
        /// If changes have been made, asks the user if they wish to save them.
        /// </summary>
        /// <returns>True if the operation should continue, false if the user wants to abort.</returns>
        protected bool querySaveChanges()
        {
            bool continueOperation = true;

            if(Model.HasChanged)
            {
                DialogResult result = View.ShowUnsavedChangesDialog();
                if(result == DialogResult.Yes)
                    saveFile();
                else if(result == DialogResult.Cancel)
                    continueOperation = false;
            }

            return continueOperation;
        }

        protected void newFile()
        {
            if(querySaveChanges())
            {
                Model.File      = new SnaFile(Resources.BaseSNA);
                Model.Levels = LevelReader.Create(Model.File).ReadLevels();
                reset();
            }
        }

        protected void openFile(string filename)
        {
            if(querySaveChanges())
            {
                try
                {
                    Model.File   = SpectrumFile.Open(filename);
                    Model.Levels = LevelReader.Create(Model.File).ReadLevels();
                    reset();
                }
                catch(InvalidSpectrumFileException ex)
                {
                    View.ShowErrorMessage("Unsupported File Format", ex.Message);
                }
                catch(Exception ex)
                {
                    string message = String.Format("An error occurred while saving {0}:\n\n{1}",
                                                   Path.GetFileName(filename),
                                                   ex.Message);
                    View.ShowErrorMessage("Open Error", message);
                }
            }
        }

        protected void saveFile()
        {
            if(Model.File.Filename == "")
            {
                string filename = View.ShowSaveDialog();

                if(filename == null)
                    return; // Abandon save
                else
                    Model.File.Filename = filename;
            }

            try
            {
                LevelReader.Create(Model.File).WriteLevels(Model.Levels);
                Model.File.Save();
                Model.HasChanged = false;
            }
            catch(Exception ex)
            {
                string message = String.Format("An error occured while saving {0}:\n\n{1}",
                                               Path.GetFileName(Model.File.Filename),
                                               ex.Message);
                View.ShowErrorMessage("Save Error", message);
            }
        }

        protected List<string> getLevelNamesList()
        {
            List<string> levelNames = new List<string>(LevelCollection.LevelCount);

            foreach(Level level in Model.Levels)
                levelNames.Add(level.Name);

            return levelNames;
        }

        protected void updateUndoRedoView()
        {
            View.SetUndoEnabled(Model.UndoStack.Count > 0);
            View.SetRedoEnabled(Model.RedoStack.Count > 0);
        }
        #endregion

        #region Actions
        // Actions are operations that can be undone and redone. They are structured in
        // the following manner:
        //
        // action()     : called when user initiates action; performs validation, clears redo stack, updates undo stack
        // doAction()   : actually performs the action
        // undoAction() : calls doAction() with params appropriate to reverse the action
        // redoAction() : calls doAction() and updates undo stack

        //
        // Draw Element
        //
        protected void drawElement(Point point)
        {
            drawElement(point, Model.CurrentElement);
        }

        protected void drawElement(Point point, Element element)
        {
            Element oldElem = Model.GetElement(point);
            Point playerPos = Model.CurrentLevel.StartPosition;

            if(oldElem == element)
                return;

            doDrawElement(point, element);

            completeAction(new SetElementAction(point, playerPos, oldElem, element));
        }

        protected void doDrawElement(Point point, Element element)
        {
            if(Model.SetElement(point, element) == SetElementResult.PlayerMoved)
                View.RedrawMap(Model.CurrentLevel);
            else
                View.DrawElement(point, Model.GetElement(point));
        }

        protected void undoDrawElement(SetElementAction action)
        {
            doDrawElement(action.Point, action.OldElement);

            if(action.OldStartPosition != Level.InvalidStartPosition)
                doDrawElement(action.OldStartPosition, Element.PlayerA);

            completeUndoAction();
        }

        protected void redoDrawElement(SetElementAction action)
        {
            doDrawElement(action.Point, action.NewElement);
            completeRedoAction(action);
        }


        //
        // Fill Element
        //
        protected void fillElement(Point point)
        {
            fillElement(point, Model.CurrentElement);
        }

        protected void fillElement(Point point, Element element)
        {
            Element newElem = element;
            Element oldElem = Model.GetElement(point);

            if(newElem == Element.PlayerA || newElem == Element.PlayerB ||
               oldElem == Element.PlayerA || oldElem == Element.PlayerB    )
            {
                drawElement(point);
                return;
            }

            if(newElem == oldElem)
                return;

            Point nextPoint;
            Stack<Point> points        = new Stack<Point>(); // Working stack
            Stack<Point> changedPoints = new Stack<Point>(); // Stack of all changed points

            Model.SetElement(point, newElem); // Set starting square
            points.Push(point);               // Push starting square

            do
            {
                nextPoint = points.Pop();
                changedPoints.Push(nextPoint);
                doFillElement(nextPoint, points, oldElem, newElem);
            }
            while(points.Count > 0);

            View.RedrawMap(Model.CurrentLevel);

            completeAction(new FillElementAction(point, changedPoints, oldElem, newElem));
        }

        protected void doFillElement(Point startPoint, Stack<Point> points, Element oldElem, Element newElem)
        {
            List<Point> offsets = new List<Point>()
            {
                new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0)
            };

            foreach(Point offset in offsets)
            {
                Point destPoint = new Point(startPoint.X + offset.X, startPoint.Y + offset.Y);

                if(destPoint.X >= 0 && destPoint.X < Level.Width &&
                   destPoint.Y >= 0 && destPoint.Y < Level.Height )
                {
                    if(Model.GetElement(destPoint) == oldElem)
                    {
                        Model.SetElement(destPoint, newElem);
                        points.Push(destPoint);
                    }
                }
            }
        }

        protected void undoFillElement(FillElementAction action)
        {
            foreach(Point point in action.Points)
                Model.SetElement(point, action.OldElement);

            View.RedrawMap(Model.CurrentLevel);
            completeUndoAction();
        }

        protected void redoFillElement(FillElementAction action)
        {
            foreach(Point point in action.Points)
                Model.SetElement(point, action.NewElement);

            View.RedrawMap(Model.CurrentLevel);
            completeRedoAction(action);
        }


        //
        // Set Sprite
        //
        protected void setSprite(Element element, Sprite sprite)
        {
            Sprite oldSprite = SpriteDatabase.GetSprite(Model.CurrentLevel, element);
            doSetSprite(element, sprite);
            completeAction(new SetSpriteAction(element, oldSprite, sprite));
        }

        protected void doSetSprite(Element element, Sprite sprite)
        {
            Level level = Model.CurrentLevel;

            switch(element)
            {
                case Element.Wall:
                    if(sprite.Attribute != null)
                        level.WallAttribute  = sprite.Attribute;

                    level.WallGraphicAddress = sprite.MemoryAddress;
                    View.SetElementSprite(Element.Wall, sprite);
                    break;

                case Element.Earth:
                    if(sprite.Attribute != null)
                        level.EarthAttribute  = sprite.Attribute;

                    level.EarthGraphicAddress = sprite.MemoryAddress;
                    View.SetElementSprite(Element.Earth, sprite);
                    break;

                case Element.Rock:
                    level.RockAttribute = sprite.Attribute;
                    View.SetElementSprite(Element.Rock, sprite);
                    break;

                case Element.Door:
                    level.DoorAttribute = sprite.Attribute;
                    View.SetElementSprite(Element.Door, sprite);
                    break;

                default:
                    throw new ArgumentException("Invalid element type for sprite change");
            }

            View.RedrawMap(Model.CurrentLevel);
        }

        protected void undoSetSprite(SetSpriteAction action)
        {
            doSetSprite(action.Element, action.OldSprite);
            completeUndoAction();
        }

        protected void redoSetSprite(SetSpriteAction action)
        {
            doSetSprite(action.Element, action.NewSprite);
            completeRedoAction(action);
        }


        //
        // Set Level Properties
        //
        protected void setLevelProperties(EditableLevelProperties properties)
        {
            EditableLevelProperties oldProperties = Model.CurrentLevel.LevelProperties;
            doSetLevelProperties(properties);
            completeAction(new SetLevelPropertiesAction(oldProperties, properties));
        }

        protected void doSetLevelProperties(EditableLevelProperties properties)
        {
            Level level = Model.CurrentLevel;
            level.Name                = properties.LevelName;
            level.AllDiamondsRequired = properties.AllDiamondsRequired;
            level.DiamondsRequired    = properties.DiamondsRequired;
            level.PointsPerDiamond    = properties.PointsPerDiamond;
            
            View.SetLevelNames(getLevelNamesList());
        }

        protected void undoSetLevelProperties(SetLevelPropertiesAction action)
        {
            doSetLevelProperties(action.OldLevelProperties);
            completeUndoAction();
        }

        protected void redoSetLevelProperties(SetLevelPropertiesAction action)
        {
            doSetLevelProperties(action.NewLevelProperties);
            completeRedoAction(action);
        }


        //
        // Set Level
        //
        protected void setLevel(int levelNum)
        {
            int oldLevelNum = Model.CurrentLevelNumber;

            if(levelNum == oldLevelNum)
                return;

            doSetLevel(levelNum);
            completeAction(new SetLevelAction(oldLevelNum, levelNum), false);
        }

        protected void doSetLevel(int levelNum)
        {
            Model.CurrentLevelNumber = levelNum;
            Level level = Model.Levels[Model.CurrentLevelNumber];

            View.SetElementSprite(Element.Wall , SpriteDatabase.GetSprite(level, Element.Wall));
            View.SetElementSprite(Element.Earth, SpriteDatabase.GetSprite(level, Element.Earth));
            View.SetElementSprite(Element.Rock , SpriteDatabase.GetSprite(level, Element.Rock));
            View.SetElementSprite(Element.Door , SpriteDatabase.GetSprite(level, Element.Door));

            View.SetCurrentLevel(Model.CurrentLevelNumber, Model.Levels[Model.CurrentLevelNumber]);
        }

        protected void undoSetLevel(SetLevelAction action)
        {
            doSetLevel(action.OldLevelNumber);
            completeUndoAction(false);
        }

        protected void redoSetLevel(SetLevelAction action)
        {
            doSetLevel(action.NewLevelNumber);
            completeRedoAction(action, false);
        }


        //
        // Clear Level
        //
        protected void clearLevel()
        {
            Element[,] oldMapData = Model.CurrentLevel.MapData;

            doClearLevel();
            completeAction(new ClearLevelAction(oldMapData, Model.CurrentLevel.MapData));
        }

        protected void doClearLevel()
        {
            Model.CurrentLevel.ClearMap();
            View.RedrawMap(Model.CurrentLevel);
        }

        protected void undoClearLevel(ClearLevelAction action)
        {
            Model.CurrentLevel.MapData = action.OldMapData;
            View.RedrawMap(Model.CurrentLevel);
            completeUndoAction();
        }

        protected void redoClearLevel(ClearLevelAction action)
        {
            doClearLevel();
            completeRedoAction(action);
        }


        //
        // Complete Action methods
        // Perform housekeeping at the end of an action
        //
        protected void completeAction(Action action, bool hasChanged = true)
        {
            Model.RedoStack.Clear();
            Model.UndoStack.Push(action);
            updateUndoRedoView();
            
            if(hasChanged)
                Model.HasChanged = true;
        }

        protected void completeUndoAction(bool hasChanged = true)
        {
            updateUndoRedoView();
            
            if(hasChanged)
                Model.HasChanged = true;
        }

        protected void completeRedoAction(Action action, bool hasChanged = true)
        {
            Model.UndoStack.Push(action);
            updateUndoRedoView();
            
            if(hasChanged)
                Model.HasChanged = true;
        }
        #endregion

        #region View event handlers
        protected void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(querySaveChanges())
 	            saveSettings();
            else
                e.Cancel = true;
        }

        void View_MapMouseDown(object sender, MapMouseEventArgs e)
        {
            if(e.MouseButtons == MouseButtons.Left && Model.CurrentTool == Tool.Draw)
                drawElement(e.Point);
        }

        void View_MapMouseMoved(object sender, MapMouseEventArgs e)
        {
            if(e.MouseButtons == MouseButtons.Left && Model.CurrentTool == Tool.Draw)
                drawElement(e.Point);
        }

        void View_MapMouseClicked(object sender, MapMouseEventArgs e)
        {
            if(e.MouseButtons == MouseButtons.Left)
            {
                if(Model.CurrentTool == Tool.Draw)
                    drawElement(e.Point);
                else if(Model.CurrentTool == Tool.Fill)
                    fillElement(e.Point);
            }
            else if(e.MouseButtons == MouseButtons.Right)
            {
                pickElement(e.Point);
            }
        }

        protected void View_LevelChanged(object sender, IndexEventArgs e)
        {
            setLevel(e.Index);
        }

        protected void View_ElementSelected(object sender, ElementEventArgs e)
        {
            if(e.Element == Element.PlayerB)
                Model.CurrentElement = Element.PlayerA;
            else
                Model.CurrentElement = e.Element;
        }

        protected void View_ToolChanged(object sender, ToolEventArgs e)
        {
            Model.CurrentTool = e.Tool;
            View.SetSelectedTool(e.Tool);
        }

        protected void View_ElementSpriteChanged(object sender, ElementSpriteEventArgs e)
        {
            setSprite(e.Element, e.Sprite);
        }

        protected void View_ZoomLevelChanged(object sender, ZoomEventArgs e)
        {
            setZoomLevel(e.ZoomLevel);
        }

        protected void View_NewFile(object sender, EventArgs e)
        {
            newFile();
        }

        protected void View_OpenFile(object sender, FilenameEventArgs e)
        {
            openFile(e.Filename);
        }

        protected void View_SaveFile(object sender, EventArgs e)
        {
            saveFile();
        }

        protected void View_SaveFileAs(object sender, FilenameEventArgs e)
        {
            Model.File.Filename = e.Filename;
            saveFile();
        }

        protected void View_ShowProperties(object sender, EventArgs e)
        {
            View.ShowPropertiesDialog(Model.CurrentLevel);
        }

        protected void View_SaveProperties(object sender, SaveLevelPropertiesEventArgs e)
        {
            setLevelProperties(e.Properties);
        }

        void View_ClearLevel(object sender, EventArgs e)
        {
            clearLevel();
        }

        void View_Undo(object sender, EventArgs e)
        {
            if(Model.UndoStack.Count > 0)
            {
                Action action = Model.UndoStack.Pop();
                Model.RedoStack.Push(action);

                if(action is SetElementAction)
                    undoDrawElement(action as SetElementAction);
                else if(action is FillElementAction)
                    undoFillElement(action as FillElementAction);
                else if(action is SetSpriteAction)
                    undoSetSprite(action as SetSpriteAction);
                else if(action is SetLevelPropertiesAction)
                    undoSetLevelProperties(action as SetLevelPropertiesAction);
                else if(action is SetLevelAction)
                    undoSetLevel(action as SetLevelAction);
                else if(action is ClearLevelAction)
                    undoClearLevel(action as ClearLevelAction);
                
                updateUndoRedoView();
            }
        }

        void View_Redo(object sender, EventArgs e)
        {
            if(Model.RedoStack.Count > 0)
            {
                Action action = Model.RedoStack.Pop();

                if(action is SetElementAction)
                    redoDrawElement(action as SetElementAction);
                else if(action is FillElementAction)
                    redoFillElement(action as FillElementAction);
                else if(action is SetSpriteAction)
                    redoSetSprite(action as SetSpriteAction);
                else if(action is SetLevelPropertiesAction)
                    redoSetLevelProperties(action as SetLevelPropertiesAction);
                else if(action is SetLevelAction)
                    redoSetLevel(action as SetLevelAction);
                else if(action is ClearLevelAction)
                    redoClearLevel(action as ClearLevelAction);
            }
        }
        #endregion
    }
}
