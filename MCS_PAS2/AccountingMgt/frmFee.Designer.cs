namespace AccountingMgt
{
    partial class frmFee
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsFees = new System.Windows.Forms.ToolStrip();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.scFee = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFeeShortName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFeeType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFeeCode = new System.Windows.Forms.TextBox();
            this.txtFeeName = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvFee = new System.Windows.Forms.DataGridView();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvOther = new System.Windows.Forms.DataGridView();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvNS = new System.Windows.Forms.DataGridView();
            this.statusStrip4 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslItem = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsFees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFee)).BeginInit();
            this.scFee.Panel1.SuspendLayout();
            this.scFee.Panel2.SuspendLayout();
            this.scFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFee)).BeginInit();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOther)).BeginInit();
            this.statusStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNS)).BeginInit();
            this.statusStrip4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsFees
            // 
            this.tsFees.BackColor = System.Drawing.Color.DarkKhaki;
            this.tsFees.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFees.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExit,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbUpdate,
            this.toolStripSeparator2,
            this.tsbDelete,
            this.tsbCancel});
            this.tsFees.Location = new System.Drawing.Point(0, 0);
            this.tsFees.Name = "tsFees";
            this.tsFees.Size = new System.Drawing.Size(989, 70);
            this.tsFees.TabIndex = 0;
            this.tsFees.Text = "toolStrip1";
            // 
            // tsbExit
            // 
            this.tsbExit.ForeColor = System.Drawing.Color.White;
            this.tsbExit.Image = global::AccountingMgt.Properties.Resources.close;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(52, 67);
            this.tsbExit.Text = "&Close";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSave.ForeColor = System.Drawing.Color.White;
            this.tsbSave.Image = global::AccountingMgt.Properties.Resources.save;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(52, 67);
            this.tsbSave.Text = "&Save";
            this.tsbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 70);
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.ForeColor = System.Drawing.Color.White;
            this.tsbUpdate.Image = global::AccountingMgt.Properties.Resources.update;
            this.tsbUpdate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(52, 67);
            this.tsbUpdate.Text = "&Update";
            this.tsbUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 70);
            // 
            // tsbDelete
            // 
            this.tsbDelete.ForeColor = System.Drawing.Color.White;
            this.tsbDelete.Image = global::AccountingMgt.Properties.Resources.delete;
            this.tsbDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(52, 67);
            this.tsbDelete.Text = "&Delete";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCancel.ForeColor = System.Drawing.Color.White;
            this.tsbCancel.Image = global::AccountingMgt.Properties.Resources.cancel2;
            this.tsbCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(52, 67);
            this.tsbCancel.Text = "Ca&ncel";
            this.tsbCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // scFee
            // 
            this.scFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFee.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scFee.IsSplitterFixed = true;
            this.scFee.Location = new System.Drawing.Point(0, 70);
            this.scFee.Margin = new System.Windows.Forms.Padding(4);
            this.scFee.Name = "scFee";
            this.scFee.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFee.Panel1
            // 
            this.scFee.Panel1.Controls.Add(this.label4);
            this.scFee.Panel1.Controls.Add(this.txtFeeShortName);
            this.scFee.Panel1.Controls.Add(this.label3);
            this.scFee.Panel1.Controls.Add(this.cmbFeeType);
            this.scFee.Panel1.Controls.Add(this.label2);
            this.scFee.Panel1.Controls.Add(this.label1);
            this.scFee.Panel1.Controls.Add(this.txtFeeCode);
            this.scFee.Panel1.Controls.Add(this.txtFeeName);
            // 
            // scFee.Panel2
            // 
            this.scFee.Panel2.Controls.Add(this.splitContainer1);
            this.scFee.Panel2.Controls.Add(this.statusStrip1);
            this.scFee.Size = new System.Drawing.Size(989, 553);
            this.scFee.SplitterDistance = 60;
            this.scFee.SplitterWidth = 5;
            this.scFee.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(533, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Short Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFeeShortName
            // 
            this.txtFeeShortName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeeShortName.Location = new System.Drawing.Point(533, 35);
            this.txtFeeShortName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFeeShortName.MaxLength = 50;
            this.txtFeeShortName.Name = "txtFeeShortName";
            this.txtFeeShortName.Size = new System.Drawing.Size(195, 25);
            this.txtFeeShortName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(733, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Fee Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbFeeType
            // 
            this.cmbFeeType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFeeType.FormattingEnabled = true;
            this.cmbFeeType.Location = new System.Drawing.Point(733, 35);
            this.cmbFeeType.Name = "cmbFeeType";
            this.cmbFeeType.Size = new System.Drawing.Size(175, 25);
            this.cmbFeeType.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Code";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(118, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFeeCode
            // 
            this.txtFeeCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeeCode.Location = new System.Drawing.Point(11, 35);
            this.txtFeeCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtFeeCode.MaxLength = 10;
            this.txtFeeCode.Name = "txtFeeCode";
            this.txtFeeCode.Size = new System.Drawing.Size(100, 25);
            this.txtFeeCode.TabIndex = 7;
            // 
            // txtFeeName
            // 
            this.txtFeeName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeeName.Location = new System.Drawing.Point(118, 35);
            this.txtFeeName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFeeName.MaxLength = 100;
            this.txtFeeName.Name = "txtFeeName";
            this.txtFeeName.Size = new System.Drawing.Size(408, 25);
            this.txtFeeName.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvNS);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip4);
            this.splitContainer1.Size = new System.Drawing.Size(989, 466);
            this.splitContainer1.SplitterDistance = 494;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvFee);
            this.splitContainer2.Panel1.Controls.Add(this.statusStrip2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvOther);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip3);
            this.splitContainer2.Size = new System.Drawing.Size(494, 466);
            this.splitContainer2.SplitterDistance = 264;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvFee
            // 
            this.dgvFee.AllowUserToAddRows = false;
            this.dgvFee.AllowUserToDeleteRows = false;
            this.dgvFee.AllowUserToResizeColumns = false;
            this.dgvFee.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvFee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFee.BackgroundColor = System.Drawing.Color.White;
            this.dgvFee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFee.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFee.Location = new System.Drawing.Point(0, 22);
            this.dgvFee.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFee.MultiSelect = false;
            this.dgvFee.Name = "dgvFee";
            this.dgvFee.ReadOnly = true;
            this.dgvFee.RowHeadersVisible = false;
            this.dgvFee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFee.Size = new System.Drawing.Size(494, 242);
            this.dgvFee.TabIndex = 0;
            this.dgvFee.Leave += new System.EventHandler(this.dgvFee_Leave);
            // 
            // statusStrip2
            // 
            this.statusStrip2.BackColor = System.Drawing.Color.DarkKhaki;
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip2.Location = new System.Drawing.Point(0, 0);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip2.Size = new System.Drawing.Size(494, 22);
            this.statusStrip2.TabIndex = 6;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(230, 17);
            this.toolStripStatusLabel1.Text = "TUITION AND STANDARD SCHOOL FEES";
            // 
            // dgvOther
            // 
            this.dgvOther.AllowUserToAddRows = false;
            this.dgvOther.AllowUserToDeleteRows = false;
            this.dgvOther.AllowUserToResizeColumns = false;
            this.dgvOther.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOther.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOther.BackgroundColor = System.Drawing.Color.White;
            this.dgvOther.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOther.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvOther.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOther.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOther.Location = new System.Drawing.Point(0, 22);
            this.dgvOther.Margin = new System.Windows.Forms.Padding(4);
            this.dgvOther.MultiSelect = false;
            this.dgvOther.Name = "dgvOther";
            this.dgvOther.ReadOnly = true;
            this.dgvOther.RowHeadersVisible = false;
            this.dgvOther.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOther.Size = new System.Drawing.Size(494, 176);
            this.dgvOther.TabIndex = 8;
            this.dgvOther.Leave += new System.EventHandler(this.dgvOther_Leave);
            // 
            // statusStrip3
            // 
            this.statusStrip3.BackColor = System.Drawing.Color.DarkKhaki;
            this.statusStrip3.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip3.Location = new System.Drawing.Point(0, 0);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip3.Size = new System.Drawing.Size(494, 22);
            this.statusStrip3.TabIndex = 7;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel2.Text = "OTHER FEES";
            // 
            // dgvNS
            // 
            this.dgvNS.AllowUserToAddRows = false;
            this.dgvNS.AllowUserToDeleteRows = false;
            this.dgvNS.AllowUserToResizeColumns = false;
            this.dgvNS.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dgvNS.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvNS.BackgroundColor = System.Drawing.Color.White;
            this.dgvNS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNS.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvNS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.DarkGoldenrod;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNS.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvNS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNS.Location = new System.Drawing.Point(0, 22);
            this.dgvNS.Margin = new System.Windows.Forms.Padding(4);
            this.dgvNS.MultiSelect = false;
            this.dgvNS.Name = "dgvNS";
            this.dgvNS.ReadOnly = true;
            this.dgvNS.RowHeadersVisible = false;
            this.dgvNS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNS.Size = new System.Drawing.Size(491, 444);
            this.dgvNS.TabIndex = 9;
            this.dgvNS.Leave += new System.EventHandler(this.dgvNS_Leave);
            // 
            // statusStrip4
            // 
            this.statusStrip4.BackColor = System.Drawing.Color.DarkKhaki;
            this.statusStrip4.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3});
            this.statusStrip4.Location = new System.Drawing.Point(0, 0);
            this.statusStrip4.Name = "statusStrip4";
            this.statusStrip4.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip4.Size = new System.Drawing.Size(491, 22);
            this.statusStrip4.TabIndex = 8;
            this.statusStrip4.Text = "statusStrip4";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(181, 17);
            this.toolStripStatusLabel3.Text = "NON-STANDARD SCHOOL FEES";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslItem});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(989, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslItem
            // 
            this.tsslItem.Font = new System.Drawing.Font("Segoe UI", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslItem.ForeColor = System.Drawing.Color.Black;
            this.tsslItem.Name = "tsslItem";
            this.tsslItem.Size = new System.Drawing.Size(26, 17);
            this.tsslItem.Text = "items";
            // 
            // frmFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(989, 623);
            this.Controls.Add(this.scFee);
            this.Controls.Add(this.tsFees);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List of Fee";
            this.Activated += new System.EventHandler(this.frmFee_Activated);
            this.Load += new System.EventHandler(this.frmFees_Load);
            this.tsFees.ResumeLayout(false);
            this.tsFees.PerformLayout();
            this.scFee.Panel1.ResumeLayout(false);
            this.scFee.Panel1.PerformLayout();
            this.scFee.Panel2.ResumeLayout(false);
            this.scFee.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFee)).EndInit();
            this.scFee.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFee)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOther)).EndInit();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNS)).EndInit();
            this.statusStrip4.ResumeLayout(false);
            this.statusStrip4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsFees;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer scFee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFeeName;
        private System.Windows.Forms.DataGridView dgvFee;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFeeCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFeeType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dgvOther;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridView dgvNS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFeeShortName;
    }
}