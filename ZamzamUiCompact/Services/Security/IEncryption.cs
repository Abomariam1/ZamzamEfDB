using System.Security.Cryptography;

namespace ZamzamUiCompact.Services.Security
{
    public interface IEncryption
    {
        byte[] CreateRandomEntropy();
        byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length);
        string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV);
        (byte[], byte[]) DeriveKeyAndIV(byte[] keyBytes);
        int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S);
        byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV);
        byte[] GenerateKey(string key);
    }
}