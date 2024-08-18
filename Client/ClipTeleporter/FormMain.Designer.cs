namespace ClipTeleporter
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSend = new System.Windows.Forms.ToolStripButton();
            this.btnReceive = new System.Windows.Forms.ToolStripButton();
            this.btnCopyToken = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSend,
            this.btnReceive,
            this.btnCopyToken,
            this.btnSettings,
            this.btnAbout});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 27);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // btnSend
            // 
            this.btnSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(46, 24);
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReceive.Enabled = false;
            this.btnReceive.Image = ((System.Drawing.Image)(resources.GetObject("btnReceive.Image")));
            this.btnReceive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(64, 24);
            this.btnReceive.Text = "Receive";
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnCopyToken
            // 
            this.btnCopyToken.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopyToken.Enabled = false;
            this.btnCopyToken.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyToken.Image")));
            this.btnCopyToken.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyToken.Name = "btnCopyToken";
            this.btnCopyToken.Size = new System.Drawing.Size(90, 24);
            this.btnCopyToken.Text = "Copy Token";
            this.btnCopyToken.Click += new System.EventHandler(this.btnCopyToken_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(66, 24);
            this.btnSettings.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(29, 24);
            this.btnAbout.Text = "?";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Direction,
            this.Token,
            this.Description});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 27);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 401);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 65;
            // 
            // Direction
            // 
            this.Direction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Direction.DataPropertyName = "Direction";
            this.Direction.HeaderText = "Direction";
            this.Direction.MinimumWidth = 6;
            this.Direction.Name = "Direction";
            this.Direction.ReadOnly = true;
            this.Direction.Width = 89;
            // 
            // Token
            // 
            this.Token.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Token.DataPropertyName = "Token";
            this.Token.HeaderText = "Token";
            this.Token.MinimumWidth = 6;
            this.Token.Name = "Token";
            this.Token.ReadOnly = true;
            this.Token.Width = 75;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "FormMain";
            this.Text = "ClipTeleporter";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.ToolStripButton btnReceive;
        private System.Windows.Forms.ToolStripButton btnCopyToken;
        private System.Windows.Forms.ToolStripButton btnSend;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

