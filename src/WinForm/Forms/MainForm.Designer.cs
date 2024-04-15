using System.Windows.Forms;

namespace WinForm.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel = new TableLayoutPanel();
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            saveUiStateToolStripMenuItem = new ToolStripMenuItem();
            clearLogsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            groupBoxLogs = new GroupBox();
            textBoxLog = new Serilog.Sinks.WinForms.Core.RichTextBoxLogControl();
            LeftTableLayoutPanel = new TableLayoutPanel();
            groupBoxAfter = new GroupBox();
            textBoxAfter = new RichTextBox();
            groupBoxBefore = new GroupBox();
            textBoxBefore = new RichTextBox();
            groupBoxControls = new GroupBox();
            labelFile = new Label();
            textBoxFilePath = new Controls.PlaceHolderTextBox();
            buttonBrowseFile = new Button();
            textBoxJsonKeys = new Controls.PlaceHolderTextBox();
            labelAesKey = new Label();
            textBoxAesKey = new Controls.PlaceHolderTextBox();
            buttonGenerateAesKey = new Button();
            labelAesIv = new Label();
            textBoxAesIv = new Controls.PlaceHolderTextBox();
            buttonGenerateAesIV = new Button();
            buttonEncrypt = new Button();
            buttonDecrypt = new Button();
            tableLayoutPanel.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBoxLogs.SuspendLayout();
            LeftTableLayoutPanel.SuspendLayout();
            groupBoxAfter.SuspendLayout();
            groupBoxBefore.SuspendLayout();
            groupBoxControls.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 800F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(toolStrip1, 0, 0);
            tableLayoutPanel.Controls.Add(groupBoxLogs, 1, 1);
            tableLayoutPanel.Controls.Add(LeftTableLayoutPanel, 0, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(1434, 636);
            tableLayoutPanel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            tableLayoutPanel.SetColumnSpan(toolStrip1, 2);
            toolStrip1.Dock = DockStyle.Fill;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1434, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { saveUiStateToolStripMenuItem, clearLogsToolStripMenuItem, exitToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // saveUiStateToolStripMenuItem
            // 
            saveUiStateToolStripMenuItem.Name = "saveUiStateToolStripMenuItem";
            saveUiStateToolStripMenuItem.Size = new Size(141, 22);
            saveUiStateToolStripMenuItem.Text = "Save Ui State";
            saveUiStateToolStripMenuItem.Click += saveUiStateToolStripMenuItem_Click;
            // 
            // clearLogsToolStripMenuItem
            // 
            clearLogsToolStripMenuItem.Name = "clearLogsToolStripMenuItem";
            clearLogsToolStripMenuItem.Size = new Size(141, 22);
            clearLogsToolStripMenuItem.Text = "Clear Logs";
            clearLogsToolStripMenuItem.Click += clearLogsToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(141, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // groupBoxLogs
            // 
            groupBoxLogs.Controls.Add(textBoxLog);
            groupBoxLogs.Dock = DockStyle.Fill;
            groupBoxLogs.Location = new Point(804, 29);
            groupBoxLogs.Margin = new Padding(4);
            groupBoxLogs.Name = "groupBoxLogs";
            groupBoxLogs.Padding = new Padding(4);
            groupBoxLogs.Size = new Size(626, 603);
            groupBoxLogs.TabIndex = 2;
            groupBoxLogs.TabStop = false;
            groupBoxLogs.Text = "Logs";
            // 
            // textBoxLog
            // 
            textBoxLog.Dock = DockStyle.Fill;
            textBoxLog.ForContext = "";
            textBoxLog.Location = new Point(4, 26);
            textBoxLog.Name = "textBoxLog";
            textBoxLog.Size = new Size(618, 573);
            textBoxLog.TabIndex = 1;
            textBoxLog.Text = "";
            // 
            // LeftTableLayoutPanel
            // 
            LeftTableLayoutPanel.ColumnCount = 2;
            LeftTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LeftTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            LeftTableLayoutPanel.Controls.Add(groupBoxAfter, 1, 1);
            LeftTableLayoutPanel.Controls.Add(groupBoxBefore, 0, 1);
            LeftTableLayoutPanel.Controls.Add(groupBoxControls, 0, 0);
            LeftTableLayoutPanel.Dock = DockStyle.Fill;
            LeftTableLayoutPanel.Location = new Point(3, 28);
            LeftTableLayoutPanel.Name = "LeftTableLayoutPanel";
            LeftTableLayoutPanel.RowCount = 2;
            LeftTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            LeftTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            LeftTableLayoutPanel.Size = new Size(794, 605);
            LeftTableLayoutPanel.TabIndex = 3;
            // 
            // groupBoxAfter
            // 
            groupBoxAfter.Controls.Add(textBoxAfter);
            groupBoxAfter.Dock = DockStyle.Fill;
            groupBoxAfter.Font = new Font("Segoe UI", 12F);
            groupBoxAfter.Location = new Point(400, 203);
            groupBoxAfter.Name = "groupBoxAfter";
            groupBoxAfter.Size = new Size(391, 399);
            groupBoxAfter.TabIndex = 20;
            groupBoxAfter.TabStop = false;
            groupBoxAfter.Text = "After";
            // 
            // textBoxAfter
            // 
            textBoxAfter.Dock = DockStyle.Fill;
            textBoxAfter.Font = new Font("Segoe UI", 9.75F);
            textBoxAfter.Location = new Point(3, 25);
            textBoxAfter.Name = "textBoxAfter";
            textBoxAfter.Size = new Size(385, 371);
            textBoxAfter.TabIndex = 1;
            textBoxAfter.Text = "";
            // 
            // groupBoxBefore
            // 
            groupBoxBefore.Controls.Add(textBoxBefore);
            groupBoxBefore.Dock = DockStyle.Fill;
            groupBoxBefore.Font = new Font("Segoe UI", 12F);
            groupBoxBefore.Location = new Point(3, 203);
            groupBoxBefore.Name = "groupBoxBefore";
            groupBoxBefore.Size = new Size(391, 399);
            groupBoxBefore.TabIndex = 19;
            groupBoxBefore.TabStop = false;
            groupBoxBefore.Text = "Before";
            // 
            // textBoxBefore
            // 
            textBoxBefore.Dock = DockStyle.Fill;
            textBoxBefore.Font = new Font("Segoe UI", 9.75F);
            textBoxBefore.Location = new Point(3, 25);
            textBoxBefore.Name = "textBoxBefore";
            textBoxBefore.Size = new Size(385, 371);
            textBoxBefore.TabIndex = 0;
            textBoxBefore.Text = "";
            // 
            // groupBoxControls
            // 
            LeftTableLayoutPanel.SetColumnSpan(groupBoxControls, 2);
            groupBoxControls.Controls.Add(labelFile);
            groupBoxControls.Controls.Add(textBoxFilePath);
            groupBoxControls.Controls.Add(buttonBrowseFile);
            groupBoxControls.Controls.Add(textBoxJsonKeys);
            groupBoxControls.Controls.Add(labelAesKey);
            groupBoxControls.Controls.Add(textBoxAesKey);
            groupBoxControls.Controls.Add(buttonGenerateAesKey);
            groupBoxControls.Controls.Add(labelAesIv);
            groupBoxControls.Controls.Add(textBoxAesIv);
            groupBoxControls.Controls.Add(buttonGenerateAesIV);
            groupBoxControls.Controls.Add(buttonEncrypt);
            groupBoxControls.Controls.Add(buttonDecrypt);
            groupBoxControls.Dock = DockStyle.Fill;
            groupBoxControls.Location = new Point(3, 3);
            groupBoxControls.Name = "groupBoxControls";
            groupBoxControls.Size = new Size(788, 194);
            groupBoxControls.TabIndex = 0;
            groupBoxControls.TabStop = false;
            groupBoxControls.Text = "Controls";
            // 
            // labelFile
            // 
            labelFile.AutoSize = true;
            labelFile.Location = new Point(25, 29);
            labelFile.Name = "labelFile";
            labelFile.Size = new Size(37, 21);
            labelFile.TabIndex = 21;
            labelFile.Text = "File:";
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            textBoxFilePath.ForeColor = Color.Gray;
            textBoxFilePath.Location = new Point(76, 29);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.PlaceholderText = "Enter the path to json file.";
            textBoxFilePath.PlaceHolderText = "";
            textBoxFilePath.Size = new Size(629, 23);
            textBoxFilePath.TabIndex = 29;
            // 
            // buttonBrowseFile
            // 
            buttonBrowseFile.Location = new Point(710, 19);
            buttonBrowseFile.Name = "buttonBrowseFile";
            buttonBrowseFile.Size = new Size(51, 34);
            buttonBrowseFile.TabIndex = 22;
            buttonBrowseFile.Text = "📁";
            buttonBrowseFile.UseVisualStyleBackColor = true;
            buttonBrowseFile.Click += buttonBrowseFile_Click;
            // 
            // textBoxJsonKeys
            // 
            textBoxJsonKeys.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            textBoxJsonKeys.ForeColor = Color.Gray;
            textBoxJsonKeys.Location = new Point(76, 67);
            textBoxJsonKeys.Name = "textBoxJsonKeys";
            textBoxJsonKeys.PlaceholderText = "Specify the json keys you would like to encrypt and/or decrypt. Use ; to separate keys e.g. property1;property2;property3.Name";
            textBoxJsonKeys.PlaceHolderText = "";
            textBoxJsonKeys.Size = new Size(682, 23);
            textBoxJsonKeys.TabIndex = 32;
            // 
            // labelAesKey
            // 
            labelAesKey.AutoSize = true;
            labelAesKey.Location = new Point(24, 113);
            labelAesKey.Name = "labelAesKey";
            labelAesKey.Size = new Size(38, 21);
            labelAesKey.TabIndex = 25;
            labelAesKey.Text = "Key:";
            // 
            // textBoxAesKey
            // 
            textBoxAesKey.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            textBoxAesKey.ForeColor = Color.Gray;
            textBoxAesKey.Location = new Point(76, 115);
            textBoxAesKey.Name = "textBoxAesKey";
            textBoxAesKey.PlaceholderText = "Enter your AES Key (256 Bits) in Base64 String.";
            textBoxAesKey.PlaceHolderText = "";
            textBoxAesKey.Size = new Size(307, 23);
            textBoxAesKey.TabIndex = 30;
            // 
            // buttonGenerateAesKey
            // 
            buttonGenerateAesKey.Location = new Point(389, 106);
            buttonGenerateAesKey.Name = "buttonGenerateAesKey";
            buttonGenerateAesKey.Size = new Size(51, 34);
            buttonGenerateAesKey.TabIndex = 28;
            buttonGenerateAesKey.Text = "🔄";
            buttonGenerateAesKey.UseVisualStyleBackColor = true;
            buttonGenerateAesKey.Click += buttonGenerateAesKey_Click;
            // 
            // labelAesIv
            // 
            labelAesIv.AutoSize = true;
            labelAesIv.Location = new Point(457, 113);
            labelAesIv.Name = "labelAesIv";
            labelAesIv.Size = new Size(27, 21);
            labelAesIv.TabIndex = 26;
            labelAesIv.Text = "IV:";
            // 
            // textBoxAesIv
            // 
            textBoxAesIv.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            textBoxAesIv.ForeColor = Color.Gray;
            textBoxAesIv.Location = new Point(492, 115);
            textBoxAesIv.Name = "textBoxAesIv";
            textBoxAesIv.PlaceholderText = "Enter your AES IV in Base64 String.";
            textBoxAesIv.PlaceHolderText = "";
            textBoxAesIv.Size = new Size(212, 23);
            textBoxAesIv.TabIndex = 31;
            // 
            // buttonGenerateAesIV
            // 
            buttonGenerateAesIV.Location = new Point(710, 106);
            buttonGenerateAesIV.Name = "buttonGenerateAesIV";
            buttonGenerateAesIV.Size = new Size(51, 34);
            buttonGenerateAesIV.TabIndex = 27;
            buttonGenerateAesIV.Text = "🔄";
            buttonGenerateAesIV.UseVisualStyleBackColor = true;
            buttonGenerateAesIV.Click += buttonGenerateAesIV_Click;
            // 
            // buttonEncrypt
            // 
            buttonEncrypt.Location = new Point(21, 149);
            buttonEncrypt.Name = "buttonEncrypt";
            buttonEncrypt.Size = new Size(365, 36);
            buttonEncrypt.TabIndex = 23;
            buttonEncrypt.Text = "🔒 Encrypt";
            buttonEncrypt.UseVisualStyleBackColor = true;
            buttonEncrypt.Click += buttonEncrypt_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(396, 149);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(365, 36);
            buttonDecrypt.TabIndex = 24;
            buttonDecrypt.Text = "🔓 Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1434, 636);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "JsonEncryptor";
            FormClosing += MainForm_FormClosing;
            Shown += MainForm_Shown;
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBoxLogs.ResumeLayout(false);
            LeftTableLayoutPanel.ResumeLayout(false);
            groupBoxAfter.ResumeLayout(false);
            groupBoxBefore.ResumeLayout(false);
            groupBoxControls.ResumeLayout(false);
            groupBoxControls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private ToolStrip toolStrip1;
        private GroupBox groupBoxLogs;
        private Serilog.Sinks.WinForms.Core.RichTextBoxLogControl textBoxLog;
        private TableLayoutPanel LeftTableLayoutPanel;
        private GroupBox groupBoxControls;
        private GroupBox groupBoxBefore;
        private RichTextBox textBoxBefore;
        private GroupBox groupBoxAfter;
        private RichTextBox textBoxAfter;
        private Label labelFile;
        private Controls.PlaceHolderTextBox textBoxFilePath;
        private Button buttonBrowseFile;
        private Controls.PlaceHolderTextBox textBoxJsonKeys;
        private Label labelAesKey;
        private Controls.PlaceHolderTextBox textBoxAesKey;
        private Button buttonGenerateAesKey;
        private Label labelAesIv;
        private Controls.PlaceHolderTextBox textBoxAesIv;
        private Button buttonGenerateAesIV;
        private Button buttonEncrypt;
        private Button buttonDecrypt;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem clearLogsToolStripMenuItem;
        private ToolStripMenuItem saveUiStateToolStripMenuItem;
    }
}