/*===============================================
 * public class Pid 
 * One Instance of Pid
 * 
 * public class PidList : IEnumerable
 * List of Pids
 * 
 * public enum PidFields
 * Used for search types
 * 
 * public class PidDiff
 * Return value for the diff function
 * 
 * public class PidEnum : IEnumerator
 * Enumerator for PidList
 *===============================================*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Process_Spy {

    public class Pid {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Parent { get; set; }

        public Pid() { }
        public Pid(string Name, int Id, int Parent) {
            this.Name = Name;
            this.Id = Id;
            this.Parent = Parent;
        }
    }

    public enum PidFields {
        Name,
        Id,
        Parent
    }

    public class PidDiff {
        public PidList Added { get; set; } = new PidList();
        public PidList Removed { get; set; } = new PidList();
        public PidList NoChange { get; set; } = new PidList();

        public PidDiff() { }
        public PidDiff(PidList Added, PidList Removed, PidList NoChange) {
            this.Added = Added;
            this.Removed = Removed;
            this.NoChange = NoChange;
        }
    }

    public class PidList : IEnumerable {
        private List<Pid> _Pids = new List<Pid>();

        public PidList() { }

        // Implementation for the GetEnumerator method.
        //
        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)GetEnumerator();
        }
        public PidEnum GetEnumerator() {
            return new PidEnum(_Pids);
        }

        // Properties
        //
        public List<Pid> Pids { get { return _Pids; } }
        public int Count { get { return _Pids.Count; } }

        // Functions
        //
        public void Clear() { _Pids.Clear(); }
        public void Add(Pid t) { _Pids.Add(t); }
        public void Reverse() { _Pids.Reverse(); }

        public Pid Find(int Search, PidFields SearchType) {
            return Find(Search.ToString(), SearchType);
        }
        public Pid Find(string Search, PidFields SearchType) {
            foreach (Pid pid in _Pids) {
                switch (SearchType) {
                    case PidFields.Name:
                        if (pid.Name.ToUpper() == Search.ToUpper())
                            return pid;
                        break;
                    case PidFields.Id:
                        if (pid.Id.ToString() == Search)
                            return pid;
                        break;
                    case PidFields.Parent:
                        if (pid.Parent.ToString() == Search)
                            return pid;
                        break;
                }
            }

            return null;
        }

        public void Remove(Pid t) { _Pids.Remove(t); }
        public void Remove(int Search, PidFields SearchType) {
            Pid tmp = Find(Search, SearchType);
            if (tmp != null) _Pids.Remove(tmp);
        }
        public void Remove(string Search, PidFields SearchType) {
            Pid tmp = Find(Search, SearchType);
            if (tmp != null) _Pids.Remove(tmp);
        }

        public void Sort(PidFields SearchType) {
            switch (SearchType) {
                case PidFields.Name:
                    _Pids.Sort((x, y) => x.Name.CompareTo(y.Name));
                    break;
                case PidFields.Id:
                    _Pids.Sort((x, y) => x.Id.CompareTo(y.Id));
                    break;
                case PidFields.Parent:
                    _Pids.Sort((x, y) => x.Parent.CompareTo(y.Parent));
                    break;
            }
        }

        public PidDiff Diff(PidList CompareTo) {  
        PidList Added = new PidList();
            PidList Removed = new PidList();
            PidList NoChange = new PidList();

            foreach (Pid comparePid in CompareTo) {
                bool ExistSource = (this.Find(comparePid.Id, PidFields.Id) != null) ? true : false;
                bool ExistCompare = (CompareTo.Find(comparePid.Id, PidFields.Id) != null) ? true : false;

                if (!ExistSource && ExistCompare)
                    Added.Add(comparePid);

                if (ExistSource && ExistCompare)
                    NoChange.Add(comparePid);
            }

            foreach (Pid comparePid in _Pids) {
                bool ExistSource = (this.Find(comparePid.Id, PidFields.Id) != null) ? true : false;
                bool ExistCompare = (CompareTo.Find(comparePid.Id, PidFields.Id) != null) ? true : false;

                if (ExistSource && !ExistCompare)
                    Removed.Add(comparePid);
            }

            return new PidDiff(Added, Removed, NoChange);
        }

        public void GetSystemPids(bool ClearListFirst) {
            if (ClearListFirst) _Pids.Clear();

            foreach (Process SystemProcess in Process.GetProcesses()) 
                this.Add(new Pid(SystemProcess.ProcessName, SystemProcess.Id, SystemProcess.Parent().Id));
        }
    }

    // IEnumerable
    //
    public class PidEnum : IEnumerator {
        public List<Pid> _Pids;
        int position = -1;

        public PidEnum(List<Pid> list) { _Pids = list; }
        public bool MoveNext() { position++; return (position < _Pids.Count); }
        public void Reset() { position = -1; }
        object IEnumerator.Current { get { return Current; } }

        public Pid Current {
            get {
                try { return _Pids[position]; } 
                catch (IndexOutOfRangeException) 
                { throw new InvalidOperationException(); }
            }
        }
    }

}
