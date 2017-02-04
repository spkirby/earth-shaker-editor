namespace EarthShakerEditor.Editor
{
    partial class LevelPropertiesDialog
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
            this.udDiamondsNeeded = new System.Windows.Forms.NumericUpDown();
            this.rbNeedAllDiamonds = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNeeded = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbNeedSomeDiamonds = new System.Windows.Forms.RadioButton();
            this.udDiamondPoints = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLevelName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.udDiamondsNeeded)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDiamondPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // udDiamondsNeeded
            // 
            this.udDiamondsNeeded.Enabled = false;
            this.udDiamondsNeeded.Location = new System.Drawing.Point(42, 73);
            this.udDiamondsNeeded.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.udDiamondsNeeded.Name = "udDiamondsNeeded";
            this.udDiamondsNeeded.Size = new System.Drawing.Size(50, 20);
            this.udDiamondsNeeded.TabIndex = 0;
            this.udDiamondsNeeded.ValueChanged += new System.EventHandler(this.udDiamondsNeeded_ValueChanged);
            // 
            // rbNeedAllDiamonds
            // 
            this.rbNeedAllDiamonds.AutoSize = true;
            this.rbNeedAllDiamonds.Checked = true;
            this.rbNeedAllDiamonds.Location = new System.Drawing.Point(22, 38);
            this.rbNeedAllDiamonds.Name = "rbNeedAllDiamonds";
            this.rbNeedAllDiamonds.Size = new System.Drawing.Size(84, 17);
            this.rbNeedAllDiamonds.TabIndex = 2;
            this.rbNeedAllDiamonds.TabStop = true;
            this.rbNeedAllDiamonds.Text = "All diamonds";
            this.rbNeedAllDiamonds.UseVisualStyleBackColor = true;
            this.rbNeedAllDiamonds.CheckedChanged += new System.EventHandler(this.rbNeedAllDiamonds_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNeeded);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbNeedSomeDiamonds);
            this.panel1.Controls.Add(this.rbNeedAllDiamonds);
            this.panel1.Controls.Add(this.udDiamondsNeeded);
            this.panel1.Location = new System.Drawing.Point(15, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 106);
            this.panel1.TabIndex = 3;
            // 
            // lblNeeded
            // 
            this.lblNeeded.AutoSize = true;
            this.lblNeeded.Location = new System.Drawing.Point(98, 75);
            this.lblNeeded.Name = "lblNeeded";
            this.lblNeeded.Size = new System.Drawing.Size(52, 13);
            this.lblNeeded.TabIndex = 4;
            this.lblNeeded.Text = "diamonds";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Diamonds needed to complete the level:";
            // 
            // rbNeedSomeDiamonds
            // 
            this.rbNeedSomeDiamonds.AutoSize = true;
            this.rbNeedSomeDiamonds.Location = new System.Drawing.Point(22, 75);
            this.rbNeedSomeDiamonds.Name = "rbNeedSomeDiamonds";
            this.rbNeedSomeDiamonds.Size = new System.Drawing.Size(14, 13);
            this.rbNeedSomeDiamonds.TabIndex = 0;
            this.rbNeedSomeDiamonds.UseVisualStyleBackColor = true;
            // 
            // udDiamondPoints
            // 
            this.udDiamondPoints.Location = new System.Drawing.Point(125, 204);
            this.udDiamondPoints.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udDiamondPoints.Name = "udDiamondPoints";
            this.udDiamondPoints.Size = new System.Drawing.Size(50, 20);
            this.udDiamondPoints.TabIndex = 1;
            this.udDiamondPoints.ValueChanged += new System.EventHandler(this.udDiamondPoints_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Diamonds are worth";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(181, 206);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(62, 13);
            this.lblPoints.TabIndex = 5;
            this.lblPoints.Text = "points each";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Level name:";
            // 
            // txtLevelName
            // 
            this.txtLevelName.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLevelName.Location = new System.Drawing.Point(83, 12);
            this.txtLevelName.MaxLength = 20;
            this.txtLevelName.Name = "txtLevelName";
            this.txtLevelName.Size = new System.Drawing.Size(265, 23);
            this.txtLevelName.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(166, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 26);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(260, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LevelPropertiesDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 308);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtLevelName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.udDiamondPoints);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LevelPropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Level Properties";
            ((System.ComponentModel.ISupportInitialize)(this.udDiamondsNeeded)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udDiamondPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown udDiamondsNeeded;
        private System.Windows.Forms.RadioButton rbNeedAllDiamonds;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbNeedSomeDiamonds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown udDiamondPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLevelName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNeeded;
    }
}