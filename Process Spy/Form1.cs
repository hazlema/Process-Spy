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
        List<String> tmp = new List<string>();
        TreeNode Root;
        TreeNode LastNode;

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
                    NewBranch.Name = p.ID.ToString();

                    branch.Nodes.Add(NewBranch);
                    BuildTree(NewBranch, p.ID);
                }
            }

            return branch;
        }

        private string FindPid(string app) {
            foreach (Processes p in Running) {
                if (p.Name == app)
                    return p.ID.ToString();
            }

            return "0";
        }

        // Refresh the tree
        private void TreeRefresh() {
            Running = GetProcesses();            // Build Processes

            tree.Nodes.Clear();                  // Clear Tree
            Root = new TreeNode("Idle");         // Process #0
            Root.Tag = 0;                        // Process Number
            Root.Name = "0";                     // Key
            tree.Nodes.Add(BuildTree(Root, 0));  // Add the rest

            // Focus explorer if you can
            tree.SelectedNode = tree.Nodes.Find(FindPid("explorer"), true)[0];
            tree.SelectedNode.Expand();
            tree.SelectedNode.EnsureVisible();

            txtProcessCount.Text = "Processes: " + Running.Count;
        }

        // Recursively crawl a node
        private void treeCrawl(TreeNode n) {
            tmp.Add(n.Tag.ToString());

            foreach (TreeNode thisNode in n.Nodes) {
                treeCrawl(thisNode);
            }
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node != null) {
                txtProcessID.Text = $"{e.Node.Text} (pid: {e.Node.Tag.ToString()})";
                LastNode = e.Node;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            TreeRefresh();
        }

        private void Form1_Load(object sender, EventArgs e) {
            TreeRefresh();
        }

        private void ctxKill_Click(object sender, EventArgs e) {
            Debug.WriteLine($"Kill: {LastNode.Text} ({LastNode.Tag})");

            if (Sys.kill(LastNode.Tag.ToString())) {
                LastNode.Remove();
            } else {
                MessageBox.Show(this, $"Error occurred killing {LastNode.Text}", "Process Spy");
                TreeRefresh();
            }
        }


        private void ctxKillTree_Click(object sender, EventArgs e) {
            bool result = true;

            tmp.Clear();
            treeCrawl(LastNode);

            foreach (string pid in tmp) {
                if (!Sys.kill(pid)) {
                    result = false;
                } else {
                    TreeNode[] all = Root.Nodes.Find(pid, true);
                    foreach (TreeNode n in all) {
                        n.Remove();
                    }
                }
            }

            if (!result) {
                MessageBox.Show(this, "Error occurred killing one or more processes", "Process Spy");
                TreeRefresh();
            }
        }
    }
}
