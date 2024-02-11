using System.Runtime.InteropServices;

namespace ZamzamUiCompact.Services.Security;

public static class CredentialManager
{
    public static void SaveCredentials(string targetName, string userName, string password, string containerName)
    {
        bool success = CredUI.CredPackAuthenticationBuffer(0, userName, password, IntPtr.Zero, 0, out nint credPtr);
        if (!success)
        {
            throw new InvalidOperationException("Failed to pack authentication buffer.");
        }

        CREDENTIAL credential = new()
        {
            TargetName = targetName,
            Type = CRED_TYPE.GENERIC,
            Persist = CRED_PERSIST.LOCAL_MACHINE,
            CredentialBlobSize = (uint)Marshal.SizeOf(typeof(CREDENTIAL)),
            CredentialBlob = credPtr,
            UserName = userName,
            AttributeCount = 0,
            Attributes = IntPtr.Zero,
            TargetAlias = null,
            Comment = default,
            Flags = 0
        };

        success = CredUI.CredWrite(ref credential, 0);
        if (!success)
        {
            throw new InvalidOperationException("Failed to write credential to Credential Manager.");
        }

        // Free the memory allocated for the credential buffer
        Marshal.FreeHGlobal(credPtr);
    }

    public static NetworkCredential GetCredentials(string targetName)
    {
        IntPtr credPtr;
        bool success = CredUI.CredRead(targetName, CRED_TYPE.GENERIC, 0, out credPtr);
        if (!success)
        {
            throw new InvalidOperationException("Failed to read credential from Credential Manager.");
        }

        var credential = (CREDENTIAL)Marshal.PtrToStructure(credPtr, typeof(CREDENTIAL));
        string userName, password;

        // Unpack the credential buffer to retrieve username and password
        success = CredUI.CredUnPackAuthenticationBuffer(0, credential.CredentialBlob, credential.CredentialBlobSize, out userName, out password);
        if (!success)
        {
            throw new InvalidOperationException("Failed to unpack authentication buffer.");
        }

        // Free the memory allocated for the credential buffer
        Marshal.FreeHGlobal(credPtr);

        return new NetworkCredential(userName, password);
    }

}


