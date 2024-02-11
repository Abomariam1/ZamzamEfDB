using System.Runtime.InteropServices;

namespace ZamzamUiCompact.Services.Security;

public static class CredUI
{
    [DllImport("credui.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool CredPackAuthenticationBuffer(int dwFlags, string pszUserName, string pszPassword, IntPtr pPackedCredentials, int pcbPackedCredentials, out IntPtr ppbAuthBuffer);

    [DllImport("Advapi32.dll", SetLastError = true, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode)]
    public static extern bool CredWrite([In] ref CREDENTIAL Credential, [In] UInt32 Flags);

    [DllImport("Advapi32.dll", SetLastError = true, EntryPoint = "CredReadW", CharSet = CharSet.Unicode)]
    public static extern bool CredRead(string target, CRED_TYPE type, UInt32 reservedFlag, out IntPtr credentialPtr);

    [DllImport("credui.dll", CharSet = CharSet.Auto)]
    public static extern bool CredUnPackAuthenticationBuffer(int dwFlags, IntPtr pAuthBuffer, uint cbAuthBuffer, out string pszUserName, out string pszDomainName);

}


