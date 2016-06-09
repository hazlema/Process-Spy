using System;
using System.Diagnostics;

namespace Process_Spy {
    static class Sys {
        #region Process exists
        public static bool exists(string pid) {
            return exists(Convert.ToInt32(pid));
        }
        public static bool exists(int pid) {
            foreach (Process thisProcess in Process.GetProcesses()) {
                if (thisProcess.Id == pid)
                    return true;
            }

            return false;
        }
        #endregion
        #region Kill Process
        public static bool kill(string pid) {
            return kill(Convert.ToInt32(pid));
        }
        public static bool kill(int pid) {
            if (exists(pid)) {
                Process thisProcess = Process.GetCurrentProcess();

                try {
                    thisProcess = Process.GetProcessById(pid);
                    thisProcess.Kill();
                } catch { return false; }

                return exists(pid);
            }

            return false;
        }
        #endregion
    }
}
