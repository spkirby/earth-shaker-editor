namespace EarthShakerEditor.Controls
{
    partial class ToolButtonPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbDraw = new System.Windows.Forms.RadioButton();
            this.rbFill = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbDraw
            // 
            this.rbDraw.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDraw.Image = global::EarthShakerEditor.Resources.IconDrawTool;
            this.rbDraw.Location = new System.Drawing.Point(0, 0);
            this.rbDraw.Name = "rbDraw";
            this.rbDraw.Size = new System.Drawing.Size(28, 28);
            this.rbDraw.TabIndex = 0;
            this.rbDraw.TabStop = true;
            this.rbDraw.UseVisualStyleBackColor = true;
            // 
            // rbFill
            // 
            this.rbFill.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFill.Image = global::EarthShakerEditor.Resources.IconFillTool;
            this.rbFill.Location = new System.Drawing.Point(0, 0);
            this.rbFill.Name = "rbFill";
            this.rbFill.Size = new System.Drawing.Size(28, 28);
            this.rbFill.TabIndex = 0;
            this.rbFill.TabStop = true;
            this.rbFill.UseVisualStyleBackColor = true;
            // 
            // ToolButtonPanel
            // 
            this.Padding = new System.Windows.Forms.Padding(10, 5, 0, 10);
            this.Size = new System.Drawing.Size(90, 90);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbDraw;
        private System.Windows.Forms.RadioButton rbFill;

    }
}
