namespace ApprendaRectangles
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.appColorExtenderProvider1 = new ApprendaRectangles.ColorPallet.AppColorExtenderProvider();
            this.gbHelp = new System.Windows.Forms.GroupBox();
            this.gbHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(349, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click and drag rectangles by their top-left corners to reposition them.  They wil" +
    "l automatically be analyzed when you finish moving them";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.Location = new System.Drawing.Point(6, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(349, 35);
            this.label3.TabIndex = 2;
            this.label3.Text = "Click the \"Generate New Rectangles\" button at anytime for new rectangles to work " +
    "with.";
            // 
            // gbHelp
            // 
            this.gbHelp.BackColor = System.Drawing.Color.White;
            this.gbHelp.Controls.Add(this.label2);
            this.gbHelp.Controls.Add(this.label3);
            this.gbHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbHelp.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHelp.Location = new System.Drawing.Point(13, 12);
            this.gbHelp.Name = "gbHelp";
            this.gbHelp.Size = new System.Drawing.Size(361, 237);
            this.gbHelp.TabIndex = 3;
            this.gbHelp.TabStop = false;
            this.gbHelp.Text = "How it works....";
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 261);
            this.Controls.Add(this.gbHelp);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Help";
            this.Text = "Help ?!?!";
            this.gbHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ColorPallet.AppColorExtenderProvider appColorExtenderProvider1;
        private System.Windows.Forms.GroupBox gbHelp;
    }
}