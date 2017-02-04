using System;
using System.Windows.Forms;
using EarthShakerEditor.Editor;

namespace EarthShakerEditor.Controls
{
    /// <summary>
    /// A specialised Panel that contains buttons for each Tool.
    /// </summary>
    public partial class ToolButtonPanel : FlowLayoutPanel
    {
        #region Events
        /// <summary>
        /// Occurs when the user selects a Tool.
        /// </summary>
        public event EventHandler<ToolEventArgs> ToolSelected;
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the currently-selected Tool.
        /// </summary>
        public Tool SelectedTool
        {
            get { return selectedTool; }

            set
            {
                selectedTool = value;

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
        private Tool selectedTool = Tool.Draw;
        private bool ignoreNextChecked = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ToolButtonPanel class.
        /// </summary>
        public ToolButtonPanel()
        {
            InitializeComponent();
            initializeButtons();

            SelectedTool = Tool.Draw;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Raises the ToolButtonPanel.ToolSelected event.
        /// </summary>
        /// <param name="e">A ToolEventArgs containing the event data.</param>
        public void OnToolSelected(ToolEventArgs e)
        {
            if(ToolSelected != null)
                ToolSelected(this, e);
        }
        #endregion

        #region Private methods
        private void initializeButtons()
        {
            int toolCount = (int)Tool.Last + 1;

            ToolTip toolTip = new ToolTip();
            toolTip.AutomaticDelay = 1000;

            buttons = new RadioButton[toolCount];
            buttons[(int)Tool.Draw] = rbDraw;
            buttons[(int)Tool.Fill] = rbFill;

            for(int i = 0; i < toolCount; i++)
            {
                buttons[i].Tag = (Tool)i;
                buttons[i].CheckedChanged += new EventHandler(button_CheckedChanged);
                toolTip.SetToolTip(buttons[i], Resources.ResourceManager.GetString("Tool" + i));

                Controls.Add(buttons[i]);
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
                    Tool tool = (Tool)(sender as Control).Tag;
                    OnToolSelected(new ToolEventArgs(tool));
                }
            }
        }
        #endregion
    }
}
