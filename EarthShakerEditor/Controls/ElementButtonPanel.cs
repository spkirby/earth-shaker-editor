using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EarthShakerEditor.Controls
{
    /// <summary>
    /// A specialised Panel that contains buttons for each Element.
    /// </summary>
    public partial class ElementButtonPanel : FlowLayoutPanel
    {
        #region Events
        /// <summary>
        /// Occurs when the user selects an Element.
        /// </summary>
        public event EventHandler<ElementEventArgs> ElementSelected;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the currently-selected Element.
        /// </summary>
        public Element SelectedElement
        {
            get
            {
                return selectedElem;
            }

            set
            {
                if(value == Element.PlayerB)
                    value = Element.PlayerA;

                selectedElem = value;

                RadioButton btn = buttons[(int)value];
                if(!btn.Checked)
                {
                    ignoreNextChecked = true;
                    btn.Checked = true;
                }
            }
        }
        #endregion

        #region Private variables
        private RadioButton[] buttons;
        private Element selectedElem = Element.Space;
        private Sprite[] sprites =
        {
            SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, 
            SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, 
            SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, 
            SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid, SpriteDatabase.Invalid
        };
        private bool ignoreNextChecked = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ElementButtonPanel class.
        /// </summary>
        public ElementButtonPanel()
        {
            InitializeComponent();
            initializeButtons();

            SelectedElement = Element.Wall;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Sets the Sprite to use for the specified Element.
        /// </summary>
        /// <param name="element">The Element to set the Sprite for.</param>
        /// <param name="sprite">The Sprite to use for the specified Element.</param>
        public void SetSprite(Element element, Sprite sprite)
        {
            this.sprites[(int)element] = sprite;
            redrawImage((int)element);
        }

        /// <summary>
        /// Sets the Sprites to use for each Element.
        /// </summary>
        /// <param name="sprites">An array of Sprites with exactly (Element.Last + 1) items.</param>
        public void SetSprites(Sprite[] sprites)
        {
            int elemCount = (int)Element.Last + 1;

            if(sprites != null && sprites.Length != elemCount)
                throw new ArgumentException("Invalid sprite array");

            this.sprites = sprites;
            redrawImages();
        }

        /// <summary>
        /// Raises the ElementButtonPanel.ElementSelected event.
        /// </summary>
        /// <param name="e">An ElementEventArgs that contains the event data.</param>
        public void OnElementSelected(ElementEventArgs e)
        {
            if(ElementSelected != null)
                ElementSelected(this, e);
        }
        #endregion

        #region Private methods
        private void initializeButtons()
        {
            int elemCount = (int)Element.Last + 1;

            ToolTip toolTip = new ToolTip();
            toolTip.AutomaticDelay = 1000;

            buttons = new RadioButton[elemCount];

            for(int i = 0; i < elemCount; i++)
            {
                if(i == (int)Element.PlayerB) // Ignore duplicate Player option
                    continue;

                RadioButton button = new RadioButton();
                button.Appearance  = Appearance.Button;
                button.BackColor   = Color.Black;
                button.Size        = new Size(28, 28);
                button.Tag         = (Element)i;
                button.CheckedChanged += new EventHandler(button_CheckedChanged);
                
                buttons[i] = button;

                toolTip.SetToolTip(button, Resources.ResourceManager.GetString("Element" + i));
                Controls.Add(button);
            }
        }

        void button_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as RadioButton).Checked)
            {
                if(ignoreNextChecked)
                    ignoreNextChecked = false;
                else
                {
                    Element elem = (Element)(sender as Control).Tag;
                    OnElementSelected(new ElementEventArgs(elem));
                }
            }
        }

        private void redrawImage(int index)
        {
            if(index != (int)Element.PlayerB)
                buttons[index].Image = sprites[index].Image;
        }

        private void redrawImages()
        {
            int elemCount = (int)Element.Last + 1;

            for(int i = 0; i < elemCount; i++)
                redrawImage(i);
        }
        #endregion
    }
}
