namespace TestWcfDeviceService
{
    partial class frmTdmSimulator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTdmSimulator));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.ofdXmlFile = new System.Windows.Forms.OpenFileDialog();
            this.trvMethods = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.tabPerformance = new System.Windows.Forms.TabPage();
            this.tbPerformance = new System.Windows.Forms.TabControl();
            this.tbDescDevice = new System.Windows.Forms.TabPage();
            this.cmdClearRows_Performance_DescDevice = new System.Windows.Forms.Button();
            this.dgvPerformance_DescDevice = new System.Windows.Forms.DataGridView();
            this.cmdInvokeMethod_performance_DescDevice = new System.Windows.Forms.Button();
            this.lblRowCount_performance_DescDevice = new System.Windows.Forms.Label();
            this.cmdAdd_performance_DescDevice = new System.Windows.Forms.Button();
            this.txtRowCount_performance_DescDevice = new System.Windows.Forms.TextBox();
            this.tbSendAlert = new System.Windows.Forms.TabPage();
            this.cmdClear_Performance_SendAlert = new System.Windows.Forms.Button();
            this.dgv_Performance_SendAlert = new System.Windows.Forms.DataGridView();
            this.cmdSendAlert_Performance = new System.Windows.Forms.Button();
            this.lblRowCount_Performance_SendAlert = new System.Windows.Forms.Label();
            this.cmdRowAdd_Performance_SendAlert = new System.Windows.Forms.Button();
            this.txtRowCount_Performance_SendAlert = new System.Windows.Forms.TextBox();
            this.tbOther = new System.Windows.Forms.TabPage();
            this.grpAlertUsingXml = new System.Windows.Forms.GroupBox();
            this.cmdStartThread = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEvery = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberOfPkgs = new System.Windows.Forms.TextBox();
            this.cmdSelectedPkg = new System.Windows.Forms.Button();
            this.tbRmteCmd = new System.Windows.Forms.TabPage();
            this.remoteCmdSplitContainer = new System.Windows.Forms.SplitContainer();
            this.cmdGetQueuedRemoteCommand = new System.Windows.Forms.Button();
            this.cmdRemoteCommandExecute = new System.Windows.Forms.Button();
            this.cmdRemoteCommandClear = new System.Windows.Forms.Button();
            this.dgvPendingRemoteCommands = new System.Windows.Forms.DataGridView();
            this.tbcompdevicechng = new System.Windows.Forms.TabPage();
            this.cmdRowAdd_Performance_Component_Click = new System.Windows.Forms.Button();
            this.txtRowCount_Performance_Component = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdClearDeviceComponentChange_Performance_Click = new System.Windows.Forms.Button();
            this.cmdDeviceComponentChange_Performance = new System.Windows.Forms.Button();
            this.dgvReportDeviceComponentChange = new System.Windows.Forms.DataGridView();
            this.DeviceGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Manufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddRemove = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StatusDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbbulkdescribedevice = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabInput.SuspendLayout();
            this.tbOutput.SuspendLayout();
            this.tabPerformance.SuspendLayout();
            this.tbPerformance.SuspendLayout();
            this.tbDescDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPerformance_DescDevice)).BeginInit();
            this.tbSendAlert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Performance_SendAlert)).BeginInit();
            this.tbOther.SuspendLayout();
            this.grpAlertUsingXml.SuspendLayout();
            this.tbRmteCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remoteCmdSplitContainer)).BeginInit();
            this.remoteCmdSplitContainer.Panel1.SuspendLayout();
            this.remoteCmdSplitContainer.Panel2.SuspendLayout();
            this.remoteCmdSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingRemoteCommands)).BeginInit();
            this.tbcompdevicechng.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportDeviceComponentChange)).BeginInit();
            this.tbbulkdescribedevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(510, 391);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(232, 23);
            this.btnExecute.TabIndex = 10;
            this.btnExecute.Text = "Check";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(305, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(3, 6);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(296, 20);
            this.txtFile.TabIndex = 13;
            // 
            // trvMethods
            // 
            this.trvMethods.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvMethods.ImageIndex = 0;
            this.trvMethods.ImageList = this.imageList1;
            this.trvMethods.Location = new System.Drawing.Point(13, 12);
            this.trvMethods.Name = "trvMethods";
            this.trvMethods.SelectedImageIndex = 0;
            this.trvMethods.Size = new System.Drawing.Size(215, 446);
            this.trvMethods.TabIndex = 15;
            this.trvMethods.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMethods_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "methodHead.gif");
            this.imageList1.Images.SetKeyName(1, "Method.gif");
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabInput);
            this.tabControl.Controls.Add(this.tbOutput);
            this.tabControl.Controls.Add(this.tabPerformance);
            this.tabControl.Location = new System.Drawing.Point(234, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(927, 446);
            this.tabControl.TabIndex = 16;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.txtDetails);
            this.tabInput.Controls.Add(this.txtFile);
            this.tabInput.Controls.Add(this.btnExecute);
            this.tabInput.Controls.Add(this.btnBrowse);
            this.tabInput.Location = new System.Drawing.Point(4, 25);
            this.tabInput.Name = "tabInput";
            this.tabInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabInput.Size = new System.Drawing.Size(919, 417);
            this.tabInput.TabIndex = 0;
            this.tabInput.Text = "Input";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(3, 33);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDetails.Size = new System.Drawing.Size(738, 356);
            this.txtDetails.TabIndex = 13;
            this.txtDetails.WordWrap = false;
            // 
            // tbOutput
            // 
            this.tbOutput.Controls.Add(this.btnClear);
            this.tbOutput.Controls.Add(this.txtResult);
            this.tbOutput.Location = new System.Drawing.Point(4, 25);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tbOutput.Size = new System.Drawing.Size(919, 417);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "Result";
            this.tbOutput.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(666, 391);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtResult.ForeColor = System.Drawing.Color.Navy;
            this.txtResult.Location = new System.Drawing.Point(3, 3);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(913, 385);
            this.txtResult.TabIndex = 0;
            this.txtResult.WordWrap = false;
            // 
            // tabPerformance
            // 
            this.tabPerformance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPerformance.Controls.Add(this.tbPerformance);
            this.tabPerformance.Location = new System.Drawing.Point(4, 25);
            this.tabPerformance.Name = "tabPerformance";
            this.tabPerformance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPerformance.Size = new System.Drawing.Size(919, 417);
            this.tabPerformance.TabIndex = 2;
            this.tabPerformance.Text = "Performance Test";
            this.tabPerformance.UseVisualStyleBackColor = true;
            // 
            // tbPerformance
            // 
            this.tbPerformance.Controls.Add(this.tbDescDevice);
            this.tbPerformance.Controls.Add(this.tbSendAlert);
            this.tbPerformance.Controls.Add(this.tbOther);
            this.tbPerformance.Controls.Add(this.tbRmteCmd);
            this.tbPerformance.Controls.Add(this.tbcompdevicechng);
            this.tbPerformance.Controls.Add(this.tbbulkdescribedevice);
            this.tbPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPerformance.Location = new System.Drawing.Point(3, 3);
            this.tbPerformance.Name = "tbPerformance";
            this.tbPerformance.SelectedIndex = 0;
            this.tbPerformance.Size = new System.Drawing.Size(911, 409);
            this.tbPerformance.TabIndex = 5;
            // 
            // tbDescDevice
            // 
            this.tbDescDevice.BackColor = System.Drawing.Color.Gainsboro;
            this.tbDescDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDescDevice.Controls.Add(this.cmdClearRows_Performance_DescDevice);
            this.tbDescDevice.Controls.Add(this.dgvPerformance_DescDevice);
            this.tbDescDevice.Controls.Add(this.cmdInvokeMethod_performance_DescDevice);
            this.tbDescDevice.Controls.Add(this.lblRowCount_performance_DescDevice);
            this.tbDescDevice.Controls.Add(this.cmdAdd_performance_DescDevice);
            this.tbDescDevice.Controls.Add(this.txtRowCount_performance_DescDevice);
            this.tbDescDevice.Location = new System.Drawing.Point(4, 22);
            this.tbDescDevice.Name = "tbDescDevice";
            this.tbDescDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tbDescDevice.Size = new System.Drawing.Size(903, 383);
            this.tbDescDevice.TabIndex = 0;
            this.tbDescDevice.Text = "Describe Device";
            // 
            // cmdClearRows_Performance_DescDevice
            // 
            this.cmdClearRows_Performance_DescDevice.Location = new System.Drawing.Point(2, 356);
            this.cmdClearRows_Performance_DescDevice.Name = "cmdClearRows_Performance_DescDevice";
            this.cmdClearRows_Performance_DescDevice.Size = new System.Drawing.Size(63, 23);
            this.cmdClearRows_Performance_DescDevice.TabIndex = 5;
            this.cmdClearRows_Performance_DescDevice.Text = "Clear Rows";
            this.cmdClearRows_Performance_DescDevice.UseVisualStyleBackColor = true;
            this.cmdClearRows_Performance_DescDevice.Click += new System.EventHandler(this.cmdClearRows_Performance_DescDevice_Click);
            // 
            // dgvPerformance_DescDevice
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPerformance_DescDevice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvPerformance_DescDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPerformance_DescDevice.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvPerformance_DescDevice.Location = new System.Drawing.Point(2, 25);
            this.dgvPerformance_DescDevice.Name = "dgvPerformance_DescDevice";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPerformance_DescDevice.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvPerformance_DescDevice.RowHeadersVisible = false;
            this.dgvPerformance_DescDevice.Size = new System.Drawing.Size(893, 328);
            this.dgvPerformance_DescDevice.TabIndex = 0;
            // 
            // cmdInvokeMethod_performance_DescDevice
            // 
            this.cmdInvokeMethod_performance_DescDevice.Location = new System.Drawing.Point(639, 359);
            this.cmdInvokeMethod_performance_DescDevice.Name = "cmdInvokeMethod_performance_DescDevice";
            this.cmdInvokeMethod_performance_DescDevice.Size = new System.Drawing.Size(256, 23);
            this.cmdInvokeMethod_performance_DescDevice.TabIndex = 4;
            this.cmdInvokeMethod_performance_DescDevice.Text = "Send Describe Device Package";
            this.cmdInvokeMethod_performance_DescDevice.UseVisualStyleBackColor = true;
            this.cmdInvokeMethod_performance_DescDevice.Click += new System.EventHandler(this.cmdInvokeMethod_performance_DescDevice_Click);
            // 
            // lblRowCount_performance_DescDevice
            // 
            this.lblRowCount_performance_DescDevice.AutoSize = true;
            this.lblRowCount_performance_DescDevice.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowCount_performance_DescDevice.Location = new System.Drawing.Point(592, 4);
            this.lblRowCount_performance_DescDevice.Name = "lblRowCount_performance_DescDevice";
            this.lblRowCount_performance_DescDevice.Size = new System.Drawing.Size(147, 16);
            this.lblRowCount_performance_DescDevice.TabIndex = 1;
            this.lblRowCount_performance_DescDevice.Text = "Row count ( max 0-99 )";
            // 
            // cmdAdd_performance_DescDevice
            // 
            this.cmdAdd_performance_DescDevice.Location = new System.Drawing.Point(851, 0);
            this.cmdAdd_performance_DescDevice.Name = "cmdAdd_performance_DescDevice";
            this.cmdAdd_performance_DescDevice.Size = new System.Drawing.Size(44, 23);
            this.cmdAdd_performance_DescDevice.TabIndex = 3;
            this.cmdAdd_performance_DescDevice.Text = "Add";
            this.cmdAdd_performance_DescDevice.UseVisualStyleBackColor = true;
            this.cmdAdd_performance_DescDevice.Click += new System.EventHandler(this.cmdAdd_performance_DescDevice_Click);
            // 
            // txtRowCount_performance_DescDevice
            // 
            this.txtRowCount_performance_DescDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRowCount_performance_DescDevice.Location = new System.Drawing.Point(745, 2);
            this.txtRowCount_performance_DescDevice.MaxLength = 2;
            this.txtRowCount_performance_DescDevice.Name = "txtRowCount_performance_DescDevice";
            this.txtRowCount_performance_DescDevice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRowCount_performance_DescDevice.Size = new System.Drawing.Size(100, 20);
            this.txtRowCount_performance_DescDevice.TabIndex = 2;
            this.txtRowCount_performance_DescDevice.Text = "0";
            this.txtRowCount_performance_DescDevice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowCount_performance_DescDevice_KeyPress);
            // 
            // tbSendAlert
            // 
            this.tbSendAlert.BackColor = System.Drawing.Color.Gainsboro;
            this.tbSendAlert.Controls.Add(this.cmdClear_Performance_SendAlert);
            this.tbSendAlert.Controls.Add(this.dgv_Performance_SendAlert);
            this.tbSendAlert.Controls.Add(this.cmdSendAlert_Performance);
            this.tbSendAlert.Controls.Add(this.lblRowCount_Performance_SendAlert);
            this.tbSendAlert.Controls.Add(this.cmdRowAdd_Performance_SendAlert);
            this.tbSendAlert.Controls.Add(this.txtRowCount_Performance_SendAlert);
            this.tbSendAlert.Location = new System.Drawing.Point(4, 22);
            this.tbSendAlert.Name = "tbSendAlert";
            this.tbSendAlert.Padding = new System.Windows.Forms.Padding(3);
            this.tbSendAlert.Size = new System.Drawing.Size(903, 383);
            this.tbSendAlert.TabIndex = 1;
            this.tbSendAlert.Text = "Send Alert";
            // 
            // cmdClear_Performance_SendAlert
            // 
            this.cmdClear_Performance_SendAlert.Location = new System.Drawing.Point(0, 357);
            this.cmdClear_Performance_SendAlert.Name = "cmdClear_Performance_SendAlert";
            this.cmdClear_Performance_SendAlert.Size = new System.Drawing.Size(66, 23);
            this.cmdClear_Performance_SendAlert.TabIndex = 10;
            this.cmdClear_Performance_SendAlert.Text = "Clear";
            this.cmdClear_Performance_SendAlert.UseVisualStyleBackColor = true;
            this.cmdClear_Performance_SendAlert.Click += new System.EventHandler(this.cmdClear_Performance_SendAlert_Click);
            // 
            // dgv_Performance_SendAlert
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Performance_SendAlert.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgv_Performance_SendAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Performance_SendAlert.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgv_Performance_SendAlert.Location = new System.Drawing.Point(2, 26);
            this.dgv_Performance_SendAlert.Name = "dgv_Performance_SendAlert";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Performance_SendAlert.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgv_Performance_SendAlert.RowHeadersVisible = false;
            this.dgv_Performance_SendAlert.Size = new System.Drawing.Size(898, 328);
            this.dgv_Performance_SendAlert.TabIndex = 5;
            // 
            // cmdSendAlert_Performance
            // 
            this.cmdSendAlert_Performance.Location = new System.Drawing.Point(644, 357);
            this.cmdSendAlert_Performance.Name = "cmdSendAlert_Performance";
            this.cmdSendAlert_Performance.Size = new System.Drawing.Size(256, 23);
            this.cmdSendAlert_Performance.TabIndex = 9;
            this.cmdSendAlert_Performance.Text = "Send Alert package";
            this.cmdSendAlert_Performance.UseVisualStyleBackColor = true;
            this.cmdSendAlert_Performance.Click += new System.EventHandler(this.cmdSendAlert_Performance_Click);
            // 
            // lblRowCount_Performance_SendAlert
            // 
            this.lblRowCount_Performance_SendAlert.AutoSize = true;
            this.lblRowCount_Performance_SendAlert.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowCount_Performance_SendAlert.Location = new System.Drawing.Point(597, 10);
            this.lblRowCount_Performance_SendAlert.Name = "lblRowCount_Performance_SendAlert";
            this.lblRowCount_Performance_SendAlert.Size = new System.Drawing.Size(147, 16);
            this.lblRowCount_Performance_SendAlert.TabIndex = 6;
            this.lblRowCount_Performance_SendAlert.Text = "Row count ( max 0-99 )";
            // 
            // cmdRowAdd_Performance_SendAlert
            // 
            this.cmdRowAdd_Performance_SendAlert.Location = new System.Drawing.Point(856, 3);
            this.cmdRowAdd_Performance_SendAlert.Name = "cmdRowAdd_Performance_SendAlert";
            this.cmdRowAdd_Performance_SendAlert.Size = new System.Drawing.Size(44, 23);
            this.cmdRowAdd_Performance_SendAlert.TabIndex = 8;
            this.cmdRowAdd_Performance_SendAlert.Text = "Add";
            this.cmdRowAdd_Performance_SendAlert.UseVisualStyleBackColor = true;
            this.cmdRowAdd_Performance_SendAlert.Click += new System.EventHandler(this.cmdRowAdd_Performance_SendAlert_Click);
            // 
            // txtRowCount_Performance_SendAlert
            // 
            this.txtRowCount_Performance_SendAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRowCount_Performance_SendAlert.Location = new System.Drawing.Point(750, 6);
            this.txtRowCount_Performance_SendAlert.MaxLength = 2;
            this.txtRowCount_Performance_SendAlert.Name = "txtRowCount_Performance_SendAlert";
            this.txtRowCount_Performance_SendAlert.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRowCount_Performance_SendAlert.Size = new System.Drawing.Size(100, 20);
            this.txtRowCount_Performance_SendAlert.TabIndex = 7;
            this.txtRowCount_Performance_SendAlert.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowCount_Performance_SendAlert_KeyPress);
            // 
            // tbOther
            // 
            this.tbOther.Controls.Add(this.grpAlertUsingXml);
            this.tbOther.Location = new System.Drawing.Point(4, 22);
            this.tbOther.Name = "tbOther";
            this.tbOther.Padding = new System.Windows.Forms.Padding(3);
            this.tbOther.Size = new System.Drawing.Size(903, 383);
            this.tbOther.TabIndex = 3;
            this.tbOther.Text = "Other";
            this.tbOther.UseVisualStyleBackColor = true;
            // 
            // grpAlertUsingXml
            // 
            this.grpAlertUsingXml.Controls.Add(this.cmdStartThread);
            this.grpAlertUsingXml.Controls.Add(this.label5);
            this.grpAlertUsingXml.Controls.Add(this.label4);
            this.grpAlertUsingXml.Controls.Add(this.txtDuration);
            this.grpAlertUsingXml.Controls.Add(this.label3);
            this.grpAlertUsingXml.Controls.Add(this.txtEvery);
            this.grpAlertUsingXml.Controls.Add(this.label2);
            this.grpAlertUsingXml.Controls.Add(this.label1);
            this.grpAlertUsingXml.Controls.Add(this.txtNumberOfPkgs);
            this.grpAlertUsingXml.Controls.Add(this.cmdSelectedPkg);
            this.grpAlertUsingXml.Location = new System.Drawing.Point(18, 22);
            this.grpAlertUsingXml.Name = "grpAlertUsingXml";
            this.grpAlertUsingXml.Size = new System.Drawing.Size(282, 161);
            this.grpAlertUsingXml.TabIndex = 0;
            this.grpAlertUsingXml.TabStop = false;
            this.grpAlertUsingXml.Text = "Alert Using Xml";
            // 
            // cmdStartThread
            // 
            this.cmdStartThread.Location = new System.Drawing.Point(25, 127);
            this.cmdStartThread.Name = "cmdStartThread";
            this.cmdStartThread.Size = new System.Drawing.Size(130, 23);
            this.cmdStartThread.TabIndex = 9;
            this.cmdStartThread.Text = "Start";
            this.cmdStartThread.UseVisualStyleBackColor = true;
            this.cmdStartThread.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Execution Duration";
            // 
            // txtDuration
            // 
            this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDuration.Location = new System.Drawing.Point(162, 96);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(44, 20);
            this.txtDuration.TabIndex = 6;
            this.txtDuration.Text = "15";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Seconds";
            // 
            // txtEvery
            // 
            this.txtEvery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEvery.Location = new System.Drawing.Point(162, 73);
            this.txtEvery.Name = "txtEvery";
            this.txtEvery.Size = new System.Drawing.Size(44, 20);
            this.txtEvery.TabIndex = 4;
            this.txtEvery.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Every";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Alert Pkgs";
            // 
            // txtNumberOfPkgs
            // 
            this.txtNumberOfPkgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumberOfPkgs.Location = new System.Drawing.Point(162, 50);
            this.txtNumberOfPkgs.Name = "txtNumberOfPkgs";
            this.txtNumberOfPkgs.Size = new System.Drawing.Size(100, 20);
            this.txtNumberOfPkgs.TabIndex = 1;
            this.txtNumberOfPkgs.Text = "1";
            // 
            // cmdSelectedPkg
            // 
            this.cmdSelectedPkg.Location = new System.Drawing.Point(25, 19);
            this.cmdSelectedPkg.Name = "cmdSelectedPkg";
            this.cmdSelectedPkg.Size = new System.Drawing.Size(130, 23);
            this.cmdSelectedPkg.TabIndex = 0;
            this.cmdSelectedPkg.Text = "Select Alert Package";
            this.cmdSelectedPkg.UseVisualStyleBackColor = true;
            this.cmdSelectedPkg.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbRmteCmd
            // 
            this.tbRmteCmd.Controls.Add(this.remoteCmdSplitContainer);
            this.tbRmteCmd.Location = new System.Drawing.Point(4, 22);
            this.tbRmteCmd.Name = "tbRmteCmd";
            this.tbRmteCmd.Padding = new System.Windows.Forms.Padding(3);
            this.tbRmteCmd.Size = new System.Drawing.Size(903, 383);
            this.tbRmteCmd.TabIndex = 4;
            this.tbRmteCmd.Text = "Remote Command";
            this.tbRmteCmd.UseVisualStyleBackColor = true;
            // 
            // remoteCmdSplitContainer
            // 
            this.remoteCmdSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteCmdSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.remoteCmdSplitContainer.Name = "remoteCmdSplitContainer";
            this.remoteCmdSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // remoteCmdSplitContainer.Panel1
            // 
            this.remoteCmdSplitContainer.Panel1.Controls.Add(this.cmdGetQueuedRemoteCommand);
            // 
            // remoteCmdSplitContainer.Panel2
            // 
            this.remoteCmdSplitContainer.Panel2.Controls.Add(this.cmdRemoteCommandExecute);
            this.remoteCmdSplitContainer.Panel2.Controls.Add(this.cmdRemoteCommandClear);
            this.remoteCmdSplitContainer.Panel2.Controls.Add(this.dgvPendingRemoteCommands);
            this.remoteCmdSplitContainer.Size = new System.Drawing.Size(897, 377);
            this.remoteCmdSplitContainer.SplitterDistance = 30;
            this.remoteCmdSplitContainer.TabIndex = 0;
            // 
            // cmdGetQueuedRemoteCommand
            // 
            this.cmdGetQueuedRemoteCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdGetQueuedRemoteCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetQueuedRemoteCommand.ForeColor = System.Drawing.Color.Maroon;
            this.cmdGetQueuedRemoteCommand.Location = new System.Drawing.Point(0, 0);
            this.cmdGetQueuedRemoteCommand.Name = "cmdGetQueuedRemoteCommand";
            this.cmdGetQueuedRemoteCommand.Size = new System.Drawing.Size(897, 30);
            this.cmdGetQueuedRemoteCommand.TabIndex = 0;
            this.cmdGetQueuedRemoteCommand.Text = "Get Queued Remote commands";
            this.cmdGetQueuedRemoteCommand.UseVisualStyleBackColor = true;
            this.cmdGetQueuedRemoteCommand.Click += new System.EventHandler(this.cmdGetQueuedRemoteCommand_Click);
            // 
            // cmdRemoteCommandExecute
            // 
            this.cmdRemoteCommandExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoteCommandExecute.ForeColor = System.Drawing.Color.Maroon;
            this.cmdRemoteCommandExecute.Location = new System.Drawing.Point(739, 317);
            this.cmdRemoteCommandExecute.Name = "cmdRemoteCommandExecute";
            this.cmdRemoteCommandExecute.Size = new System.Drawing.Size(155, 23);
            this.cmdRemoteCommandExecute.TabIndex = 3;
            this.cmdRemoteCommandExecute.Text = "Execute";
            this.cmdRemoteCommandExecute.UseVisualStyleBackColor = true;
            this.cmdRemoteCommandExecute.Click += new System.EventHandler(this.cmdRemoteCommandExecute_Click);
            // 
            // cmdRemoteCommandClear
            // 
            this.cmdRemoteCommandClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemoteCommandClear.ForeColor = System.Drawing.Color.Maroon;
            this.cmdRemoteCommandClear.Location = new System.Drawing.Point(3, 317);
            this.cmdRemoteCommandClear.Name = "cmdRemoteCommandClear";
            this.cmdRemoteCommandClear.Size = new System.Drawing.Size(155, 23);
            this.cmdRemoteCommandClear.TabIndex = 1;
            this.cmdRemoteCommandClear.Text = "Clear";
            this.cmdRemoteCommandClear.UseVisualStyleBackColor = true;
            this.cmdRemoteCommandClear.Click += new System.EventHandler(this.cmdRemoteCommandClear_Click);
            // 
            // dgvPendingRemoteCommands
            // 
            this.dgvPendingRemoteCommands.AllowUserToAddRows = false;
            this.dgvPendingRemoteCommands.AllowUserToDeleteRows = false;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendingRemoteCommands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvPendingRemoteCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPendingRemoteCommands.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvPendingRemoteCommands.Location = new System.Drawing.Point(3, 3);
            this.dgvPendingRemoteCommands.Name = "dgvPendingRemoteCommands";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPendingRemoteCommands.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvPendingRemoteCommands.RowHeadersVisible = false;
            this.dgvPendingRemoteCommands.Size = new System.Drawing.Size(891, 312);
            this.dgvPendingRemoteCommands.TabIndex = 2;
            // 
            // tbcompdevicechng
            // 
            this.tbcompdevicechng.Controls.Add(this.cmdRowAdd_Performance_Component_Click);
            this.tbcompdevicechng.Controls.Add(this.txtRowCount_Performance_Component);
            this.tbcompdevicechng.Controls.Add(this.label6);
            this.tbcompdevicechng.Controls.Add(this.cmdClearDeviceComponentChange_Performance_Click);
            this.tbcompdevicechng.Controls.Add(this.cmdDeviceComponentChange_Performance);
            this.tbcompdevicechng.Controls.Add(this.dgvReportDeviceComponentChange);
            this.tbcompdevicechng.Location = new System.Drawing.Point(4, 22);
            this.tbcompdevicechng.Name = "tbcompdevicechng";
            this.tbcompdevicechng.Padding = new System.Windows.Forms.Padding(3);
            this.tbcompdevicechng.Size = new System.Drawing.Size(903, 383);
            this.tbcompdevicechng.TabIndex = 5;
            this.tbcompdevicechng.Text = "Component Device Change";
            this.tbcompdevicechng.UseVisualStyleBackColor = true;
            // 
            // cmdRowAdd_Performance_Component_Click
            // 
            this.cmdRowAdd_Performance_Component_Click.Location = new System.Drawing.Point(853, 1);
            this.cmdRowAdd_Performance_Component_Click.Name = "cmdRowAdd_Performance_Component_Click";
            this.cmdRowAdd_Performance_Component_Click.Size = new System.Drawing.Size(44, 23);
            this.cmdRowAdd_Performance_Component_Click.TabIndex = 14;
            this.cmdRowAdd_Performance_Component_Click.Text = "Add";
            this.cmdRowAdd_Performance_Component_Click.UseVisualStyleBackColor = true;
            this.cmdRowAdd_Performance_Component_Click.Click += new System.EventHandler(this.cmdRowAdd_Performance_Component_Click_Click);
            // 
            // txtRowCount_Performance_Component
            // 
            this.txtRowCount_Performance_Component.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRowCount_Performance_Component.Location = new System.Drawing.Point(799, 3);
            this.txtRowCount_Performance_Component.MaxLength = 2;
            this.txtRowCount_Performance_Component.Name = "txtRowCount_Performance_Component";
            this.txtRowCount_Performance_Component.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRowCount_Performance_Component.Size = new System.Drawing.Size(48, 20);
            this.txtRowCount_Performance_Component.TabIndex = 13;
            this.txtRowCount_Performance_Component.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRowCount_Performance_Component_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(646, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Row count ( max 0-10 )";
            // 
            // cmdClearDeviceComponentChange_Performance_Click
            // 
            this.cmdClearDeviceComponentChange_Performance_Click.Location = new System.Drawing.Point(6, 357);
            this.cmdClearDeviceComponentChange_Performance_Click.Name = "cmdClearDeviceComponentChange_Performance_Click";
            this.cmdClearDeviceComponentChange_Performance_Click.Size = new System.Drawing.Size(66, 23);
            this.cmdClearDeviceComponentChange_Performance_Click.TabIndex = 11;
            this.cmdClearDeviceComponentChange_Performance_Click.Text = "Clear";
            this.cmdClearDeviceComponentChange_Performance_Click.UseVisualStyleBackColor = true;
            this.cmdClearDeviceComponentChange_Performance_Click.Click += new System.EventHandler(this.cmdClearDeviceComponentChange_Performance_Click_Click);
            // 
            // cmdDeviceComponentChange_Performance
            // 
            this.cmdDeviceComponentChange_Performance.Location = new System.Drawing.Point(641, 354);
            this.cmdDeviceComponentChange_Performance.Name = "cmdDeviceComponentChange_Performance";
            this.cmdDeviceComponentChange_Performance.Size = new System.Drawing.Size(256, 23);
            this.cmdDeviceComponentChange_Performance.TabIndex = 10;
            this.cmdDeviceComponentChange_Performance.Text = "Send Device Component Change package";
            this.cmdDeviceComponentChange_Performance.UseVisualStyleBackColor = true;
            this.cmdDeviceComponentChange_Performance.Click += new System.EventHandler(this.cmdDeviceComponentChange_Performance_Click);
            // 
            // dgvReportDeviceComponentChange
            // 
            this.dgvReportDeviceComponentChange.AllowUserToAddRows = false;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportDeviceComponentChange.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvReportDeviceComponentChange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportDeviceComponentChange.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceGuid,
            this.SerialNumber,
            this.DeviceType,
            this.Manufacturer,
            this.Model,
            this.Description,
            this.UserName,
            this.AddRemove,
            this.StatusDate});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReportDeviceComponentChange.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvReportDeviceComponentChange.Location = new System.Drawing.Point(6, 30);
            this.dgvReportDeviceComponentChange.Name = "dgvReportDeviceComponentChange";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReportDeviceComponentChange.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvReportDeviceComponentChange.RowHeadersVisible = false;
            this.dgvReportDeviceComponentChange.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvReportDeviceComponentChange.Size = new System.Drawing.Size(891, 321);
            this.dgvReportDeviceComponentChange.TabIndex = 6;
            // 
            // DeviceGuid
            // 
            this.DeviceGuid.HeaderText = "DeviceGuid";
            this.DeviceGuid.MaxInputLength = 36;
            this.DeviceGuid.MinimumWidth = 50;
            this.DeviceGuid.Name = "DeviceGuid";
            this.DeviceGuid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceGuid.ToolTipText = "Enter the Device Guid";
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "SerialNumber";
            this.SerialNumber.Name = "SerialNumber";
            // 
            // DeviceType
            // 
            this.DeviceType.HeaderText = "DeviceType";
            this.DeviceType.Name = "DeviceType";
            // 
            // Manufacturer
            // 
            this.Manufacturer.HeaderText = "Manufacturer";
            this.Manufacturer.Name = "Manufacturer";
            // 
            // Model
            // 
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // UserName
            // 
            this.UserName.HeaderText = "UserName";
            this.UserName.Name = "UserName";
            // 
            // AddRemove
            // 
            this.AddRemove.HeaderText = "AddRemove";
            this.AddRemove.Items.AddRange(new object[] {
            "Add",
            "Remove"});
            this.AddRemove.Name = "AddRemove";
            this.AddRemove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AddRemove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AddRemove.ToolTipText = "0=Add,1=Remove";
            // 
            // StatusDate
            // 
            this.StatusDate.HeaderText = "StatusDate";
            this.StatusDate.Name = "StatusDate";
            // 
            // tbbulkdescribedevice
            // 
            this.tbbulkdescribedevice.Controls.Add(this.button1);
            this.tbbulkdescribedevice.Controls.Add(this.txtDevice);
            this.tbbulkdescribedevice.Controls.Add(this.label7);
            this.tbbulkdescribedevice.Location = new System.Drawing.Point(4, 22);
            this.tbbulkdescribedevice.Name = "tbbulkdescribedevice";
            this.tbbulkdescribedevice.Padding = new System.Windows.Forms.Padding(3);
            this.tbbulkdescribedevice.Size = new System.Drawing.Size(903, 383);
            this.tbbulkdescribedevice.TabIndex = 6;
            this.tbbulkdescribedevice.Text = "tbcbulkDescribe";
            this.tbbulkdescribedevice.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Send Package";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtDevice
            // 
            this.txtDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDevice.Location = new System.Drawing.Point(312, 110);
            this.txtDevice.MaxLength = 3;
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDevice.Size = new System.Drawing.Size(48, 20);
            this.txtDevice.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(201, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Device count";
            // 
            // frmTdmSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 494);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.trvMethods);
            this.Name = "frmTdmSimulator";
            this.Text = "TDM Device Simulator";
            this.Load += new System.EventHandler(this.frmTdmSimulator_Load);
            this.tabControl.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            this.tabInput.PerformLayout();
            this.tbOutput.ResumeLayout(false);
            this.tbOutput.PerformLayout();
            this.tabPerformance.ResumeLayout(false);
            this.tbPerformance.ResumeLayout(false);
            this.tbDescDevice.ResumeLayout(false);
            this.tbDescDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPerformance_DescDevice)).EndInit();
            this.tbSendAlert.ResumeLayout(false);
            this.tbSendAlert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Performance_SendAlert)).EndInit();
            this.tbOther.ResumeLayout(false);
            this.grpAlertUsingXml.ResumeLayout(false);
            this.grpAlertUsingXml.PerformLayout();
            this.tbRmteCmd.ResumeLayout(false);
            this.remoteCmdSplitContainer.Panel1.ResumeLayout(false);
            this.remoteCmdSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.remoteCmdSplitContainer)).EndInit();
            this.remoteCmdSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingRemoteCommands)).EndInit();
            this.tbcompdevicechng.ResumeLayout(false);
            this.tbcompdevicechng.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportDeviceComponentChange)).EndInit();
            this.tbbulkdescribedevice.ResumeLayout(false);
            this.tbbulkdescribedevice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.OpenFileDialog ofdXmlFile;
        private System.Windows.Forms.TreeView trvMethods;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.TabPage tbOutput;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TabPage tabPerformance;
        private System.Windows.Forms.TabControl tbPerformance;
        private System.Windows.Forms.TabPage tbDescDevice;
        private System.Windows.Forms.Button cmdClearRows_Performance_DescDevice;
        private System.Windows.Forms.DataGridView dgvPerformance_DescDevice;
        private System.Windows.Forms.Button cmdInvokeMethod_performance_DescDevice;
        private System.Windows.Forms.Label lblRowCount_performance_DescDevice;
        private System.Windows.Forms.Button cmdAdd_performance_DescDevice;
        private System.Windows.Forms.TextBox txtRowCount_performance_DescDevice;
        private System.Windows.Forms.TabPage tbSendAlert;
        private System.Windows.Forms.Button cmdClear_Performance_SendAlert;
        private System.Windows.Forms.DataGridView dgv_Performance_SendAlert;
        private System.Windows.Forms.Button cmdSendAlert_Performance;
        private System.Windows.Forms.Label lblRowCount_Performance_SendAlert;
        private System.Windows.Forms.Button cmdRowAdd_Performance_SendAlert;
        private System.Windows.Forms.TextBox txtRowCount_Performance_SendAlert;
        private System.Windows.Forms.TabPage tbOther;
        private System.Windows.Forms.GroupBox grpAlertUsingXml;
        private System.Windows.Forms.Button cmdStartThread;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEvery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfPkgs;
        private System.Windows.Forms.Button cmdSelectedPkg;
        private System.Windows.Forms.TabPage tbRmteCmd;
        private System.Windows.Forms.SplitContainer remoteCmdSplitContainer;
        private System.Windows.Forms.Button cmdGetQueuedRemoteCommand;
        private System.Windows.Forms.Button cmdRemoteCommandExecute;
        private System.Windows.Forms.Button cmdRemoteCommandClear;
        private System.Windows.Forms.DataGridView dgvPendingRemoteCommands;
        private System.Windows.Forms.TabPage tbcompdevicechng;
        private System.Windows.Forms.Button cmdClearDeviceComponentChange_Performance_Click;
        private System.Windows.Forms.Button cmdDeviceComponentChange_Performance;
        private System.Windows.Forms.DataGridView dgvReportDeviceComponentChange;
        private System.Windows.Forms.TextBox txtRowCount_Performance_Component;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdRowAdd_Performance_Component_Click;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Manufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewComboBoxColumn AddRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDate;
        private System.Windows.Forms.TabPage tbbulkdescribedevice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label label7;
    }
}

