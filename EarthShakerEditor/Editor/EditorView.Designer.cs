namespace EarthShakerEditor.Editor
{
    partial class EditorView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ToolStripLabel toolStripLabel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ddLevelNames = new System.Windows.Forms.ToolStripComboBox();
            this.btnLevelProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWallSprite = new EarthShakerEditor.Controls.SpriteButton();
            this.btnEarthSprite = new EarthShakerEditor.Controls.SpriteButton();
            this.btnRockSprite = new EarthShakerEditor.Controls.SpriteButton();
            this.btnDoorSprite = new EarthShakerEditor.Controls.SpriteButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.clearLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomDoubleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWall = new EarthShakerEditor.Controls.SpriteMenu();
            this.menuEarth = new EarthShakerEditor.Controls.SpriteMenu();
            this.menuRock = new EarthShakerEditor.Controls.SpriteMenu();
            this.menuDoor = new EarthShakerEditor.Controls.SpriteMenu();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolButtonPanel = new EarthShakerEditor.Controls.ToolButtonPanel();
            this.elemButtonPanel = new EarthShakerEditor.Controls.ElementButtonPanel();
            this.picMapPanel = new System.Windows.Forms.Panel();
            this.picMap = new System.Windows.Forms.PictureBox();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.picMapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            toolStripLabel1.Text = "Level : ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator4,
            this.btnUndo,
            this.btnRedo,
            this.toolStripSeparator1,
            toolStripLabel1,
            this.ddLevelNames,
            this.btnLevelProperties,
            this.toolStripSeparator5,
            this.btnWallSprite,
            this.btnEarthSprite,
            this.btnRockSprite,
            this.btnDoorSprite});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.new_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.open_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = global::EarthShakerEditor.Resources.IconUndo;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "Undo";
            this.btnUndo.Click += new System.EventHandler(this.undo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = global::EarthShakerEditor.Resources.IconRedo;
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(23, 22);
            this.btnRedo.Text = "Redo";
            this.btnRedo.Click += new System.EventHandler(this.redo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ddLevelNames
            // 
            this.ddLevelNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddLevelNames.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.ddLevelNames.Name = "ddLevelNames";
            this.ddLevelNames.Size = new System.Drawing.Size(265, 25);
            this.ddLevelNames.SelectedIndexChanged += new System.EventHandler(this.ddLevelNames_SelectedIndexChanged);
            // 
            // btnLevelProperties
            // 
            this.btnLevelProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLevelProperties.Image = global::EarthShakerEditor.Resources.IconProperties;
            this.btnLevelProperties.Name = "btnLevelProperties";
            this.btnLevelProperties.Size = new System.Drawing.Size(23, 22);
            this.btnLevelProperties.Text = "Level Properties";
            this.btnLevelProperties.Click += new System.EventHandler(this.levelProperties_Selected);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnWallSprite
            // 
            this.btnWallSprite.AutoSize = false;
            this.btnWallSprite.AutoToolTip = false;
            this.btnWallSprite.Element = EarthShakerEditor.Element.Wall;
            this.btnWallSprite.Image = ((System.Drawing.Image)(resources.GetObject("btnWallSprite.Image")));
            this.btnWallSprite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWallSprite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnWallSprite.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnWallSprite.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.btnWallSprite.Name = "btnWallSprite";
            this.btnWallSprite.Size = new System.Drawing.Size(70, 22);
            this.btnWallSprite.Text = "Wall";
            // 
            // btnEarthSprite
            // 
            this.btnEarthSprite.AutoSize = false;
            this.btnEarthSprite.AutoToolTip = false;
            this.btnEarthSprite.Element = EarthShakerEditor.Element.Earth;
            this.btnEarthSprite.Image = ((System.Drawing.Image)(resources.GetObject("btnEarthSprite.Image")));
            this.btnEarthSprite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEarthSprite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEarthSprite.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnEarthSprite.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.btnEarthSprite.Name = "btnEarthSprite";
            this.btnEarthSprite.Size = new System.Drawing.Size(70, 22);
            this.btnEarthSprite.Text = "Earth";
            // 
            // btnRockSprite
            // 
            this.btnRockSprite.AutoSize = false;
            this.btnRockSprite.AutoToolTip = false;
            this.btnRockSprite.Element = EarthShakerEditor.Element.Rock;
            this.btnRockSprite.Image = ((System.Drawing.Image)(resources.GetObject("btnRockSprite.Image")));
            this.btnRockSprite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRockSprite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRockSprite.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRockSprite.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.btnRockSprite.Name = "btnRockSprite";
            this.btnRockSprite.Size = new System.Drawing.Size(70, 22);
            this.btnRockSprite.Text = "Rock";
            // 
            // btnDoorSprite
            // 
            this.btnDoorSprite.AutoSize = false;
            this.btnDoorSprite.AutoToolTip = false;
            this.btnDoorSprite.Element = EarthShakerEditor.Element.Door;
            this.btnDoorSprite.Image = ((System.Drawing.Image)(resources.GetObject("btnDoorSprite.Image")));
            this.btnDoorSprite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDoorSprite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDoorSprite.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDoorSprite.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.btnDoorSprite.Name = "btnDoorSprite";
            this.btnDoorSprite.Size = new System.Drawing.Size(70, 22);
            this.btnDoorSprite.Text = "Door";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconNew;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.new_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconOpen;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.open_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconSave;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.save_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.clearLevelToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconUndo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconRedo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
            // 
            // clearLevelToolStripMenuItem
            // 
            this.clearLevelToolStripMenuItem.Name = "clearLevelToolStripMenuItem";
            this.clearLevelToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.clearLevelToolStripMenuItem.Text = "C&lear Level";
            this.clearLevelToolStripMenuItem.Click += new System.EventHandler(this.clearLevelToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomNormalToolStripMenuItem,
            this.zoomDoubleToolStripMenuItem});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "&Zoom";
            // 
            // zoomNormalToolStripMenuItem
            // 
            this.zoomNormalToolStripMenuItem.Name = "zoomNormalToolStripMenuItem";
            this.zoomNormalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.zoomNormalToolStripMenuItem.Text = "&Normal";
            this.zoomNormalToolStripMenuItem.Click += new System.EventHandler(this.zoomNormal_Click);
            // 
            // zoomDoubleToolStripMenuItem
            // 
            this.zoomDoubleToolStripMenuItem.Name = "zoomDoubleToolStripMenuItem";
            this.zoomDoubleToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.zoomDoubleToolStripMenuItem.Text = "&Double";
            this.zoomDoubleToolStripMenuItem.Click += new System.EventHandler(this.zoomDouble_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWall,
            this.menuEarth,
            this.menuRock,
            this.menuDoor,
            this.toolStripSeparator3,
            this.propertiesToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "&Level";
            // 
            // menuWall
            // 
            this.menuWall.Element = EarthShakerEditor.Element.Wall;
            this.menuWall.Name = "menuWall";
            this.menuWall.Size = new System.Drawing.Size(177, 22);
            this.menuWall.Text = "&Wall";
            // 
            // menuEarth
            // 
            this.menuEarth.Element = EarthShakerEditor.Element.Earth;
            this.menuEarth.Name = "menuEarth";
            this.menuEarth.Size = new System.Drawing.Size(177, 22);
            this.menuEarth.Text = "&Earth";
            // 
            // menuRock
            // 
            this.menuRock.Element = EarthShakerEditor.Element.Rock;
            this.menuRock.Name = "menuRock";
            this.menuRock.Size = new System.Drawing.Size(177, 22);
            this.menuRock.Text = "R&ock";
            // 
            // menuDoor
            // 
            this.menuDoor.Element = EarthShakerEditor.Element.Door;
            this.menuDoor.Name = "menuDoor";
            this.menuDoor.Size = new System.Drawing.Size(177, 22);
            this.menuDoor.Text = "&Door";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = global::EarthShakerEditor.Resources.IconProperties;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.propertiesToolStripMenuItem.Text = "P&roperties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.levelProperties_Selected);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.helpAbout_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.toolButtonPanel);
            this.flowLayoutPanel1.Controls.Add(this.elemButtonPanel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(92, 513);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // toolButtonPanel
            // 
            this.toolButtonPanel.Location = new System.Drawing.Point(3, 3);
            this.toolButtonPanel.Name = "toolButtonPanel";
            this.toolButtonPanel.Padding = new System.Windows.Forms.Padding(10, 5, 0, 10);
            this.toolButtonPanel.SelectedTool = EarthShakerEditor.Editor.Tool.Draw;
            this.toolButtonPanel.Size = new System.Drawing.Size(90, 45);
            this.toolButtonPanel.TabIndex = 7;
            // 
            // elemButtonPanel
            // 
            this.elemButtonPanel.Location = new System.Drawing.Point(3, 54);
            this.elemButtonPanel.Name = "elemButtonPanel";
            this.elemButtonPanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.elemButtonPanel.SelectedElement = EarthShakerEditor.Element.Wall;
            this.elemButtonPanel.Size = new System.Drawing.Size(90, 281);
            this.elemButtonPanel.TabIndex = 6;
            // 
            // picMapPanel
            // 
            this.picMapPanel.AutoScroll = true;
            this.picMapPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.picMapPanel.Controls.Add(this.picMap);
            this.picMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMapPanel.Location = new System.Drawing.Point(92, 49);
            this.picMapPanel.Name = "picMapPanel";
            this.picMapPanel.Size = new System.Drawing.Size(692, 513);
            this.picMapPanel.TabIndex = 11;
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.Color.Black;
            this.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMap.Location = new System.Drawing.Point(0, 0);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(480, 320);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseUp);
            // 
            // EditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.picMapPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "EditorView";
            this.Text = "Earth Shaker Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.picMapPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox ddLevelNames;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomDoubleToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnLevelProperties;
        private System.Windows.Forms.ToolStripMenuItem clearLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private Controls.SpriteMenu menuWall;
        private Controls.SpriteMenu menuEarth;
        private Controls.SpriteMenu menuRock;
        private Controls.SpriteMenu menuDoor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Controls.SpriteButton btnWallSprite;
        private Controls.SpriteButton btnEarthSprite;
        private Controls.SpriteButton btnRockSprite;
        private Controls.SpriteButton btnDoorSprite;
        private Controls.ElementButtonPanel elemButtonPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.ToolButtonPanel toolButtonPanel;
        private System.Windows.Forms.Panel picMapPanel;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    }
}

