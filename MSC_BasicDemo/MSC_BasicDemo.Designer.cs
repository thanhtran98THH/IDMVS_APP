namespace MSC_BasicDemo
{
    partial class Form1
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
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bnClose = new System.Windows.Forms.Button();
            this.bnOpen = new System.Windows.Forms.Button();
            this.bnEnum = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bnTriggerExec = new System.Windows.Forms.Button();
            this.cbSoftTrigger = new System.Windows.Forms.CheckBox();
            this.bnStopGrab = new System.Windows.Forms.Button();
            this.bnStartGrab = new System.Windows.Forms.Button();
            this.bnTriggerMode = new System.Windows.Forms.RadioButton();
            this.bnContinuesMode = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bnSetParam = new System.Windows.Forms.Button();
            this.bnGetParam = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFrameRate = new System.Windows.Forms.TextBox();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.tbExposure = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownR = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.row = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownStartX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStartY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonRoi = new System.Windows.Forms.Button();
            this.numericUpDownBinary = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownThresholdMin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThresholdMax = new System.Windows.Forms.NumericUpDown();
            this.buttonNewContour = new System.Windows.Forms.Button();
            this.buttonMono = new System.Windows.Forms.Button();
            this.findContour = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBoxCV = new System.Windows.Forms.PictureBox();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.dataGridViewShowData = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDrawRoi = new System.Windows.Forms.Button();
            this.buttonDrawConer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartY)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowData)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            this.cbDeviceList.Location = new System.Drawing.Point(3, 22);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Size = new System.Drawing.Size(505, 21);
            this.cbDeviceList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bnClose);
            this.groupBox1.Controls.Add(this.bnOpen);
            this.groupBox1.Controls.Add(this.bnEnum);
            this.groupBox1.Location = new System.Drawing.Point(265, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 118);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Initialization";
            // 
            // bnClose
            // 
            this.bnClose.Enabled = false;
            this.bnClose.Location = new System.Drawing.Point(142, 75);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(88, 25);
            this.bnClose.TabIndex = 2;
            this.bnClose.Text = "Close Device";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnOpen
            // 
            this.bnOpen.Location = new System.Drawing.Point(20, 75);
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.Size = new System.Drawing.Size(83, 25);
            this.bnOpen.TabIndex = 1;
            this.bnOpen.Text = "Open Device";
            this.bnOpen.UseVisualStyleBackColor = true;
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // bnEnum
            // 
            this.bnEnum.Location = new System.Drawing.Point(20, 22);
            this.bnEnum.Name = "bnEnum";
            this.bnEnum.Size = new System.Drawing.Size(210, 25);
            this.bnEnum.TabIndex = 0;
            this.bnEnum.Text = "Search Device";
            this.bnEnum.UseVisualStyleBackColor = true;
            this.bnEnum.Click += new System.EventHandler(this.bnEnum_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bnTriggerExec);
            this.groupBox2.Controls.Add(this.cbSoftTrigger);
            this.groupBox2.Controls.Add(this.bnStopGrab);
            this.groupBox2.Controls.Add(this.bnStartGrab);
            this.groupBox2.Controls.Add(this.bnTriggerMode);
            this.groupBox2.Controls.Add(this.bnContinuesMode);
            this.groupBox2.Location = new System.Drawing.Point(265, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 152);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Acquisition";
            // 
            // bnTriggerExec
            // 
            this.bnTriggerExec.Enabled = false;
            this.bnTriggerExec.Location = new System.Drawing.Point(162, 107);
            this.bnTriggerExec.Name = "bnTriggerExec";
            this.bnTriggerExec.Size = new System.Drawing.Size(75, 25);
            this.bnTriggerExec.TabIndex = 5;
            this.bnTriggerExec.Text = "Trigger Once";
            this.bnTriggerExec.UseVisualStyleBackColor = true;
            this.bnTriggerExec.Click += new System.EventHandler(this.bnTriggerExec_Click);
            // 
            // cbSoftTrigger
            // 
            this.cbSoftTrigger.AutoSize = true;
            this.cbSoftTrigger.Enabled = false;
            this.cbSoftTrigger.Location = new System.Drawing.Point(18, 112);
            this.cbSoftTrigger.Name = "cbSoftTrigger";
            this.cbSoftTrigger.Size = new System.Drawing.Size(118, 17);
            this.cbSoftTrigger.TabIndex = 4;
            this.cbSoftTrigger.Text = "Trigger by Software";
            this.cbSoftTrigger.UseVisualStyleBackColor = true;
            this.cbSoftTrigger.CheckedChanged += new System.EventHandler(this.cbSoftTrigger_CheckedChanged);
            // 
            // bnStopGrab
            // 
            this.bnStopGrab.Enabled = false;
            this.bnStopGrab.Location = new System.Drawing.Point(142, 67);
            this.bnStopGrab.Name = "bnStopGrab";
            this.bnStopGrab.Size = new System.Drawing.Size(95, 25);
            this.bnStopGrab.TabIndex = 3;
            this.bnStopGrab.Text = "Stop";
            this.bnStopGrab.UseVisualStyleBackColor = true;
            this.bnStopGrab.Click += new System.EventHandler(this.bnStopGrab_Click);
            // 
            // bnStartGrab
            // 
            this.bnStartGrab.Enabled = false;
            this.bnStartGrab.Location = new System.Drawing.Point(20, 67);
            this.bnStartGrab.Name = "bnStartGrab";
            this.bnStartGrab.Size = new System.Drawing.Size(83, 25);
            this.bnStartGrab.TabIndex = 2;
            this.bnStartGrab.Text = "Start";
            this.bnStartGrab.UseVisualStyleBackColor = true;
            this.bnStartGrab.Click += new System.EventHandler(this.bnStartGrab_Click);
            // 
            // bnTriggerMode
            // 
            this.bnTriggerMode.AutoSize = true;
            this.bnTriggerMode.Enabled = false;
            this.bnTriggerMode.Location = new System.Drawing.Point(142, 28);
            this.bnTriggerMode.Name = "bnTriggerMode";
            this.bnTriggerMode.Size = new System.Drawing.Size(88, 17);
            this.bnTriggerMode.TabIndex = 1;
            this.bnTriggerMode.TabStop = true;
            this.bnTriggerMode.Text = "Trigger Mode";
            this.bnTriggerMode.UseMnemonic = false;
            this.bnTriggerMode.UseVisualStyleBackColor = true;
            this.bnTriggerMode.CheckedChanged += new System.EventHandler(this.bnTriggerMode_CheckedChanged);
            // 
            // bnContinuesMode
            // 
            this.bnContinuesMode.AutoSize = true;
            this.bnContinuesMode.Enabled = false;
            this.bnContinuesMode.Location = new System.Drawing.Point(20, 28);
            this.bnContinuesMode.Name = "bnContinuesMode";
            this.bnContinuesMode.Size = new System.Drawing.Size(78, 17);
            this.bnContinuesMode.TabIndex = 0;
            this.bnContinuesMode.TabStop = true;
            this.bnContinuesMode.Text = "Continuous";
            this.bnContinuesMode.UseVisualStyleBackColor = true;
            this.bnContinuesMode.CheckedChanged += new System.EventHandler(this.bnContinuesMode_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bnSetParam);
            this.groupBox4.Controls.Add(this.bnGetParam);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tbFrameRate);
            this.groupBox4.Controls.Add(this.tbGain);
            this.groupBox4.Controls.Add(this.tbExposure);
            this.groupBox4.Location = new System.Drawing.Point(266, 334);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(243, 194);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            // 
            // bnSetParam
            // 
            this.bnSetParam.Enabled = false;
            this.bnSetParam.Location = new System.Drawing.Point(137, 148);
            this.bnSetParam.Name = "bnSetParam";
            this.bnSetParam.Size = new System.Drawing.Size(93, 25);
            this.bnSetParam.TabIndex = 7;
            this.bnSetParam.Text = "Set Parameter";
            this.bnSetParam.UseVisualStyleBackColor = true;
            this.bnSetParam.Click += new System.EventHandler(this.bnSetParam_Click);
            // 
            // bnGetParam
            // 
            this.bnGetParam.Enabled = false;
            this.bnGetParam.Location = new System.Drawing.Point(18, 148);
            this.bnGetParam.Name = "bnGetParam";
            this.bnGetParam.Size = new System.Drawing.Size(85, 25);
            this.bnGetParam.TabIndex = 6;
            this.bnGetParam.Text = "Get Parameter";
            this.bnGetParam.UseVisualStyleBackColor = true;
            this.bnGetParam.Click += new System.EventHandler(this.bnGetParam_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Frame Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gain";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Exposure Time";
            // 
            // tbFrameRate
            // 
            this.tbFrameRate.Enabled = false;
            this.tbFrameRate.Location = new System.Drawing.Point(137, 103);
            this.tbFrameRate.Name = "tbFrameRate";
            this.tbFrameRate.Size = new System.Drawing.Size(100, 20);
            this.tbFrameRate.TabIndex = 2;
            // 
            // tbGain
            // 
            this.tbGain.Enabled = false;
            this.tbGain.Location = new System.Drawing.Point(137, 64);
            this.tbGain.Name = "tbGain";
            this.tbGain.Size = new System.Drawing.Size(100, 20);
            this.tbGain.TabIndex = 1;
            // 
            // tbExposure
            // 
            this.tbExposure.Enabled = false;
            this.tbExposure.Location = new System.Drawing.Point(137, 27);
            this.tbExposure.Name = "tbExposure";
            this.tbExposure.Size = new System.Drawing.Size(100, 20);
            this.tbExposure.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox2.Location = new System.Drawing.Point(513, 62);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(161, 40);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(964, 745);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 49);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(257, 602);
            this.listBox1.TabIndex = 7;
            // 
            // numericUpDownH
            // 
            this.numericUpDownH.Location = new System.Drawing.Point(70, 13);
            this.numericUpDownH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownH.TabIndex = 8;
            this.numericUpDownH.ValueChanged += new System.EventHandler(this.numericUpDownH_ValueChanged);
            // 
            // numericUpDownR
            // 
            this.numericUpDownR.Location = new System.Drawing.Point(70, 39);
            this.numericUpDownR.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownR.Name = "numericUpDownR";
            this.numericUpDownR.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownR.TabIndex = 9;
            this.numericUpDownR.ValueChanged += new System.EventHandler(this.numericUpDownR_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "height";
            // 
            // row
            // 
            this.row.AutoSize = true;
            this.row.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.row.Location = new System.Drawing.Point(6, 46);
            this.row.Name = "row";
            this.row.Size = new System.Drawing.Size(24, 13);
            this.row.TabIndex = 11;
            this.row.Text = "row";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(70, 65);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "offset";
            // 
            // numericUpDownStartX
            // 
            this.numericUpDownStartX.Location = new System.Drawing.Point(1347, 608);
            this.numericUpDownStartX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStartX.Name = "numericUpDownStartX";
            this.numericUpDownStartX.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownStartX.TabIndex = 14;
            this.numericUpDownStartX.ValueChanged += new System.EventHandler(this.numericUpDownStartX_ValueChanged);
            // 
            // numericUpDownStartY
            // 
            this.numericUpDownStartY.Location = new System.Drawing.Point(1347, 634);
            this.numericUpDownStartY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStartY.Name = "numericUpDownStartY";
            this.numericUpDownStartY.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownStartY.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1283, 610);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "StartX";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(1284, 641);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "StartY";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1503, 777);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1495, 751);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewShowData);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.cbDeviceList);
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1489, 745);
            this.splitContainer1.SplitterDistance = 964;
            this.splitContainer1.TabIndex = 21;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonDrawConer);
            this.groupBox3.Controls.Add(this.buttonDrawRoi);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDownH);
            this.groupBox3.Controls.Add(this.numericUpDownR);
            this.groupBox3.Controls.Add(this.row);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(267, 534);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 206);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonProcess);
            this.tabPage2.Controls.Add(this.buttonLoad);
            this.tabPage2.Controls.Add(this.buttonRoi);
            this.tabPage2.Controls.Add(this.numericUpDownBinary);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.numericUpDownThresholdMin);
            this.tabPage2.Controls.Add(this.numericUpDownThresholdMax);
            this.tabPage2.Controls.Add(this.buttonNewContour);
            this.tabPage2.Controls.Add(this.buttonMono);
            this.tabPage2.Controls.Add(this.findContour);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.pictureBoxCV);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1495, 751);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(1357, 679);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(90, 41);
            this.buttonLoad.TabIndex = 11;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonRoi
            // 
            this.buttonRoi.Location = new System.Drawing.Point(1343, 183);
            this.buttonRoi.Name = "buttonRoi";
            this.buttonRoi.Size = new System.Drawing.Size(75, 23);
            this.buttonRoi.TabIndex = 10;
            this.buttonRoi.Text = "Roi";
            this.buttonRoi.UseVisualStyleBackColor = true;
            this.buttonRoi.Click += new System.EventHandler(this.buttonRoi_Click);
            // 
            // numericUpDownBinary
            // 
            this.numericUpDownBinary.Location = new System.Drawing.Point(1357, 525);
            this.numericUpDownBinary.Name = "numericUpDownBinary";
            this.numericUpDownBinary.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBinary.TabIndex = 9;
            this.numericUpDownBinary.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownBinary.ValueChanged += new System.EventHandler(this.numericUpDownBinary_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1321, 527);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Binary";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1324, 501);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "MIN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1321, 475);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "MAX";
            // 
            // numericUpDownThresholdMin
            // 
            this.numericUpDownThresholdMin.Location = new System.Drawing.Point(1357, 499);
            this.numericUpDownThresholdMin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThresholdMin.Name = "numericUpDownThresholdMin";
            this.numericUpDownThresholdMin.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownThresholdMin.TabIndex = 6;
            this.numericUpDownThresholdMin.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownThresholdMin.ValueChanged += new System.EventHandler(this.numericUpDownThresholdMin_ValueChanged);
            // 
            // numericUpDownThresholdMax
            // 
            this.numericUpDownThresholdMax.Location = new System.Drawing.Point(1357, 473);
            this.numericUpDownThresholdMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThresholdMax.Name = "numericUpDownThresholdMax";
            this.numericUpDownThresholdMax.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownThresholdMax.TabIndex = 6;
            this.numericUpDownThresholdMax.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownThresholdMax.ValueChanged += new System.EventHandler(this.numericUpDownThreshold_ValueChanged);
            // 
            // buttonNewContour
            // 
            this.buttonNewContour.Enabled = false;
            this.buttonNewContour.Location = new System.Drawing.Point(1343, 154);
            this.buttonNewContour.Name = "buttonNewContour";
            this.buttonNewContour.Size = new System.Drawing.Size(75, 23);
            this.buttonNewContour.TabIndex = 5;
            this.buttonNewContour.Text = "new contour";
            this.buttonNewContour.UseVisualStyleBackColor = true;
            this.buttonNewContour.Click += new System.EventHandler(this.buttonNewContour_Click);
            // 
            // buttonMono
            // 
            this.buttonMono.Location = new System.Drawing.Point(1343, 125);
            this.buttonMono.Name = "buttonMono";
            this.buttonMono.Size = new System.Drawing.Size(75, 23);
            this.buttonMono.TabIndex = 4;
            this.buttonMono.Text = "Mono";
            this.buttonMono.UseVisualStyleBackColor = true;
            this.buttonMono.Click += new System.EventHandler(this.buttonMono_Click);
            // 
            // findContour
            // 
            this.findContour.Location = new System.Drawing.Point(1343, 96);
            this.findContour.Name = "findContour";
            this.findContour.Size = new System.Drawing.Size(75, 23);
            this.findContour.TabIndex = 3;
            this.findContour.Text = "Contour";
            this.findContour.UseVisualStyleBackColor = true;
            this.findContour.Click += new System.EventHandler(this.findContour_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1343, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "binary";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1343, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBoxCV
            // 
            this.pictureBoxCV.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBoxCV.Location = new System.Drawing.Point(6, 6);
            this.pictureBoxCV.Name = "pictureBoxCV";
            this.pictureBoxCV.Size = new System.Drawing.Size(1309, 737);
            this.pictureBoxCV.TabIndex = 0;
            this.pictureBoxCV.TabStop = false;
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(1343, 225);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(75, 23);
            this.buttonProcess.TabIndex = 12;
            this.buttonProcess.Text = "Filter";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // dataGridViewShowData
            // 
            this.dataGridViewShowData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShowData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.data});
            this.dataGridViewShowData.Location = new System.Drawing.Point(3, 49);
            this.dataGridViewShowData.Name = "dataGridViewShowData";
            this.dataGridViewShowData.Size = new System.Drawing.Size(257, 691);
            this.dataGridViewShowData.TabIndex = 20;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.Width = 50;
            // 
            // data
            // 
            this.data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.data.HeaderText = "Code";
            this.data.Name = "data";
            // 
            // buttonDrawRoi
            // 
            this.buttonDrawRoi.Location = new System.Drawing.Point(13, 118);
            this.buttonDrawRoi.Name = "buttonDrawRoi";
            this.buttonDrawRoi.Size = new System.Drawing.Size(88, 49);
            this.buttonDrawRoi.TabIndex = 14;
            this.buttonDrawRoi.Text = "ROI";
            this.buttonDrawRoi.UseVisualStyleBackColor = true;
            this.buttonDrawRoi.Click += new System.EventHandler(this.buttonDrawRoi_Click);
            // 
            // buttonDrawConer
            // 
            this.buttonDrawConer.Location = new System.Drawing.Point(126, 118);
            this.buttonDrawConer.Name = "buttonDrawConer";
            this.buttonDrawConer.Size = new System.Drawing.Size(88, 49);
            this.buttonDrawConer.TabIndex = 14;
            this.buttonDrawConer.Text = "CONER";
            this.buttonDrawConer.UseVisualStyleBackColor = true;
            this.buttonDrawConer.Click += new System.EventHandler(this.buttonDrawConer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1503, 777);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownStartY);
            this.Controls.Add(this.numericUpDownStartX);
            this.Name = "Form1";
            this.Text = "Machine Vision Camera SDK Basic Demo (C#)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartY)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBinary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShowData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.Button bnEnum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton bnTriggerMode;
        private System.Windows.Forms.RadioButton bnContinuesMode;
        private System.Windows.Forms.CheckBox cbSoftTrigger;
        private System.Windows.Forms.Button bnStopGrab;
        private System.Windows.Forms.Button bnStartGrab;
        private System.Windows.Forms.Button bnTriggerExec;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbFrameRate;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnSetParam;
        private System.Windows.Forms.Button bnGetParam;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownH;
        private System.Windows.Forms.NumericUpDown numericUpDownR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label row;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownStartX;
        private System.Windows.Forms.NumericUpDown numericUpDownStartY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxCV;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button findContour;
        private System.Windows.Forms.Button buttonMono;
        private System.Windows.Forms.Button buttonNewContour;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdMax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdMin;
        private System.Windows.Forms.NumericUpDown numericUpDownBinary;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonRoi;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.DataGridView dataGridViewShowData;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.Button buttonDrawRoi;
        private System.Windows.Forms.Button buttonDrawConer;
    }
}

