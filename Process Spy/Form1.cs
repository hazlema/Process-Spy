using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Process_Spy {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        List<Processes> Running = new List<Processes>();  // We only want a snapshot
        TreeNode Root;

        // Build process snapshot
        private List<Processes> GetProcesses() {
            Process[] proc = Process.GetProcesses();
            Running.Clear();

            foreach (Process thisProc in proc) {
                if (thisProc.Id != 0)   // Avoids loop
                    Running.Add(new Processes(thisProc.ProcessName, thisProc.Id, thisProc.Parent().Id));
            }

            return Running;
        }

        // Build a branch
        private TreeNode BuildTree(TreeNode branch, int pid) {
            foreach (Processes p in Running) {
                if (p.Parent == pid) {
                    TreeNode NewBranch = new TreeNode(p.Name);
                    NewBranch.Tag = p.ID;

                    branch.Nodes.Add(NewBranch);
                    BuildTree(NewBranch, p.ID);
                }
            }

            return branch;
        }

        // Refresh the tree
        private void TreeRefresh() {
            Running = GetProcesses();            // Build Processes

            tree.Nodes.Clear();                  // Clear Tree
            Root = new TreeNode("Idle");         // Process #0
            Root.Tag = 0;                        // Process Number
            tree.Nodes.Add(BuildTree(Root, 0));  // Add the rest

            tree.SelectedNode = Root;
            tree.ExpandAll();
            txtProcessCount.Text = "Processes: " + (Running.Count/3) + 1;
            Root.EnsureVisible();
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node != null)
                txtProcessID.Text = $"{e.Node.Text} (pid: {e.Node.Tag.ToString()})";
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            TreeRefresh();
        }

        private void Form1_Load(object sender, EventArgs e) {
            TreeRefresh();
        }
    }
}
