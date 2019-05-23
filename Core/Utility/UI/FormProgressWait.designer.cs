namespace Sanita.Utility.UI
{
    partial class FormProgressWait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgressWait));
            this.button1 = new System.Windows.Forms.Button();
            this.txtStatusText = new DevComponents.DotNetBar.LabelX();
            this.mProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtStatusText
            // 
            // 
            // 
            // 
            this.txtStatusText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStatusText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatusText.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtStatusText.ForeColor = System.Drawing.Color.Blue;
            this.txtStatusText.Location = new System.Drawing.Point(70, 1);
            this.txtStatusText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStatusText.Name = "txtStatusText";
            this.txtStatusText.Size = new System.Drawing.Size(140, 44);
            this.txtStatusText.TabIndex = 222;
            this.txtStatusText.Text = "In Progress...";
            // 
            // mProgress
            // 
            this.mProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.mProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.mProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.mProgress.Location = new System.Drawing.Point(10, 1);
            this.mProgress.Name = "mProgress";
            this.mProgress.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
            this.mProgress.ProgressColor = System.Drawing.Color.Blue;
            this.mProgress.ProgressTextColor = System.Drawing.Color.Blue;
            this.mProgress.ProgressTextFormat = "";
            this.mProgress.Size = new System.Drawing.Size(60, 44);
            this.mProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.mProgress.TabIndex = 221;
            this.mProgress.Value = 100;
            // 
            // FormProgressWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(220, 46);
            this.ControlBox = false;
            this.Controls.Add(this.txtStatusText);
            this.Controls.Add(this.mProgress);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProgressWait";
            this.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Progress...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevComponents.DotNetBar.LabelX txtStatusText;
        private DevComponents.DotNetBar.Controls.CircularProgress mProgress;

    }
}