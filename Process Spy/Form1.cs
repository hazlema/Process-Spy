using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Process_Spy {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        List<Processes> Running = new List<Processes>();
        TreeNode Root = new TreeNode("Idle");  // Process #0

        private List<Processes> GetProcesses() {
            Process[] proc = Process.GetProcesses();

            foreach (Process thisProc in proc) {
                if (thisProc.Id != 0)   // Avoids loop
                    Running.Add(new Processes(thisProc.ProcessName, thisProc.Id, thisProc.Parent().Id));
            }

            return Running;
        }

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

        private void Form1_Load(object sender, EventArgs e) {
            Running = GetProcesses();
            tree.Nodes.Add(BuildTree(Root, 0));
            tree.SelectedNode = Root;
            tree.ExpandAll();
            Root.EnsureVisible();
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node != null)
                txtProcessID.Text = $"{e.Node.Text} (pid: {e.Node.Tag.ToString()})";
        }
    }
}
