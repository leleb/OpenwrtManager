namespace OpenwrtManager
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      textBoxCommand = new TextBox();
      label1 = new Label();
      label2 = new Label();
      buttonSend = new Button();
      textBoxIP = new TextBox();
      label3 = new Label();
      label4 = new Label();
      textBoxPwd = new TextBox();
      buttonConnect = new Button();
      groupBoxTerminal = new GroupBox();
      tabControlRourer = new TabControl();
      tabPage1 = new TabPage();
      richTextBoxAns = new RichTextBox();
      tabPage2 = new TabPage();
      buttonSetRedirectRouter = new Button();
      dataGridViewPortForward = new DataGridView();
      buttonSaveFile = new Button();
      buttonGetRedirectRouter = new Button();
      buttonOpenFile = new Button();
      label5 = new Label();
      textBoxPort = new TextBox();
      groupBoxTerminal.SuspendLayout();
      tabControlRourer.SuspendLayout();
      tabPage1.SuspendLayout();
      tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)dataGridViewPortForward).BeginInit();
      SuspendLayout();
      // 
      // textBoxCommand
      // 
      textBoxCommand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxCommand.Location = new Point(93, 7);
      textBoxCommand.Name = "textBoxCommand";
      textBoxCommand.Size = new Size(483, 27);
      textBoxCommand.TabIndex = 0;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(6, 10);
      label1.Name = "label1";
      label1.Size = new Size(81, 20);
      label1.TabIndex = 2;
      label1.Text = "Command:";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(6, 30);
      label2.Name = "label2";
      label2.Size = new Size(60, 20);
      label2.TabIndex = 3;
      label2.Text = "Answer:";
      // 
      // buttonSend
      // 
      buttonSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonSend.Location = new Point(582, 6);
      buttonSend.Name = "buttonSend";
      buttonSend.Size = new Size(150, 29);
      buttonSend.TabIndex = 4;
      buttonSend.Text = "Send command";
      buttonSend.UseVisualStyleBackColor = true;
      buttonSend.Click += buttonSend_Click;
      // 
      // textBoxIP
      // 
      textBoxIP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      textBoxIP.Location = new Point(111, 13);
      textBoxIP.Name = "textBoxIP";
      textBoxIP.Size = new Size(184, 27);
      textBoxIP.TabIndex = 5;
      textBoxIP.Text = "127.0.0.1";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(12, 16);
      label3.Name = "label3";
      label3.Size = new Size(93, 20);
      label3.TabIndex = 6;
      label3.Text = "Router Addr:";
      // 
      // label4
      // 
      label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      label4.AutoSize = true;
      label4.Location = new Point(412, 16);
      label4.Name = "label4";
      label4.Size = new Size(73, 20);
      label4.TabIndex = 8;
      label4.Text = "Password:";
      // 
      // textBoxPwd
      // 
      textBoxPwd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      textBoxPwd.Location = new Point(491, 13);
      textBoxPwd.Name = "textBoxPwd";
      textBoxPwd.PasswordChar = '*';
      textBoxPwd.Size = new Size(123, 27);
      textBoxPwd.TabIndex = 7;
      // 
      // buttonConnect
      // 
      buttonConnect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonConnect.Location = new Point(620, 12);
      buttonConnect.Name = "buttonConnect";
      buttonConnect.Size = new Size(150, 29);
      buttonConnect.TabIndex = 9;
      buttonConnect.Text = "Connect";
      buttonConnect.UseVisualStyleBackColor = true;
      buttonConnect.Click += buttonConnect_Click;
      // 
      // groupBoxTerminal
      // 
      groupBoxTerminal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      groupBoxTerminal.Controls.Add(tabControlRourer);
      groupBoxTerminal.Enabled = false;
      groupBoxTerminal.Location = new Point(12, 46);
      groupBoxTerminal.Name = "groupBoxTerminal";
      groupBoxTerminal.Size = new Size(758, 445);
      groupBoxTerminal.TabIndex = 10;
      groupBoxTerminal.TabStop = false;
      groupBoxTerminal.Text = "Terminal";
      // 
      // tabControlRourer
      // 
      tabControlRourer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      tabControlRourer.Controls.Add(tabPage1);
      tabControlRourer.Controls.Add(tabPage2);
      tabControlRourer.Location = new Point(6, 26);
      tabControlRourer.Name = "tabControlRourer";
      tabControlRourer.SelectedIndex = 0;
      tabControlRourer.Size = new Size(746, 413);
      tabControlRourer.TabIndex = 10;
      // 
      // tabPage1
      // 
      tabPage1.Controls.Add(richTextBoxAns);
      tabPage1.Controls.Add(label2);
      tabPage1.Controls.Add(textBoxCommand);
      tabPage1.Controls.Add(label1);
      tabPage1.Controls.Add(buttonSend);
      tabPage1.Location = new Point(4, 29);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new Padding(3);
      tabPage1.Size = new Size(738, 380);
      tabPage1.TabIndex = 2;
      tabPage1.Text = "Teminal";
      tabPage1.UseVisualStyleBackColor = true;
      // 
      // richTextBoxAns
      // 
      richTextBoxAns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      richTextBoxAns.Location = new Point(6, 53);
      richTextBoxAns.Name = "richTextBoxAns";
      richTextBoxAns.Size = new Size(726, 321);
      richTextBoxAns.TabIndex = 5;
      richTextBoxAns.Text = "";
      richTextBoxAns.DoubleClick += richTextBoxAns_DoubleClick;
      // 
      // tabPage2
      // 
      tabPage2.Controls.Add(buttonSetRedirectRouter);
      tabPage2.Controls.Add(dataGridViewPortForward);
      tabPage2.Controls.Add(buttonSaveFile);
      tabPage2.Controls.Add(buttonGetRedirectRouter);
      tabPage2.Controls.Add(buttonOpenFile);
      tabPage2.Location = new Point(4, 29);
      tabPage2.Name = "tabPage2";
      tabPage2.Padding = new Padding(3);
      tabPage2.Size = new Size(738, 380);
      tabPage2.TabIndex = 1;
      tabPage2.Text = "Port forwards (redirect)";
      tabPage2.UseVisualStyleBackColor = true;
      // 
      // buttonSetRedirectRouter
      // 
      buttonSetRedirectRouter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonSetRedirectRouter.Location = new Point(426, 6);
      buttonSetRedirectRouter.Name = "buttonSetRedirectRouter";
      buttonSetRedirectRouter.Size = new Size(150, 29);
      buttonSetRedirectRouter.TabIndex = 10;
      buttonSetRedirectRouter.Text = "Set to router";
      buttonSetRedirectRouter.UseVisualStyleBackColor = true;
      buttonSetRedirectRouter.Click += buttonSetRedirectRouter_Click;
      // 
      // dataGridViewPortForward
      // 
      dataGridViewPortForward.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      dataGridViewPortForward.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      dataGridViewPortForward.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewPortForward.Location = new Point(3, 51);
      dataGridViewPortForward.Name = "dataGridViewPortForward";
      dataGridViewPortForward.RowHeadersWidth = 51;
      dataGridViewPortForward.RowTemplate.Height = 29;
      dataGridViewPortForward.Size = new Size(729, 323);
      dataGridViewPortForward.TabIndex = 6;
      // 
      // buttonSaveFile
      // 
      buttonSaveFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      buttonSaveFile.Location = new Point(582, 6);
      buttonSaveFile.Name = "buttonSaveFile";
      buttonSaveFile.Size = new Size(150, 29);
      buttonSaveFile.TabIndex = 8;
      buttonSaveFile.Text = "Save file";
      buttonSaveFile.UseVisualStyleBackColor = true;
      buttonSaveFile.Click += buttonSaveFile_Click;
      // 
      // buttonGetRedirectRouter
      // 
      buttonGetRedirectRouter.Location = new Point(162, 6);
      buttonGetRedirectRouter.Name = "buttonGetRedirectRouter";
      buttonGetRedirectRouter.Size = new Size(150, 29);
      buttonGetRedirectRouter.TabIndex = 9;
      buttonGetRedirectRouter.Text = "Get from router";
      buttonGetRedirectRouter.UseVisualStyleBackColor = true;
      buttonGetRedirectRouter.Click += buttonGetRedirectRouter_Click;
      // 
      // buttonOpenFile
      // 
      buttonOpenFile.Location = new Point(6, 6);
      buttonOpenFile.Name = "buttonOpenFile";
      buttonOpenFile.Size = new Size(150, 29);
      buttonOpenFile.TabIndex = 7;
      buttonOpenFile.Text = "Open file";
      buttonOpenFile.UseVisualStyleBackColor = true;
      buttonOpenFile.Click += buttonOpenFile_Click;
      // 
      // label5
      // 
      label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      label5.AutoSize = true;
      label5.Location = new Point(301, 16);
      label5.Name = "label5";
      label5.Size = new Size(38, 20);
      label5.TabIndex = 12;
      label5.Text = "Port:";
      // 
      // textBoxPort
      // 
      textBoxPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      textBoxPort.Location = new Point(345, 13);
      textBoxPort.Name = "textBoxPort";
      textBoxPort.Size = new Size(61, 27);
      textBoxPort.TabIndex = 11;
      textBoxPort.Text = "22";
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(782, 503);
      Controls.Add(label5);
      Controls.Add(textBoxPort);
      Controls.Add(groupBoxTerminal);
      Controls.Add(buttonConnect);
      Controls.Add(label4);
      Controls.Add(textBoxPwd);
      Controls.Add(label3);
      Controls.Add(textBoxIP);
      Name = "Form1";
      Text = "OpenwrtManager";
      groupBoxTerminal.ResumeLayout(false);
      tabControlRourer.ResumeLayout(false);
      tabPage1.ResumeLayout(false);
      tabPage1.PerformLayout();
      tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)dataGridViewPortForward).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TextBox textBoxCommand;
    private Label label1;
    private Label label2;
    private Button buttonSend;
    private TextBox textBoxIP;
    private Label label3;
    private Label label4;
    private TextBox textBoxPwd;
    private Button buttonConnect;
    private GroupBox groupBoxTerminal;
    private RichTextBox richTextBoxAns;
    private DataGridView dataGridViewPortForward;
    private Button buttonOpenFile;
    private Button buttonSaveFile;
    private TabControl tabControlRourer;
    private TabPage tabPage2;
    private Button buttonSetRedirectRouter;
    private Button buttonGetRedirectRouter;
    private TabPage tabPage1;
    private Label label5;
    private TextBox textBoxPort;
  }
}