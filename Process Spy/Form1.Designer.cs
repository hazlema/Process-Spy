namespace Process_Spy {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxKill = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxKillTree = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdatePids = new System.Windows.Forms.Timer(this.components);
            this.PidTree = new Process_Spy.cTreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtProcessID = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtProcessCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxKill,
            this.ctxKillTree});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 48);
            // 
            // ctxKill
            // 
            this.ctxKill.Name = "ctxKill";
            this.ctxKill.Size = new System.Drawing.Size(159, 22);
            this.ctxKill.Text = "Kill Process";
            this.ctxKill.Click += new System.EventHandler(this.ContextMenu);
            // 
            // ctxKillTree
            // 
            this.ctxKillTree.Name = "ctxKillTree";
            this.ctxKillTree.Size = new System.Drawing.Size(159, 22);
            this.ctxKillTree.Text = "Kill Process Tree";
            this.ctxKillTree.Click += new System.EventHandler(this.ContextMenu);
            // 
            // UpdatePids
            // 
            this.UpdatePids.Enabled = true;
            this.UpdatePids.Interval = 5000;
            this.UpdatePids.Tick += new System.EventHandler(this.UpdatePids_Tick);
            // 
            // PidTree
            // 
            this.PidTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PidTree.ContextMenuStrip = this.contextMenuStrip1;
            this.PidTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PidTree.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PidTree.ForeColor = System.Drawing.Color.White;
            this.PidTree.LastNode = null;
            this.PidTree.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.PidTree.Location = new System.Drawing.Point(0, 0);
            this.PidTree.Name = "PidTree";
            this.PidTree.Size = new System.Drawing.Size(314, 389);
            this.PidTree.TabIndex = 3;
            this.PidTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtProcessID,
            this.txtProcessCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(314, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtProcessID
            // 
            this.txtProcessID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcessID.Name = "txtProcessID";
            this.txtProcessID.Size = new System.Drawing.Size(66, 17);
            this.txtProcessID.Text = "Idle (pid: 0)";
            // 
            // txtProcessCount
            // 
            this.txtProcessCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProcessCount.Name = "txtProcessCount";
            this.txtProcessCount.Size = new System.Drawing.Size(231, 17);
            this.txtProcessCount.Spring = true;
            this.txtProcessCount.Text = "Loading";
            this.txtProcessCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 411);
            this.Controls.Add(this.PidTree);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Process Spy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxKill;
        private System.Windows.Forms.ToolStripMenuItem ctxKillTree;
        private System.Windows.Forms.Timer UpdatePids;
        private cTreeView PidTree;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtProcessID;
        private System.Windows.Forms.ToolStripStatusLabel txtProcessCount;
    }
}

