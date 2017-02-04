using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using EarthShakerEditor.Controls;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor.Editor
{
    public partial class EditorView : Form
    {
        #region Events
        public event EventHandler<ElementEventArgs>       ElementChanged;
        public event EventHandler<ToolEventArgs>          ToolChanged;
        public event EventHandler<IndexEventArgs>         LevelChanged;
        public event EventHandler<MapMouseEventArgs>      MapMouseDown;
        public event EventHandler<MapMouseEventArgs>      MapMouseClicked;
        public event EventHandler<MapMouseEventArgs>      MapMouseMoved;
        public event EventHandler<ElementSpriteEventArgs> ElementSpriteChanged;
        public event EventHandler<ZoomEventArgs>          ZoomLevelChanged;
        public event EventHandler                         Undo;
        public event EventHandler                         Redo;
        public event EventHandler                         ClearLevel;

        public event EventHandler                               NewFile;
        public event EventHandler<FilenameEventArgs>            OpenFile;
        public event EventHandler                               SaveFile;
        public event EventHandler<FilenameEventArgs>            SaveFileAs;
        public event EventHandler                               ShowProperties;
        public event EventHandler<SaveLevelPropertiesEventArgs> SaveProperties;
        #endregion

        #region Private variables
        protected LevelPropertiesDialog dlgProperties = new LevelPropertiesDialog();
        protected AboutDialog           dlgAbout      = new AboutDialog();

        protected int   zoomLevel = 1;
        protected Size  defaultMapSize = new Size(480, 320);
        protected bool  ignoreLevelDropDown = true; // Ignore events from the level drop down when manually setting it
        protected Point mouseDownPoint;
        protected Point InvalidPoint = new Point(-1, -1);

        protected Sprite[] sprites =
        {
            SpriteDatabase.Space    , SpriteDatabase.Door      , SpriteDatabase.Rock        , SpriteDatabase.Player ,
            SpriteDatabase.Player   , SpriteDatabase.BrickWall , SpriteDatabase.Earth       , SpriteDatabase.Diamond,
            SpriteDatabase.JellyBean, SpriteDatabase.ForceField, SpriteDatabase.GravityStick, SpriteDatabase.Monitor,
            SpriteDatabase.Elixir   , SpriteDatabase.Teleport  , SpriteDatabase.Bubble      , SpriteDatabase.Fire
        };

        protected List<Sprite> wallSprites = new List<Sprite>()
        {
            SpriteDatabase.BlueStone    , SpriteDatabase.Brain        , SpriteDatabase.Clouds      , SpriteDatabase.Girder      ,
            SpriteDatabase.Hearts       , SpriteDatabase.HoneycombWall, SpriteDatabase.IceCube     , SpriteDatabase.JaggedRock  ,
            SpriteDatabase.MetalPlate   , SpriteDatabase.Moss         , SpriteDatabase.QuestionMark, SpriteDatabase.VerticalTube,
            SpriteDatabase.WhiteTiles
        };

        protected List<Sprite> earthSprites = new List<Sprite>()
        {
            SpriteDatabase.HoneycombEarth, SpriteDatabase.QuestionMark, SpriteDatabase.Sparkles, SpriteDatabase.WetHellSoil
        };

        protected const int graphicButtonsPerRow = 7;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the EditorView class.
        /// </summary>
        public EditorView()
        {
            InitializeComponent();
            initializeGraphicMenus();
            initializeGraphicButtons();

            this.Icon = Resources.MainIcon;
            dlgProperties.FormClosed += new FormClosedEventHandler(dlgProperties_FormClosed);

            picMap.Image = new Bitmap(defaultMapSize.Width, defaultMapSize.Height);
            elemButtonPanel.SetSprites(sprites);
            elemButtonPanel.ElementSelected += new EventHandler<ElementEventArgs>(elemButtonPanel_ElementSelected);

            toolButtonPanel.ToolSelected += new EventHandler<ToolEventArgs>(toolButtonPanel_ToolSelected);

            mouseDownPoint = InvalidPoint;
        }
        #endregion

        #region Initialization
        private void initializeGraphicMenus()
        {
            // Wall Menu
            foreach(Sprite s in getColourGraphicsList(SpriteDatabase.BrickWall))
                menuWall.Add(s, SpectrumAttribute.GetColourName(s.Attribute.Foreground));

            menuWall.AddSeparator();

            foreach(Sprite s in wallSprites)
                menuWall.Add(s);

            menuWall.SpriteSelected += new EventHandler<SpriteEventArgs>(menu_SpriteSelected);

            // Earth Menu
            foreach(Sprite s in getColourGraphicsList(SpriteDatabase.Earth))
                menuEarth.Add(s, SpectrumAttribute.GetColourName(s.Attribute.Foreground));

            menuEarth.AddSeparator();

            foreach(Sprite s in earthSprites)
                menuEarth.Add(s);

            menuEarth.SpriteSelected += new EventHandler<SpriteEventArgs>(menu_SpriteSelected);

            // Rock Menu
            foreach(Sprite s in getColourGraphicsList(SpriteDatabase.Rock))
                menuRock.Add(s, SpectrumAttribute.GetColourName(s.Attribute.Foreground));

            menuRock.SpriteSelected += new EventHandler<SpriteEventArgs>(menu_SpriteSelected);

            // Door Menu           
            foreach(Sprite s in getColourGraphicsList(SpriteDatabase.Door))
                menuDoor.Add(s, SpectrumAttribute.GetColourName(s.Attribute.Foreground));

            menuDoor.SpriteSelected += new EventHandler<SpriteEventArgs>(menu_SpriteSelected);
        }

        private void initializeGraphicButtons()
        {
            btnWallSprite.AddRow(getColourGraphicsList(SpriteDatabase.BrickWall));
            btnWallSprite.AddRow(wallSprites.GetRange(0, 7));
            btnWallSprite.AddRow(wallSprites.GetRange(7, 6));
            btnWallSprite.SpriteSelected += new EventHandler<SpriteEventArgs>(btn_SpriteSelected);

            btnEarthSprite.AddRow(getColourGraphicsList(SpriteDatabase.Earth));
            btnEarthSprite.AddRow(earthSprites);
            btnEarthSprite.SpriteSelected += new EventHandler<SpriteEventArgs>(btn_SpriteSelected);

            btnRockSprite.AddRow(getColourGraphicsList(SpriteDatabase.Rock));
            btnRockSprite.SpriteSelected += new EventHandler<SpriteEventArgs>(btn_SpriteSelected);

            btnDoorSprite.AddRow(getColourGraphicsList(SpriteDatabase.Door));
            btnDoorSprite.SpriteSelected += new EventHandler<SpriteEventArgs>(btn_SpriteSelected);
        }

        private List<Sprite> getColourGraphicsList(Sprite baseSprite)
        {
            List<Sprite> sprites = new List<Sprite>();

            for(int i = 0; i < 7; i++)
                sprites.Add(SpriteDatabase.GetSprite(baseSprite, (SpectrumColour)(i + 1)));

            return sprites;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the display Sprites for Wall, Earth, Rock, and Door Elements.
        /// </summary>
        /// <param name="wallSprite">The Sprite for the Wall Element.</param>
        /// <param name="earthSprite">The Sprite for the Earth Element.</param>
        /// <param name="rockSprite">The Sprite for the Rock Element.</param>
        /// <param name="doorSprite">The Sprite for the Door Element.</param>
        public void SetElementSprites(Sprite wallSprite, Sprite earthSprite, Sprite rockSprite, Sprite doorSprite)
        {
            SetElementSprite(Element.Wall , wallSprite);
            SetElementSprite(Element.Earth, earthSprite);
            SetElementSprite(Element.Rock , rockSprite);
            SetElementSprite(Element.Door , doorSprite);
        }

        /// <summary>
        /// Sets the display Sprite for an Element.
        /// </summary>
        /// <param name="element">The Element to set the Sprite for. Must be one of Wall, Earth, Rock, or Door.</param>
        /// <param name="sprite">The Sprite to use to display the specified Element.</param>
        public void SetElementSprite(Element element, Sprite sprite)
        {
            sprites[(int)element] = sprite;

            switch(element)
            {
                case Element.Wall:
                    menuWall.SelectedSprite = sprite;
                    btnWallSprite.SetSelectedSprite(sprite);
                    elemButtonPanel.SetSprite(Element.Wall, sprite);
                    break;
                case Element.Earth:
                    menuEarth.SelectedSprite = sprite;
                    btnEarthSprite.SetSelectedSprite(sprite);
                    elemButtonPanel.SetSprite(Element.Earth, sprite);
                    break;
                case Element.Rock:
                    menuRock.SelectedSprite = sprite;
                    btnRockSprite.SetSelectedSprite(sprite);
                    elemButtonPanel.SetSprite(Element.Rock, sprite);
                    break;
                case Element.Door:
                    menuDoor.SelectedSprite = sprite;
                    btnDoorSprite.SetSelectedSprite(sprite);
                    elemButtonPanel.SetSprite(Element.Door, sprite);
                    break;
                default:
                    throw new ArgumentException("Element " + element + " can't have its sprite changed");
            }
        }

        /// <summary>
        /// Draws the specified Element at the given position, in tiles.
        /// </summary>
        /// <param name="pos">The position, in tiles, at which to draw the Element.</param>
        /// <param name="element">The Element to draw.</param>
        public void DrawElement(Point pos, Element element)
        {
            DrawElement(pos.X, pos.Y, element);
        }

        /// <summary>
        /// Draws the specified Element at the given position, in tiles.
        /// </summary>
        /// <param name="x">The x-coordinate, in tiles, at which to draw the Element.</param>
        /// <param name="y">The y-coordinate, in tiles, at which to draw the Element.</param>
        /// <param name="element">The Element to draw.</param>
        public void DrawElement(int x, int y, Element element)
        {
            Point drawPoint = new Point(x * Sprite.Size.Width, y * Sprite.Size.Height);
            const int overspill       = 8;
            const int overspillOffset = (overspill / 2);

            // Draw element on image
            using(Graphics g = Graphics.FromImage(picMap.Image))
            {    
                g.DrawImage(sprites[(int)element].Image, drawPoint);
            }

            // Invalidate rectangle on PictureBox, plus a little extra to account for overspill
            // caused by graphic smoothing
            Size invalidateSize = Sprite.Size;
            invalidateSize.Width  = (invalidateSize.Width  * zoomLevel) + overspill;
            invalidateSize.Height = (invalidateSize.Height * zoomLevel) + overspill;

            Point invalidatePoint = drawPoint;
            invalidatePoint.X = (invalidatePoint.X * zoomLevel) - overspillOffset;
            invalidatePoint.Y = (invalidatePoint.Y * zoomLevel) - overspillOffset;
            picMap.Invalidate(new Rectangle(invalidatePoint, invalidateSize));
        }

        /// <summary>
        /// Selects the specified Element's control.
        /// </summary>
        /// <param name="element"></param>
        public void SetSelectedElement(Element element)
        {
            elemButtonPanel.SelectedElement = element;
        }

        /// <summary>
        /// Selects the specified Tool's control.
        /// </summary>
        /// <param name="tool"></param>
        public void SetSelectedTool(Tool tool)
        {
            toolButtonPanel.SelectedTool = tool;
        }

        /// <summary>
        /// Selects the specified level and draws its map.
        /// </summary>
        /// <param name="levelNumber">The number of the specified Level.</param>
        /// <param name="level">The Level to draw.</param>
        public void SetCurrentLevel(int levelNumber, Level level)
        {
            ignoreLevelDropDown = true;
            ddLevelNames.SelectedIndex = levelNumber - 1;
            ignoreLevelDropDown = false;

            redrawMap(level);
        }

        /// <summary>
        /// Sets the names displayed in the drop-down level list.
        /// </summary>
        /// <param name="levelNames"></param>
        public void SetLevelNames(List<string> levelNames)
        {
            ignoreLevelDropDown = true;

            int index = ddLevelNames.SelectedIndex;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            ddLevelNames.Items.Clear();
            for(int i=0; i < levelNames.Count; i++)
            {
                string name = textInfo.ToLower(levelNames[i]);
                name = textInfo.ToTitleCase(name);
                ddLevelNames.Items.Add(String.Format("{0:00}: {1}", i+1, name));
            }

            ddLevelNames.SelectedIndex = index;

            ignoreLevelDropDown = false;
        }

        /// <summary>
        /// Sets the zoom level of the map.
        /// </summary>
        /// <param name="zoomLevel"></param>
        public void SetZoomLevel(int zoomLevel)
        {
            this.zoomLevel = zoomLevel;
            picMap.Size  = new Size(defaultMapSize.Width * zoomLevel, defaultMapSize.Height * zoomLevel);

            if(zoomLevel == 1)
            {
                zoomNormalToolStripMenuItem.Checked = true;
                zoomDoubleToolStripMenuItem.Checked = false;
            }
            else if(zoomLevel == 2)
            {
                zoomNormalToolStripMenuItem.Checked = false;
                zoomDoubleToolStripMenuItem.Checked = true;
            }
        }

        /// <summary>
        /// Enables or disables the menu and toolbar Undo items.
        /// </summary>
        /// <param name="enabled">True if enabled, false if disabled.</param>
        public void SetUndoEnabled(bool enabled)
        {
            btnUndo.Enabled = undoToolStripMenuItem.Enabled = enabled;
        }

        /// <summary>
        /// Enables or disables the menu and toolbar Redo items.
        /// </summary>
        /// <param name="enabled">True if enabled, false if disabled.</param>
        public void SetRedoEnabled(bool enabled)
        {
            btnRedo.Enabled = redoToolStripMenuItem.Enabled = enabled;
        }

        /// <summary>
        /// Redraws the entire map for the specified Level.
        /// </summary>
        /// <param name="level">The Level to redraw.</param>
        public void RedrawMap(Level level)
        {
            redrawMap(level);
        }

        

        /// <summary>
        /// Displays an error message in a pop-up box.
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The text of the message box.</param>
        public void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays an Open File dialog.
        /// </summary>
        /// <returns>The name of the selected file, or null if the dialog was cancelled.</returns>
        public string ShowOpenDialog()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.DefaultExt = "sna";
            dlgOpen.Filter     = "Snapshot file (*.sna)|*.sna|All files (*.*)|*.*";

            if(dlgOpen.ShowDialog() == DialogResult.OK)
                return dlgOpen.FileName;
            else
                return null;
        }

        /// <summary>
        /// Displays a Save File dialog.
        /// </summary>
        /// <returns>The name of the file to save as, or null if the dialog was cancelled.</returns>
        public string ShowSaveDialog()
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.DefaultExt = "sna";
            dlgSave.Filter     = "Snapshot file (*.sna)|*.sna|All files (*.*)|*.*";

            if(dlgSave.ShowDialog() == DialogResult.OK)
                return dlgSave.FileName;
            else
                return null;
        }

        /// <summary>
        /// Prompts the user to save any unsaved changes and returns their response.
        /// </summary>
        /// <returns>One of DialogResult.Yes, DialogResult.No, and DialogResult.Cancel.</returns>
        public DialogResult ShowUnsavedChangesDialog()
        {
            return MessageBox.Show("Do you want to save changes?",
                                   "Save Changes",
                                   MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Displays the Level Properties dialog box.
        /// </summary>
        /// <param name="level">The Level to display properties for.</param>
        public void ShowPropertiesDialog(Level level)
        {
            dlgProperties.DiamondsRequired    = level.DiamondsRequired;
            dlgProperties.AllDiamondsRequired = level.AllDiamondsRequired;
            dlgProperties.PointsPerDiamond    = level.PointsPerDiamond;
            dlgProperties.LevelName           = level.Name;
            dlgProperties.StartPosition       = FormStartPosition.CenterParent;
            dlgProperties.ShowDialog();
        }
        #endregion

        #region Private methods
        private void redrawMap(Level level)
        {
            int x = 0, y = 0;
            Rectangle src  = new Rectangle(0, 0, Sprite.Size.Width, Sprite.Size.Height);

            using(Graphics g = Graphics.FromImage(picMap.Image))
            {
                for(int mapY = 0; mapY < Level.Height; mapY++)
                {
                    for(int mapX = 0; mapX < Level.Width; mapX++)
                    {
                        Element elem = level.GetElement(mapX, mapY);            
                        g.DrawImage(sprites[(int)elem].Image, new Point(x, y));
                        x += Sprite.Size.Width;
                    }

                    x = 0;
                    y += Sprite.Size.Height;
                }

                picMap.Invalidate();
            }
        }

        private Point getMapPosition(Point mousePos)
        {
            if(mousePos.X < 0 || mousePos.X >= picMap.Width || mousePos.Y < 0 || mousePos.Y >= picMap.Height)
                return InvalidPoint;

            int gridX = mousePos.X / (Sprite.Size.Width  * zoomLevel);
            int gridY = mousePos.Y / (Sprite.Size.Height * zoomLevel);

            return new Point(gridX, gridY);
        }
        #endregion

        #region Event handlers
        void ddLevelNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!ignoreLevelDropDown)
                OnLevelChanged(new IndexEventArgs(ddLevelNames.SelectedIndex + 1));
        }
        
        void picMap_MouseUp(object sender, MouseEventArgs e)
        {
            Point gridPos = getMapPosition(e.Location);

            if(gridPos != InvalidPoint && gridPos.Equals(mouseDownPoint))
            {
                if(e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    OnMapMouseClicked(new MapMouseEventArgs(gridPos, e.Button));
                }
            }

            mouseDownPoint = InvalidPoint;
        }

        void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            Point gridPos = getMapPosition(e.Location);

            if(gridPos != InvalidPoint)
            {
                OnMapMouseDown(new MapMouseEventArgs(gridPos, e.Button));
                mouseDownPoint = gridPos;
            }
        }

        void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.None)
                return;

            Point gridPos = getMapPosition(e.Location);

            if(gridPos != InvalidPoint)
            {
                OnMapMouseMoved(new MapMouseEventArgs(gridPos, e.Button));
            }
        }

        void btn_SpriteSelected(object sender, SpriteEventArgs e)
        {
            OnElementSpriteChanged(new ElementSpriteEventArgs((sender as SpriteButton).Element, e.Sprite));
        }

        void menu_SpriteSelected(object sender, SpriteEventArgs e)
        {
            OnElementSpriteChanged(new ElementSpriteEventArgs((sender as SpriteMenu).Element, e.Sprite));
        }

        void elemButtonPanel_ElementSelected(object sender, ElementEventArgs e)
        {
            OnElementSelected(e);
        }

        void toolButtonPanel_ToolSelected(object sender, ToolEventArgs e)
        {
            OnToolSelected(e);
        }

        // Menu Items
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void new_Click(object sender, EventArgs e)
        {
            OnNewFile(new EventArgs());
        }

        private void open_Click(object sender, EventArgs e)
        {
            string filename = ShowOpenDialog();

            if(filename != null)
                OnOpenFile(new FilenameEventArgs(filename));
        }

        private void save_Click(object sender, EventArgs e)
        {
            OnSaveFile(new EventArgs());
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            string filename = ShowSaveDialog();

            if(filename != null)
                OnSaveFileAs(new FilenameEventArgs(filename));
        }

        private void levelProperties_Selected(object sender, EventArgs e)
        {
            OnShowProperties(new EventArgs());
        }

        private void dlgProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(dlgProperties.DialogResult == DialogResult.OK)
                OnSaveProperties(new SaveLevelPropertiesEventArgs(dlgProperties.LevelProperties));
        }

        private void zoomNormal_Click(object sender, EventArgs e)
        {
            OnZoomLevelChanged(new ZoomEventArgs(1));
        }

        private void zoomDouble_Click(object sender, EventArgs e)
        {
            OnZoomLevelChanged(new ZoomEventArgs(2));
        }

        private void clearLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnClearLevel(new EventArgs());
            /*if(MessageBox.Show("Are you sure you want to clear the current level?",
                               "Clear Level",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) == DialogResult.Yes)
                OnClearLevel(new EventArgs());*/
        }

        private void helpAbout_Click(object sender, EventArgs e)
        {
            dlgAbout.StartPosition = FormStartPosition.CenterParent;
            dlgAbout.ShowDialog();
        }

        private void btnDrawTool_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as RadioButton).Checked)
                OnToolSelected(new ToolEventArgs(Tool.Draw));
        }

        private void btnFillTool_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as RadioButton).Checked)
                OnToolSelected(new ToolEventArgs(Tool.Fill));
        }

        private void undo_Click(object sender, EventArgs e)
        {
            OnUndo(new EventArgs());
        }

        private void redo_Click(object sender, EventArgs e)
        {
            OnRedo(new EventArgs());
        }
        #endregion

        #region Event raisers
        protected void OnMapMouseDown(MapMouseEventArgs e)
        {
            if(MapMouseDown != null)
                MapMouseDown(this, e);
        }

        protected void OnMapMouseClicked(MapMouseEventArgs e)
        {
            if(MapMouseClicked != null)
                MapMouseClicked(this, e);
        }

        protected void OnMapMouseMoved(MapMouseEventArgs e)
        {
            if(MapMouseMoved != null)
                MapMouseMoved(this, e);
        }

        protected void OnElementSelected(ElementEventArgs e)
        {
            if(ElementChanged != null)
                ElementChanged(this, e);
        }

        protected void OnToolSelected(ToolEventArgs e)
        {
            if(ToolChanged != null)
                ToolChanged(this, e);
        }

        protected void OnLevelChanged(IndexEventArgs e)
        {
            if(LevelChanged != null)
                LevelChanged(this, e);
        }

        protected void OnElementSpriteChanged(ElementSpriteEventArgs e)
        {
            if(ElementSpriteChanged != null)
                ElementSpriteChanged(this, e);
        }

        protected void OnNewFile(EventArgs e)
        {
            if(NewFile != null)
                NewFile(this, e);
        }

        protected void OnOpenFile(FilenameEventArgs e)
        {
            if(OpenFile != null)
                OpenFile(this, e);
        }

        protected void OnSaveFile(EventArgs e)
        {
            if(SaveFile != null)
                SaveFile(this, e);
        }

        protected void OnSaveFileAs(FilenameEventArgs e)
        {
            if(SaveFileAs != null)
                SaveFileAs(this, e);
        }

        protected void OnShowProperties(EventArgs e)
        {
            if(ShowProperties != null)
                ShowProperties(this, e);
        }

        protected void OnSaveProperties(SaveLevelPropertiesEventArgs e)
        {
            if(SaveProperties != null)
                SaveProperties(this, e);
        }

        protected void OnZoomLevelChanged(ZoomEventArgs e)
        {
            if(ZoomLevelChanged != null)
                ZoomLevelChanged(this, e);
        }

        protected void OnUndo(EventArgs e)
        {
            if(Undo != null)
                Undo(this, e);
        }

        protected void OnRedo(EventArgs e)
        {
            if(Redo != null)
                Redo(this, e);
        }

        protected void OnClearLevel(EventArgs e)
        {
            if(ClearLevel != null)
                ClearLevel(this, e);
        }
        #endregion
    }
}
