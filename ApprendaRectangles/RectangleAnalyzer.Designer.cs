namespace ApprendaRectangles
{
    partial class RectangleAnalyzer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RectangleAnalyzer));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.colorProvider = new ApprendaRectangles.ColorPallet.AppColorExtenderProvider();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtStats = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(163)))), ((int)(((byte)(17)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnGenerate.Location = new System.Drawing.Point(519, 13);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(173, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate Rectangles";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.pnlGrid.Location = new System.Drawing.Point(13, 13);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(501, 501);
            this.pnlGrid.TabIndex = 1;
            this.pnlGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawGridOnPanel);
            // 
            // txtStats
            // 
            this.txtStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.txtStats.Location = new System.Drawing.Point(13, 520);
            this.txtStats.Multiline = true;
            this.txtStats.Name = "txtStats";
            this.txtStats.Size = new System.Drawing.Size(500, 94);
            this.txtStats.TabIndex = 2;
            // 
            // RectangleAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(699, 654);
            this.Controls.Add(this.txtStats);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.btnGenerate);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RectangleAnalyzer";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private ColorPallet.AppColorExtenderProvider colorProvider;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.TextBox txtStats;
    }
}

