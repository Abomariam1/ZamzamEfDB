using System.Security.Cryptography;
using System.Text;

namespace ZamzamUiCompact.Services.Security;

public class Encryption : IEncryption
{
    public byte[] GenerateKey(string key)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(key);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return hashBytes;
        }
    }
    public (byte[], byte[]) DeriveKeyAndIV(byte[] keyBytes)
    {
        // Use the first half of the SHA256 hash as the key, and the second half as the IV
        int halfLength = keyBytes.Length / 2;
        byte[] derivedKey = new byte[halfLength];
        byte[] derivedIV = new byte[halfLength];

        Array.Copy(keyBytes, 0, derivedKey, 0, halfLength);
        Array.Copy(keyBytes, halfLength, derivedIV, 0, halfLength);

        return (derivedKey, derivedIV);
    }
    public byte[] CreateRandomEntropy()
    {
        // Create a byte array to hold the random value.
        byte[] entropy = new byte[16];

        // Create a new instance of the RNGCryptoServiceProvider.
        // Fill the array with a random value.
        RandomNumberGenerator.Create().GetBytes(entropy);
        // Return the array.
        return entropy;
    }
    public byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new
                    StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
    public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;
        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor =
            aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new(cipherText))
            {
                using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }
    public int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S)
    {
        if (Buffer == null)
            throw new ArgumentNullException(nameof(Buffer));
        if (Buffer.Length <= 0)
            throw new ArgumentException("The buffer length was 0.", nameof(Buffer));
        if (Entropy == null)
            throw new ArgumentNullException(nameof(Entropy));
        if (Entropy.Length <= 0)
            throw new ArgumentException("The entropy length was 0.", nameof(Entropy));
        if (S == null)
            throw new ArgumentNullException(nameof(S));

        int length = 0;

        // Encrypt the data and store the result in a new byte array. The original data remains unchanged.
        byte[] encryptedData = ProtectedData.Protect(Buffer, Entropy, Scope);

        // Write the encrypted data to a stream.
        if (S.CanWrite && encryptedData != null)
        {
            S.Write(encryptedData, 0, encryptedData.Length);

            length = encryptedData.Length;
        }

        // Return the length that was written to the stream.
        return length;
    }
    public byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length)
    {
        if (S == null)
            throw new ArgumentNullException(nameof(S));
        if (Length <= 0)
            throw new ArgumentException("The given length was 0.", nameof(Length));
        if (Entropy == null)
            throw new ArgumentNullException(nameof(Entropy));
        if (Entropy.Length <= 0)
            throw new ArgumentException("The entropy length was 0.", nameof(Entropy));

        byte[] inBuffer = new byte[Length];
        byte[] outBuffer;

        // Read the encrypted data from a stream.
        if (S.CanRead)
        {
            S.Read(inBuffer, 0, Length);

            outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope);
        }
        else
        {
            throw new IOException("Could not read the stream.");
        }

        // Return the decrypted data
        return outBuffer;
    }
}
