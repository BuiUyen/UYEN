using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sanita.Utility.UI
{
	/// <summary>
	/// Summary description for AlertCustom.
	/// </summary>
	public class AlertCustom : DevComponents.DotNetBar.Balloon
	{
        private DevComponents.DotNetBar.Controls.ReflectionImage reflectionImage1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.LabelX txtAlert;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AlertCustom(String strText)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            txtAlert.Text = strText;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertCustom));
            this.reflectionImage1 = new DevComponents.DotNetBar.Controls.ReflectionImage();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtAlert = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // reflectionImage1
            // 
            this.reflectionImage1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.reflectionImage1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionImage1.Dock = System.Windows.Forms.DockStyle.Left;
            this.reflectionImage1.Image = global::MediboxAssistant.Properties.Resources.Info_32x32;
            this.reflectionImage1.Location = new System.Drawing.Point(0, 0);
            this.reflectionImage1.Name = "reflectionImage1";
            this.reflectionImage1.Size = new System.Drawing.Size(34, 125);
            this.reflectionImage1.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Location = new System.Drawing.Point(34, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(297, 30);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "<b>NOTIFICATION</b>";
            // 
            // txtAlert
            // 
            this.txtAlert.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.txtAlert.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlert.Location = new System.Drawing.Point(34, 30);
            this.txtAlert.Name = "txtAlert";
            this.txtAlert.Size = new System.Drawing.Size(297, 95);
            this.txtAlert.TabIndex = 3;
            this.txtAlert.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.txtAlert.WordWrap = true;
            // 
            // AlertCustom
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(240)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(161)))), ((int)(((byte)(171)))));
            this.CaptionFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.ClientSize = new System.Drawing.Size(331, 125);
            this.Controls.Add(this.txtAlert);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.reflectionImage1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AlertCustom";
            this.Style = DevComponents.DotNetBar.eBallonStyle.Office2007Alert;
            this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.devcomponents.com");
		}
	}
}
