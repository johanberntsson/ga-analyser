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

namespace Analyser {
  /// <summary>
  /// Summary description for GraphOptionsForm.
  /// </summary>
  public class GraphOptionsForm : System.Windows.Forms.Form {
    private System.Windows.Forms.GroupBox groupBox1;
    public System.Windows.Forms.RadioButton buttonXAuto;
    public System.Windows.Forms.RadioButton buttonXCustom;
    public System.Windows.Forms.TextBox textBoxXMin;
    public System.Windows.Forms.TextBox textBoxXMax;
    private System.Windows.Forms.GroupBox groupBox2;
    public System.Windows.Forms.TextBox textBoxYMax;
    public System.Windows.Forms.TextBox textBoxYMin;
    public System.Windows.Forms.RadioButton buttonYCustom;
    public System.Windows.Forms.RadioButton buttonYAuto;
    public System.Windows.Forms.TextBox textBoxCommands;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.GroupBox groupBox4;
    public System.Windows.Forms.RadioButton buttonLegendOff;
    public System.Windows.Forms.RadioButton buttonLegendOn;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public GraphOptionsForm() {
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
    protected override void Dispose( bool disposing ) {
      if( disposing ) {
        if(components != null) {
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
    private void InitializeComponent() {
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.textBoxXMax = new System.Windows.Forms.TextBox();
      this.textBoxXMin = new System.Windows.Forms.TextBox();
      this.buttonXCustom = new System.Windows.Forms.RadioButton();
      this.buttonXAuto = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.textBoxYMax = new System.Windows.Forms.TextBox();
      this.textBoxYMin = new System.Windows.Forms.TextBox();
      this.buttonYCustom = new System.Windows.Forms.RadioButton();
      this.buttonYAuto = new System.Windows.Forms.RadioButton();
      this.textBoxCommands = new System.Windows.Forms.TextBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.buttonLegendOff = new System.Windows.Forms.RadioButton();
      this.buttonLegendOn = new System.Windows.Forms.RadioButton();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      // 
      // buttonOK
      // 
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Location = new System.Drawing.Point(8, 216);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(328, 216);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(80, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.textBoxXMax);
      this.groupBox1.Controls.Add(this.textBoxXMin);
      this.groupBox1.Controls.Add(this.buttonXCustom);
      this.groupBox1.Controls.Add(this.buttonXAuto);
      this.groupBox1.Location = new System.Drawing.Point(8, 8);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(128, 96);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "X Axis";
      // 
      // textBoxXMax
      // 
      this.textBoxXMax.Location = new System.Drawing.Point(72, 64);
      this.textBoxXMax.Name = "textBoxXMax";
      this.textBoxXMax.Size = new System.Drawing.Size(48, 20);
      this.textBoxXMax.TabIndex = 3;
      this.textBoxXMax.Text = "";
      this.textBoxXMax.Enter += new System.EventHandler(this.textBox_Enter);
      // 
      // textBoxXMin
      // 
      this.textBoxXMin.Location = new System.Drawing.Point(8, 64);
      this.textBoxXMin.Name = "textBoxXMin";
      this.textBoxXMin.Size = new System.Drawing.Size(48, 20);
      this.textBoxXMin.TabIndex = 2;
      this.textBoxXMin.Text = "";
      this.textBoxXMin.Enter += new System.EventHandler(this.textBox_Enter);
      // 
      // buttonXCustom
      // 
      this.buttonXCustom.Location = new System.Drawing.Point(8, 40);
      this.buttonXCustom.Name = "buttonXCustom";
      this.buttonXCustom.TabIndex = 1;
      this.buttonXCustom.Text = "Custom";
      // 
      // buttonXAuto
      // 
      this.buttonXAuto.Checked = true;
      this.buttonXAuto.Location = new System.Drawing.Point(8, 16);
      this.buttonXAuto.Name = "buttonXAuto";
      this.buttonXAuto.TabIndex = 0;
      this.buttonXAuto.TabStop = true;
      this.buttonXAuto.Text = "Automatic";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.textBoxYMax);
      this.groupBox2.Controls.Add(this.textBoxYMin);
      this.groupBox2.Controls.Add(this.buttonYCustom);
      this.groupBox2.Controls.Add(this.buttonYAuto);
      this.groupBox2.Location = new System.Drawing.Point(152, 8);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(128, 96);
      this.groupBox2.TabIndex = 4;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Y Axis";
      // 
      // textBoxYMax
      // 
      this.textBoxYMax.Location = new System.Drawing.Point(72, 64);
      this.textBoxYMax.Name = "textBoxYMax";
      this.textBoxYMax.Size = new System.Drawing.Size(48, 20);
      this.textBoxYMax.TabIndex = 3;
      this.textBoxYMax.Text = "";
      this.textBoxYMax.Enter += new System.EventHandler(this.textBox_Enter);
      // 
      // textBoxYMin
      // 
      this.textBoxYMin.Location = new System.Drawing.Point(8, 64);
      this.textBoxYMin.Name = "textBoxYMin";
      this.textBoxYMin.Size = new System.Drawing.Size(48, 20);
      this.textBoxYMin.TabIndex = 2;
      this.textBoxYMin.Text = "";
      this.textBoxYMin.Enter += new System.EventHandler(this.textBox_Enter);
      // 
      // buttonYCustom
      // 
      this.buttonYCustom.Location = new System.Drawing.Point(8, 40);
      this.buttonYCustom.Name = "buttonYCustom";
      this.buttonYCustom.TabIndex = 1;
      this.buttonYCustom.Text = "Custom";
      // 
      // buttonYAuto
      // 
      this.buttonYAuto.Checked = true;
      this.buttonYAuto.Location = new System.Drawing.Point(8, 16);
      this.buttonYAuto.Name = "buttonYAuto";
      this.buttonYAuto.TabIndex = 0;
      this.buttonYAuto.TabStop = true;
      this.buttonYAuto.Text = "Automatic";
      // 
      // textBoxCommands
      // 
      this.textBoxCommands.Location = new System.Drawing.Point(8, 16);
      this.textBoxCommands.Multiline = true;
      this.textBoxCommands.Name = "textBoxCommands";
      this.textBoxCommands.Size = new System.Drawing.Size(392, 72);
      this.textBoxCommands.TabIndex = 6;
      this.textBoxCommands.Text = "";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.textBoxCommands);
      this.groupBox3.Location = new System.Drawing.Point(8, 104);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(408, 100);
      this.groupBox3.TabIndex = 7;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "GnuPlot Commands";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.buttonLegendOff);
      this.groupBox4.Controls.Add(this.buttonLegendOn);
      this.groupBox4.Location = new System.Drawing.Point(288, 8);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(128, 96);
      this.groupBox4.TabIndex = 5;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Legend";
      // 
      // buttonLegendOff
      // 
      this.buttonLegendOff.Location = new System.Drawing.Point(8, 40);
      this.buttonLegendOff.Name = "buttonLegendOff";
      this.buttonLegendOff.TabIndex = 1;
      this.buttonLegendOff.Text = "Off";
      // 
      // buttonLegendOn
      // 
      this.buttonLegendOn.Checked = true;
      this.buttonLegendOn.Location = new System.Drawing.Point(8, 16);
      this.buttonLegendOn.Name = "buttonLegendOn";
      this.buttonLegendOn.TabIndex = 0;
      this.buttonLegendOn.TabStop = true;
      this.buttonLegendOn.Text = "On";
      // 
      // GraphOptionsForm
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(424, 253);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox4);
      this.Name = "GraphOptionsForm";
      this.Text = "Graph Options";
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    private bool IsValidNumber(string text, out int number) {
      number=0;
      try {
        number=int.Parse(text);
      } catch(Exception) {
        return false;
      }
      return (number>=0);
    }

    private void buttonOK_Click(object sender, System.EventArgs e) {
      int min, max;
      DialogResult=DialogResult.OK;
      if(buttonXCustom.Checked) {
        if(IsValidNumber(textBoxXMin.Text, out min)) {
          if(IsValidNumber(textBoxXMax.Text, out max) && min>=max) {
            DialogResult=DialogResult.None;
          }
        } else {
          DialogResult=DialogResult.None;
        }
      }
      if(buttonYCustom.Checked) {
        if(IsValidNumber(textBoxYMin.Text, out min)) {
          if(IsValidNumber(textBoxYMax.Text, out max) && min>=max) {
            DialogResult=DialogResult.None;
          }
        } else {
          DialogResult=DialogResult.None;
        }
      }
      if(DialogResult==DialogResult.None) {
        MessageBox.Show(this, "Invalid end points for custom axises");
      }
    }

    private void textBox_Enter(object sender, System.EventArgs e) {
      if(sender==textBoxXMax || sender==textBoxXMin) {
        buttonXCustom.Checked=true;  
      } else {
        buttonYCustom.Checked=true;  
      }
    }
  }
}
