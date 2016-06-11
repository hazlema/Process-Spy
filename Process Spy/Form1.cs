using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Process_Spy {
    public partial class Form1 : Form {

        PidList Running = new PidList();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            TreeRefresh(true); // Full refresh
        }
        private void UpdatePids_Tick(object sender, EventArgs e) {
            TreeRefresh(false);  // Updates only
        }

        // Refresh the tree
        //
        private void TreeRefresh(bool FullRefresh) {
            int KillSwitch = 0;
            PidList Updates = new PidList();

            Updates.GetSystemPids(true);             // Load system processes
            Updates.Remove("idle", PidFields.Name);  // Remove the Idles
            Running.Remove("idle", PidFields.Name);

            if (FullRefresh) {                       // Clear the running processes
                Running.Clear();                     // so the Diff will think   
                PidTree.Nodes.Clear();                  // everything's new  

                TreeNode Root = new TreeNode("Idle");  // Add Idle process to the tree
                Root.Name = "0";
                PidTree.Nodes.Add(Root);
            }

            PidDiff Result = Running.Diff(Updates);     // Execute a Diff

            PruneBranches(Result.Removed);              // Kill Removed

            // Add nodes loop
            //
            while (Result.Added.Count != 0 && KillSwitch < 9) {
                Result.Added = AddBranches(Result.Added);
                KillSwitch++;
            }

            if (FullRefresh) {                       // Collapse the Tree
                PidTree.CollapseAll();
                
                TreeNode[] t = PidTree.Nodes.Find("0", true);  // Expand Idle
                t[0].Expand();
                PidTree.LastNode = t[0];
                txtProcessID.Text = $"Idle (0)";

                // Expand Explorer
                //
                Pid tmpPid = Running.Find("explorer", PidFields.Name);  // Search for explorer
                if (tmpPid != null) {
                    t = PidTree.Nodes.Find(tmpPid.Id.ToString(), true); // Highlight explorer
                    t[0].Expand();

                    txtProcessID.Text = $"{tmpPid.Name} ({tmpPid.Id})";
                    PidTree.SelectedNode = t[0];
                    PidTree.LastNode = t[0];
                    t[0].EnsureVisible();
                }
            }

            txtProcessCount.Text = $"Processes: {Running.Count}";
        }

        // Adds "some" of the nodes to the Tree
        // May need to be run more then once to process them all.  This is because
        // children can have lower pid's then the parents.  (Don't ask me how or why)
        //
        private PidList AddBranches(PidList Updates) {
            PidList Retry = new PidList();      // In some weird instances children 
                                                // can have a lower pid then the parent
                                                // So we have to do retry's

            Updates.Sort(PidFields.Parent);

            foreach (Pid p in Updates) {
                TreeNode tmp = new TreeNode(p.Name);  // Create the node
                tmp.Name = p.Id.ToString();           // Set it's key

                // Look for its parent process in the tree
                //
                TreeNode[] t = PidTree.Nodes.Find(p.Parent.ToString(), true);

                if (t.Length == 0) { // Not found
                    Retry.Add(new Pid(p.Name, p.Id, p.Parent));
                } else { // Found
                    t[0].Nodes.Add(tmp);
                    t[0].Expand();

                    // Add as a running process
                    //
                    Running.Add(new Pid(p.Name, p.Id, p.Parent));
                }
            }

            return Retry;
        }

        // Remove nodes from the tree
        // TODO: Re-parent orphans
        //
        private void PruneBranches(PidList Updates) {
            foreach (Pid p in Updates) {
                TreeNode[] t = PidTree.Nodes.Find(p.Id.ToString(), true);

                if (t.Length != 0) {
                    Running.Remove(p.Id, PidFields.Id);
                    t[0].Remove();
                }
            }
        }

        // UI: Click on TreeNode
        //
        private void NodeClick(object sender, TreeNodeMouseClickEventArgs e) {
            txtProcessID.Text = $"{e.Node.Text} ({e.Node.Name})";
        }

        // UI: Selected something from the context menu
        //
        private new void ContextMenu(object sender, EventArgs e) {
            string cmd = ((ToolStripMenuItem)sender).Text;

            switch (cmd) {
                case "Kill Process":
                    int pid = Convert.ToInt32(PidTree.LastNode.Name);
                    try { Process.GetProcessById(pid).Kill(); } catch { }
                    break;

                case "Kill Process Tree":
                    foreach (Pid p in PidTree.GeneratePidList(Running))
                        try { Process.GetProcessById(p.Id).Kill(); } catch { }
                    break;
            }

            TreeRefresh(false);
        }
    }
}
