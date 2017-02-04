using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EarthShakerEditor.Controls
{
    /// <summary>
    /// A specialised drop-down button used for displaying Element-specific Sprites.
    /// </summary>
    public partial class SpriteButton : ToolStripDropDownButton
    {
        #region Note on transparency
        // Note: ImageTransparentColor must be set to a non-Spectrum colour, otherwise it
        //       corrupts the image. Color.Transparent doesn't work; it causes white to
        //       become transparent instead.
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user selects a Sprite from this button's drop-down menu.
        /// </summary>
        public event EventHandler<SpriteEventArgs> SpriteSelected;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the Element that this SpriteButton's Sprites are for.
        /// </summary>
        public Element Element { get; set; }
        #endregion

        #region Private variables
        private ToolStripControlHost panelHost;
        private SpriteButtonPanel spritePanel;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SpriteButton class.
        /// </summary>
        public SpriteButton()
        {
            InitializeComponent();
            initializeMenu();

            (DropDown as ToolStripDropDownMenu).ShowImageMargin = false;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds a new row to the drop-down SpriteButtonPanel containing the specified list of Sprites.
        /// </summary>
        /// <param name="sprites">A List of Sprites to add to the new row.</param>
        public void AddRow(List<Sprite> sprites)
        {
            spritePanel.AddRow(sprites);
            adjustSize();
        }

        /// <summary>
        /// Sets the currently-selected Sprite.
        /// </summary>
        /// <param name="sprite">The Sprite to be selected.</param>
        public void SetSelectedSprite(Sprite sprite)
        {
            Image = sprite.Image;
            spritePanel.SelectedSprite = sprite;
        }

        /// <summary>
        /// Raises the SpriteButton.SpriteSelected event.
        /// </summary>
        /// <param name="e">A SpriteEventArgs containing the event data.</param>
        public void OnSpriteSelected(SpriteEventArgs e)
        {
            if(SpriteSelected != null)
                SpriteSelected(this, e);
        }
        #endregion

        #region Private methods
        private void initializeMenu()
        {
            spritePanel = new SpriteButtonPanel();
            spritePanel.SpriteSelected += new EventHandler<SpriteEventArgs>(spritePanel_SpriteSelected);

            panelHost = new ToolStripControlHost(spritePanel);
            panelHost.Padding = Padding.Empty;
            panelHost.Margin  = new Padding(0, 7, 6, 6);

            adjustSize();
            DropDownItems.Add(panelHost);
        }

        void spritePanel_SpriteSelected(object sender, SpriteEventArgs e)
        {
            DropDown.Close();
            OnSpriteSelected(e);
        }

        private void adjustSize()
        {
            DropDown.AutoSize = false;
            DropDown.Width  = panelHost.Size.Width  + panelHost.Margin.Horizontal + DropDown.Padding.Horizontal;
            DropDown.Height = panelHost.Size.Height + panelHost.Margin.Vertical + DropDown.Padding.Vertical;
        }
        #endregion
    }
}