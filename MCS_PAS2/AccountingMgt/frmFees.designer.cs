namespace AccountingMgt
{
    partial class frmFees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFees));
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.scFee = new System.Windows.Forms.SplitContainer();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbFeeName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGradeLevel = new System.Windows.Forms.ComboBox();
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
            this.tsslTotalOF = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTuition = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTotal = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.tsbRemove,
            this.tsbClear});
            this.tsFees.Location = new System.Drawing.Point(0, 0);
            this.tsFees.Name = "tsFees";
            this.tsFees.Size = new System.Drawing.Size(872, 70);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 70);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 70);
            // 
            // tsbRemove
            // 
            this.tsbRemove.ForeColor = System.Drawing.Color.White;
            this.tsbRemove.Image = global::AccountingMgt.Properties.Resources.delete;
            this.tsbRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(54, 67);
            this.tsbRemove.Text = "&Remove";
            this.tsbRemove.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClear.ForeColor = System.Drawing.Color.White;
            this.tsbClear.Image = global::AccountingMgt.Properties.Resources.clear_icon;
            this.tsbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(52, 67);
            this.tsbClear.Text = "C&lear";
            this.tsbClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // scFee
            // 
            this.scFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFee.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scFee.Location = new System.Drawing.Point(0, 70);
            this.scFee.Margin = new System.Windows.Forms.Padding(4);
            this.scFee.Name = "scFee";
            this.scFee.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFee.Panel1
            // 
            this.scFee.Panel1.Controls.Add(this.btnAdd);
            this.scFee.Panel1.Controls.Add(this.label3);
            this.scFee.Panel1.Controls.Add(this.txtAmount);
            this.scFee.Panel1.Controls.Add(this.cmbFeeName);
            this.scFee.Panel1.Controls.Add(this.label1);
            this.scFee.Panel1.Controls.Add(this.label8);
            this.scFee.Panel1.Controls.Add(this.cmbGradeLevel);
            // 
            // scFee.Panel2
            // 
            this.scFee.Panel2.Controls.Add(this.splitContainer1);
            this.scFee.Panel2.Controls.Add(this.statusStrip1);
            this.scFee.Size = new System.Drawing.Size(872, 554);
            this.scFee.SplitterDistance = 59;
            this.scFee.SplitterWidth = 5;
            this.scFee.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(691, 31);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(570, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Amount";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(570, 31);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.MaxLength = 8;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 25);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbFeeName
            // 
            this.cmbFeeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFeeName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFeeName.FormattingEnabled = true;
            this.cmbFeeName.Location = new System.Drawing.Point(227, 31);
            this.cmbFeeName.Name = "cmbFeeName";
            this.cmbFeeName.Size = new System.Drawing.Size(337, 25);
            this.cmbFeeName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grade/Year Level";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(227, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(337, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Fee Name";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbGradeLevel
            // 
            this.cmbGradeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGradeLevel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGradeLevel.FormattingEnabled = true;
            this.cmbGradeLevel.Location = new System.Drawing.Point(10, 31);
            this.cmbGradeLevel.Name = "cmbGradeLevel";
            this.cmbGradeLevel.Size = new System.Drawing.Size(211, 25);
            this.cmbGradeLevel.TabIndex = 5;
            this.cmbGradeLevel.SelectionChangeCommitted += new System.EventHandler(this.cmbGradeLevel_SelectionChangeCommitted);
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
            this.splitContainer1.Size = new System.Drawing.Size(872, 460);
            this.splitContainer1.SplitterDistance = 434;
            this.splitContainer1.TabIndex = 5;
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
            this.splitContainer2.Size = new System.Drawing.Size(434, 460);
            this.splitContainer2.SplitterDistance = 224;
            this.splitContainer2.TabIndex = 1;
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
            this.dgvFee.Size = new System.Drawing.Size(434, 202);
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
            this.statusStrip2.Size = new System.Drawing.Size(434, 22);
            this.statusStrip2.TabIndex = 5;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.DarkKhaki;
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
            this.dgvOther.Size = new System.Drawing.Size(434, 210);
            this.dgvOther.TabIndex = 7;
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
            this.statusStrip3.Size = new System.Drawing.Size(434, 22);
            this.statusStrip3.TabIndex = 6;
            this.statusStrip3.Text = "statusStrip3";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.DarkKhaki;
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
            this.dgvNS.Size = new System.Drawing.Size(434, 438);
            this.dgvNS.TabIndex = 7;
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
            this.statusStrip4.Size = new System.Drawing.Size(434, 22);
            this.statusStrip4.TabIndex = 6;
            this.statusStrip4.Text = "statusStrip4";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.DarkKhaki;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(181, 17);
            this.toolStripStatusLabel3.Text = "NON-STANDARD SCHOOL FEES";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslItem,
            this.tsslTotalOF,
            this.tsslTuition,
            this.tsslTotal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(872, 30);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslItem
            // 
            this.tsslItem.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslItem.Font = new System.Drawing.Font("Segoe UI", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslItem.ForeColor = System.Drawing.Color.Black;
            this.tsslItem.Name = "tsslItem";
            this.tsslItem.Size = new System.Drawing.Size(30, 25);
            this.tsslItem.Text = "items";
            // 
            // tsslTotalOF
            // 
            this.tsslTotalOF.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslTotalOF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTotalOF.ForeColor = System.Drawing.Color.Black;
            this.tsslTotalOF.Name = "tsslTotalOF";
            this.tsslTotalOF.Size = new System.Drawing.Size(70, 25);
            this.tsslTotalOF.Text = "Total OF";
            // 
            // tsslTuition
            // 
            this.tsslTuition.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslTuition.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTuition.ForeColor = System.Drawing.Color.Black;
            this.tsslTuition.Name = "tsslTuition";
            this.tsslTuition.Size = new System.Drawing.Size(62, 25);
            this.tsslTuition.Text = "Tuition";
            // 
            // tsslTotal
            // 
            this.tsslTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslTotal.ForeColor = System.Drawing.Color.Red;
            this.tsslTotal.Name = "tsslTotal";
            this.tsslTotal.Size = new System.Drawing.Size(42, 25);
            this.tsslTotal.Text = "Total";
            // 
            // frmFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(872, 624);
            this.Controls.Add(this.scFee);
            this.Controls.Add(this.tsFees);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grade Level Fees";
            this.Activated += new System.EventHandler(this.frmFees_Activated);
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
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbRemove;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer scFee;
        private System.Windows.Forms.ComboBox cmbGradeLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DataGridView dgvFee;
        private System.Windows.Forms.ComboBox cmbFeeName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslItem;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotalOF;
        private System.Windows.Forms.ToolStripStatusLabel tsslTuition;
        private System.Windows.Forms.ToolStripStatusLabel tsslTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridView dgvOther;
        private System.Windows.Forms.DataGridView dgvNS;
    }
}