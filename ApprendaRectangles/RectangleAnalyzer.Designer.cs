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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInstr2 = new System.Windows.Forms.Label();
            this.lblInstr1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(163)))), ((int)(((byte)(17)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.btnGenerate.Location = new System.Drawing.Point(12, 520);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(173, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate New Rectangles";
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
            this.txtStats.Location = new System.Drawing.Point(520, 13);
            this.txtStats.Multiline = true;
            this.txtStats.Name = "txtStats";
            this.txtStats.Size = new System.Drawing.Size(175, 339);
            this.txtStats.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.lblInstr2);
            this.panel1.Controls.Add(this.lblInstr1);
            this.panel1.Location = new System.Drawing.Point(13, 549);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 56);
            this.panel1.TabIndex = 3;
            // 
            // lblInstr2
            // 
            this.lblInstr2.AutoSize = true;
            this.lblInstr2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblInstr2.Location = new System.Drawing.Point(2, 23);
            this.lblInstr2.Name = "lblInstr2";
            this.lblInstr2.Size = new System.Drawing.Size(500, 14);
            this.lblInstr2.TabIndex = 1;
            this.lblInstr2.Text = "Click the \"Generate New Rectangles\" button at anytime for new rectangles to work " +
    "with.";
            // 
            // lblInstr1
            // 
            this.lblInstr1.AutoSize = true;
            this.lblInstr1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblInstr1.Location = new System.Drawing.Point(2, 4);
            this.lblInstr1.Name = "lblInstr1";
            this.lblInstr1.Size = new System.Drawing.Size(501, 14);
            this.lblInstr1.TabIndex = 0;
            this.lblInstr1.Text = "Click and drag rectangles by their top-left corners to reposition and analyze the" +
    "ir positions.";
            // 
            // RectangleAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(699, 617);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtStats);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.btnGenerate);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RectangleAnalyzer";
            this.Text = "Rectangle Analyzer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private ColorPallet.AppColorExtenderProvider colorProvider;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.TextBox txtStats;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInstr2;
        private System.Windows.Forms.Label lblInstr1;
    }
}

