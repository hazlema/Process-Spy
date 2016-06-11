using System;
using System.Windows.Forms;

namespace Process_Spy {
    public class cTreeView : TreeView {
        public  TreeNode LastNode {get; set;}
        PidList Running;
        PidList ReturnPids;

        public cTreeView() {
            base.NodeMouseClick += CTreeView_NodeMouseClick;
        }

        private void CTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            this.LastNode = e.Node;
        }

        private void CreatePidList(TreeNode n) {
            ReturnPids.Add(Running.Find(n.Name, PidFields.Id));

            foreach (TreeNode Search in n.Nodes)
                CreatePidList(Search);
        }

        public PidList GeneratePidList(PidList Running) {
            this.Running    = Running;
            this.ReturnPids = new PidList();

            CreatePidList(this.LastNode);

            return this.ReturnPids;
        }
    }
}