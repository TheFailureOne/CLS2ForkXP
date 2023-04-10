using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassicLogonShell
{
    public class InjectProcess
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedMember.Local

        private const int MAXIMUM_ALLOWED = 0x02000000;
        private const int STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        private const int STANDARD_RIGHTS_READ = 0x00020000;
        private const int TOKEN_ASSIGN_PRIMARY = 0x0001;
        private const int TOKEN_DUPLICATE = 0x0002;
        private const int TOKEN_IMPERSONATE = 0x0004;
        private const int TOKEN_QUERY = 0x0008;
        private const int TOKEN_QUERY_SOURCE = 0x0010;
        private const int TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private const int TOKEN_ADJUST_GROUPS = 0x0040;
        private const int TOKEN_ADJUST_DEFAULT = 0x0080;
        private const int TOKEN_ADJUST_SESSIONID = 0x0100;
        private const int TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        private const int TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
                                              TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
                                              TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
                                              TOKEN_ADJUST_SESSIONID);

        private enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        private enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        }

        private enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public int lpReserved2;
            public int hStdInput;
            public int hStdOutput;
            public int hStdError;
        }

        [DllImport("kernel32.dll")]
        private static extern Int32 WTSGetActiveConsoleSessionId();

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            int DesiredAccess,
            ref IntPtr TokenHandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            UInt32 dwDesiredAccess,
            IntPtr lpTokenAttributes,
            UInt32 ImpersonationLevel,
            UInt32 TokenType,
            ref IntPtr phNewToken);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern Boolean SetTokenInformation(
            IntPtr TokenHandle,
            UInt32 TokenInformationClass,
            ref int TokenInformation,
            int TokenInformationLength);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref IntPtr lpProcessAttributes,
            ref IntPtr lpThreadAttributes,
            bool bInheritHandles,
            int dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            ref PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(
            IntPtr hObject);

        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming

        public void Inject(string desktop, string processPath, string cmdLine = null)
        {
            if (cmdLine != null)
            {
                cmdLine = "\"" + processPath + "\" " + cmdLine;
                processPath = null;
            }
            var pi = new PROCESS_INFORMATION();
            var si = new STARTUPINFO();
            var defaultIntPtr = default(IntPtr);
            var hToken = defaultIntPtr;
            var hDuplicateToken = defaultIntPtr;
            int sessionId = WTSGetActiveConsoleSessionId();
            si.lpDesktop = desktop;
            si.cb = Marshal.SizeOf(si);
            OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_QUERY | TOKEN_DUPLICATE | TOKEN_ADJUST_SESSIONID, ref hToken);
            DuplicateTokenEx(hToken, MAXIMUM_ALLOWED, defaultIntPtr, (uint)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, (uint)TOKEN_TYPE.TokenPrimary, ref hDuplicateToken);
            SetTokenInformation(hDuplicateToken, (uint)TOKEN_INFORMATION_CLASS.TokenSessionId, ref sessionId, sizeof(int));
            CreateProcessAsUser(hDuplicateToken, processPath, cmdLine, ref defaultIntPtr, ref defaultIntPtr, false, 0, defaultIntPtr, null, ref si, ref pi);
            CloseHandle(hToken);
            CloseHandle(hDuplicateToken);
            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
        }

        public void TerminateSystemProcess(string processPath)
        {
            string query = processPath.Contains(@"\") ? String.Format("SELECT * FROM Win32_Process WHERE ExecutablePath='{0}'", processPath.Replace(@"\", @"\\")) : String.Format("SELECT * FROM Win32_Process WHERE Name='{0}'", processPath);
            var mos = new ManagementObjectSearcher(@"\\.\root\CIMv2", query);
            var moc = mos.Get();

            if (moc.Count > 0)
            {
                foreach (ManagementObject mo in moc)
                {
                    try
                    {
                        var owner = new object[1];
                        mo.InvokeMethod("GetOwner", owner);

                        if (owner[0].ToString().Equals("SYSTEM", StringComparison.CurrentCultureIgnoreCase))
                        {
                            mo.InvokeMethod("Terminate", null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
