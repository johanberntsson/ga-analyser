/* (c) 2004 Johan Berntsson, j.berntsson@qut.edu.au
 * Queensland University of Technology, Brisbane, Australia
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Analyser
{
	/// <summary>
	/// Summary description for PollGAForm.
	/// </summary>
	public class PollGAForm : System.Windows.Forms.Form
	{
    public System.Windows.Forms.TextBox textBoxID;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PollGAForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
      this.textBoxID = new System.Windows.Forms.TextBox();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBoxID
      // 
      this.textBoxID.Location = new System.Drawing.Point(104, 24);
      this.textBoxID.Name = "textBoxID";
      this.textBoxID.Size = new System.Drawing.Size(176, 20);
      this.textBoxID.TabIndex = 0;
      this.textBoxID.Text = "";
      this.textBoxID.TextChanged += new System.EventHandler(this.textBoxID_TextChanged);
      // 
      // buttonOK
      // 
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Enabled = false;
      this.buttonOK.Location = new System.Drawing.Point(24, 80);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(200, 80);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 23);
      this.label1.TabIndex = 3;
      this.label1.Text = "GA Job ID";
      // 
      // PollGAForm
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(292, 117);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.textBoxID);
      this.Name = "PollGAForm";
      this.Text = "Poll GA Framework";
      this.ResumeLayout(false);

    }
		#endregion

    private void textBoxID_TextChanged(object sender, System.EventArgs e) {
      buttonOK.Enabled=(textBoxID.Text.Trim().Length>0);
    }
	}
}
