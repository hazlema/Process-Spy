using System.Diagnostics;

namespace Process_Spy {
    public static class ProcessExtensions {
        private static string FindIndexedProcessName(int pid) {
            try {
                var processName = Process.GetProcessById(pid).ProcessName;
                var processesByName = Process.GetProcessesByName(processName);
                string processIndexdName = null;

                for (var index = 0; index < processesByName.Length; index++) {
                    processIndexdName = index == 0 ? processName : processName + "#" + index;
                    var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                    if ((int)processId.NextValue() == pid) {
                        return processIndexdName;
                    }
                }

                return processIndexdName;
            } catch { }

            return "";
        }

        private static Process FindPidFromIndexedProcessName(string indexedProcessName) {
            if (indexedProcessName != "") {
                var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
                try {
                    return Process.GetProcessById((int)parentId.NextValue());
                } catch (System.ArgumentException ex) { Debug.WriteLine(ex.Message); }
            }

            return Process.GetProcessById(0);
        }

        public static Process Parent(this Process process) {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
        }
    }
}
