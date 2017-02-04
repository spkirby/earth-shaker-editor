using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EarthShakerEditor.Editor
{
    public partial class AboutDialog : Form
    {
        private bool isLogoAnimating = false;

        public AboutDialog()
        {
            InitializeComponent();
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            setLogoAnimation(isLogoAnimating);
            isLogoAnimating = !isLogoAnimating;
        }

        protected void setLogoAnimation(bool animate)
        {
            picLogo.Image = (animate ? Resources.LogoAnimated : Resources.LogoStatic);
        }

        protected override void OnLoad(EventArgs e)
        {
            setLogoAnimation(false);
            isLogoAnimating = false;
            base.OnLoad(e);
        }
    }
}
