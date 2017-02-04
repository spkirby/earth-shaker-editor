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
    public partial class LevelPropertiesDialog : Form
    {
        public bool AllDiamondsRequired
        {
            get { return rbNeedAllDiamonds.Checked; }

            set { rbNeedAllDiamonds.Checked = value; }
        }

        public int DiamondsRequired
        {
            get { return (int)udDiamondsNeeded.Value; }

            set
            {
                if(value < 0 || value > 99)
                    throw new ArgumentException("Invalid number for diamonds required");

                udDiamondsNeeded.Value = value;
            }
        }

        public string LevelName
        {
            get
            {
                string name = txtLevelName.Text.Trim();
                
                if(name.Length > Level.NameLength)
                    name = name.Substring(0, Level.NameLength);
                
                return name;
            }
            set
            {
                string name = value.Trim();

                if(name.Length > Level.NameLength)
                    name = name.Substring(0, Level.NameLength);

                txtLevelName.Text = name;
            }
        }

        public EditableLevelProperties LevelProperties
        {
            get
            {
                EditableLevelProperties props = new EditableLevelProperties();
                props.AllDiamondsRequired = AllDiamondsRequired;
                props.DiamondsRequired    = DiamondsRequired;
                props.LevelName           = LevelName;
                props.PointsPerDiamond    = PointsPerDiamond;
                return props;
            }

            set
            {
                AllDiamondsRequired = value.AllDiamondsRequired;
                DiamondsRequired    = value.DiamondsRequired;
                LevelName           = value.LevelName;
                PointsPerDiamond    = value.PointsPerDiamond;
            }
        }

        public int PointsPerDiamond
        {
            get { return (int)udDiamondPoints.Value; }
            
            set
            {
                if(value < 0 || value > 255)
                    throw new ArgumentException("Invalid number for PointsPerDiamond. Must be in the range 0-255");

                udDiamondPoints.Value = value;
            }
        }


        public LevelPropertiesDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Resources.IconProperties.GetHicon());
        }

        private void udDiamondsNeeded_ValueChanged(object sender, EventArgs e)
        {
            lblNeeded.Text = (udDiamondsNeeded.Value == 1 ? "diamond" : "diamonds");
        }

        private void udDiamondPoints_ValueChanged(object sender, EventArgs e)
        {
            lblPoints.Text = (udDiamondPoints.Value == 1 ? "point each" : "points each");
        }

        private void rbNeedAllDiamonds_CheckedChanged(object sender, EventArgs e)
        {
            udDiamondsNeeded.Enabled = !rbNeedAllDiamonds.Checked;
        }
    }
}
