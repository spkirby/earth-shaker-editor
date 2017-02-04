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
    /// A specialised drop-down menu for selecting Sprites.
    /// </summary>
    public partial class SpriteMenu : ToolStripMenuItem
    {
        #region Events
        /// <summary>
        /// Occurs when the user selects a Sprite.
        /// </summary>
        public event EventHandler<SpriteEventArgs> SpriteSelected;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the Element that this SpriteMenu's Sprites are for.
        /// </summary>
        public Element Element { get; set; }

        /// <summary>
        /// Gets or sets the currently-selected Sprite.
        /// </summary>
        public Sprite SelectedSprite
        {
            get { return selectedSprite;    }
            set { setSelectedSprite(value); }
        }
        #endregion

        #region Private variables
        private Sprite selectedSprite = SpriteDatabase.Invalid;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SpriteMenu class.
        /// </summary>
        public SpriteMenu()
        {
            InitializeComponent();
            selectedSprite = SpriteDatabase.Invalid;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds a separator to the menu.
        /// </summary>
        public void AddSeparator()
        {
            DropDownItems.Add(new ToolStripSeparator());
        }

        /// <summary>
        /// Adds a Sprite to the menu.
        /// </summary>
        /// <param name="sprite">The Sprite to add.</param>
        public void Add(Sprite sprite)
        {
            Add(sprite, null);
        }

        /// <summary>
        /// Adds a Sprite to the menu with a custom label.
        /// </summary>
        /// <param name="sprite">The Sprite to add.</param>
        /// <param name="customLabel">The custom label to display.</param>
        public void Add(Sprite sprite, string customLabel)
        {
            string name = (customLabel ?? sprite.Name);
            ToolStripMenuItem menuItem = new ToolStripMenuItem(name, sprite.Image);
            menuItem.Checked = false;
            menuItem.Tag = sprite;
            menuItem.Click += new EventHandler(menuItem_Click);
            DropDownItems.Add(menuItem);
        }

        /// <summary>
        /// Raises the SpriteMenu.SpriteSelected event.
        /// </summary>
        /// <param name="e">A SpriteEventArgs containing the event data.</param>
        public void OnSpriteSelected(SpriteEventArgs e)
        {
            if(SpriteSelected != null)
                SpriteSelected(this, e);
        }
        #endregion

        #region Private methods
        private void setSelectedSprite(Sprite sprite)
        {
            Image = sprite.Image;

            foreach(ToolStripItem item in DropDownItems)
            {
                ToolStripMenuItem menuItem = (item as ToolStripMenuItem);
                Sprite tagSprite = (item.Tag as Sprite);
                
                if(menuItem != null && tagSprite != null)
                {
                    //if(tagSprite.MemoryAddress == sprite.MemoryAddress && tagSprite.Attribute == sprite.Attribute)
                    if(tagSprite == sprite)
                    {
                        if(!menuItem.Checked)
                            menuItem.Checked = true;

                        selectedSprite = sprite;
                    }
                    else
                        menuItem.Checked = false;
                }
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            OnSpriteSelected(new SpriteEventArgs((sender as ToolStripMenuItem).Tag as Sprite));
        }
        #endregion
    }
}
