using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Analyser
{
  public class Preferences : System.Windows.Forms.Form {
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtGnuplotPath;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.TextBox txtLogPath;
    private System.Windows.Forms.Label label2;
    private System.ComponentModel.Container components = null;


    public Preferences() {
      LoadPreferences();
      InitializeComponent();
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtGnuplotPath = new System.Windows.Forms.TextBox();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.txtLogPath = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "Gnuplot path";
      // 
      // txtGnuplotPath
      // 
      this.txtGnuplotPath.Location = new System.Drawing.Point(96, 16);
      this.txtGnuplotPath.Name = "txtGnuplotPath";
      this.txtGnuplotPath.Size = new System.Drawing.Size(184, 20);
      this.txtGnuplotPath.TabIndex = 1;
      this.txtGnuplotPath.Text = "";
      // 
      // buttonOK
      // 
      this.buttonOK.Location = new System.Drawing.Point(8, 96);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.TabIndex = 2;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonCancel.Location = new System.Drawing.Point(208, 96);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      // 
      // txtLogPath
      // 
      this.txtLogPath.Location = new System.Drawing.Point(98, 55);
      this.txtLogPath.Name = "txtLogPath";
      this.txtLogPath.Size = new System.Drawing.Size(184, 20);
      this.txtLogPath.TabIndex = 11;
      this.txtLogPath.Text = "";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(10, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(80, 23);
      this.label2.TabIndex = 10;
      this.label2.Text = "Log path";
      // 
      // Preferences
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(292, 133);
      this.Controls.Add(this.txtLogPath);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.txtGnuplotPath);
      this.Controls.Add(this.label1);
      this.Name = "Preferences";
      this.Text = "Preferences";
      this.Load += new System.EventHandler(this.Preferences_Load);
      this.ResumeLayout(false);

    }
    #endregion

    private void buttonOK_Click(object sender, System.EventArgs e) {
      // Validate

      // Update preferences
      gnuplotPath=txtGnuplotPath.Text;
      logPath=txtLogPath.Text;

      SavePreferences();
      DialogResult=DialogResult.OK;
    }

    private void Preferences_Load(object sender, System.EventArgs e) {
      txtGnuplotPath.Text=gnuplotPath;
      txtLogPath.Text=logPath;
    }

    // Preferences
    public static string gnuplotPath;
    public static string logPath;

    static public void LoadPreferences() 
    {
      // defaults
      gnuplotPath=@"D:\Documents and Settings\n4558146\My Documents\gnuplot\bin\wgnuplot.exe";
      logPath=@"D:\Documents and Settings\n4558146\My Documents\Visual Studio Projects\data";

      RegistryKey key=Registry.LocalMachine.OpenSubKey("Software\\PGA\\Analyser");
      if(key==null) {
        // First time started. Create initial set of registration keys.
        SavePreferences();
      } else {
        Object val;
        if((val=key.GetValue("gnuplotPath"))!=null) gnuplotPath=val.ToString();
        if((val=key.GetValue("logPath"))!=null) logPath=val.ToString();
      }
    }

    static public void SavePreferences() {
      RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
      RegistryKey subkey = key.CreateSubKey("PGA");
      RegistryKey newkey = subkey.CreateSubKey("Analyser");
      newkey.SetValue("gnuplotPath", gnuplotPath);
      newkey.SetValue("logPath", logPath);
    }
  }
}
