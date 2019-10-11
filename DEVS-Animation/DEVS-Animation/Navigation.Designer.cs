namespace CPaint
{
    partial class Navigation
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.percentBar = new System.Windows.Forms.TrackBar();
            this.percentText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.percentBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(14, 15);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(347, 281);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // percentBar
            // 
            this.percentBar.Location = new System.Drawing.Point(14, 304);
            this.percentBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.percentBar.Maximum = 1000;
            this.percentBar.Minimum = 100;
            this.percentBar.Name = "percentBar";
            this.percentBar.Size = new System.Drawing.Size(282, 45);
            this.percentBar.TabIndex = 1;
            this.percentBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.percentBar.Value = 100;
            this.percentBar.Scroll += new System.EventHandler(this.checkScroll);
            // 
            // percentText
            // 
            this.percentText.Location = new System.Drawing.Point(303, 304);
            this.percentText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.percentText.Name = "percentText";
            this.percentText.Size = new System.Drawing.Size(38, 21);
            this.percentText.TabIndex = 2;
            this.percentText.Text = "100";
            this.percentText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.percentText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.setValue);
            this.percentText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.checkType);
            this.percentText.Leave += new System.EventHandler(this.checkText);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            // 
            // Navigation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(375, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.percentText);
            this.Controls.Add(this.percentBar);
            this.Controls.Add(this.pictureBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Navigation";
            this.Text = "Navigation";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resetFocus);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.percentBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TrackBar percentBar;
        private System.Windows.Forms.TextBox percentText;
        private System.Windows.Forms.Label label1;
    }
}