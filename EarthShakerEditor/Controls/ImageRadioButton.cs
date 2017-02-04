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
    /// A specialised RadioButton that appears as a flat image, with a border
    /// if it is selected.
    /// </summary>
    public partial class ImageRadioButton : RadioButton
    {
        /// <summary>
        /// Gets or sets the Image displayed on the control.
        /// </summary>
        public new Image Image { get; set; }

        /// <summary>
        /// Initializes a new instance of the ImageRadioButton class.
        /// </summary>
        public ImageRadioButton()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if(Checked)
                e.Graphics.Clear(Color.Red);
            else
                e.Graphics.Clear(Color.Black);

            e.Graphics.DrawImage(Image, 2, 2);
        }
    }
}
