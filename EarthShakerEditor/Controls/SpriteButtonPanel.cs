using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EarthShakerEditor.Spectrum;

namespace EarthShakerEditor.Controls
{
    /// <summary>
    /// A specialised Panel that contains ImageRadioButtons representing Sprites.
    /// </summary>
    public partial class SpriteButtonPanel : Panel
    {
        #region Events
        /// <summary>
        /// Occurs when the user selects a Sprite.
        /// </summary>
        public event EventHandler<SpriteEventArgs> SpriteSelected;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the level of padding between ImageRadioButtons.
        /// </summary>
        public int ButtonPadding { get; private set; }

        /// <summary>
        /// Gets or sets the currently-selected Sprite.
        /// </summary>
        public Sprite SelectedSprite
        {
            get { return selectedSprite; }
            set { setSelectedSprite(value); }
        }
        #endregion

        #region Private variables
        //private ToolTip toolTip = new ToolTip();
        private int    x = 0, y = 0;
        private bool   ignoreNextChecked = false;
        private Sprite selectedSprite  = SpriteDatabase.Invalid;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SpriteButtonPanel class.
        /// </summary>
        public SpriteButtonPanel()
        {
            InitializeComponent();
            ButtonPadding = 4;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds a new row to the panel, containing the specified Sprites.
        /// </summary>
        /// <param name="sprites">A list of Sprites to add to the row.</param>
        public void AddRow(List<Sprite> sprites)
        {
            ImageRadioButton btn = new ImageRadioButton();

            foreach(Sprite sprite in sprites)
            {
                btn = new ImageRadioButton();
                btn.Image = sprite.Image;
                btn.Margin = Padding.Empty;
                btn.Padding = Padding.Empty;
                btn.Location = new Point(x, y);
                btn.Tag = sprite;
                btn.CheckedChanged += new EventHandler(btn_CheckedChanged);
                this.Controls.Add(btn);

                x += (btn.Size.Width + ButtonPadding);
            }

            x -= ButtonPadding;
            if(x > Width)
                Width = x;
            x = 0;

            y += btn.Size.Height;
            if(y > Height)
                Height = y;
            y += ButtonPadding;
        }

        /// <summary>
        /// Raises the SpriteButtonPanel.SpriteSelected event.
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
            foreach(Control btn in Controls)
            {
                Sprite tagSprite = (btn.Tag as Sprite);
                ImageRadioButton btnRadio = (btn as ImageRadioButton);

                if(btnRadio != null && tagSprite != null)
                {
                    //if(sprite.MemoryAddress == selectedSprite.MemoryAddress && sprite.Attribute == selectedSprite.Attribute)
                    if(tagSprite == selectedSprite)
                    {
                        if(!btnRadio.Checked)
                        {
                            ignoreNextChecked = true;
                            btnRadio.Checked = true;
                            selectedSprite = sprite;
                        }

                        break;
                    }
                }
            }
        }

        void btn_CheckedChanged(object sender, EventArgs e)
        {
            // We're only interested in the RadioButton that's turning on, not the one turning off
            if((sender as RadioButton).Checked)
            {
                if(ignoreNextChecked)
                    ignoreNextChecked = false;
                else
                    OnSpriteSelected(new SpriteEventArgs((sender as Control).Tag as Sprite));
            }
        }
        #endregion
    }
}
