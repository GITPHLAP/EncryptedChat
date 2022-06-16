using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLibrary
{
    public class CryptoManager
    {

        /// <summary>
        /// Create private/public key pair 
        /// With the defined publickey
        /// </summary>
        /// <param name="publicKey"> XML string of the public key</param>
        /// <returns>KeyPair</returns>
        public static RSACryptoServiceProvider GetKeyPair(string publicKey)
        {
            RSACryptoServiceProvider _rsa;

            // Import public key 
            _rsa = new RSACryptoServiceProvider(2048);
            _rsa.FromXmlString(publicKey);


            // Create private key part 
            _rsa = new RSACryptoServiceProvider(2048);

            return _rsa;
        }

        /// <summary>
        /// Encrypt a string with given rsa keypair
        /// </summary>
        /// <param name="_rsa">KeyPair</param>
        /// <param name="message"></param>
        /// <returns>Encrypted base64 string</returns>
        public static string EncryptString(RSACryptoServiceProvider _rsa, string message)
        {
            byte[] encryptedMsg = _rsa.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedMsg);
        }

        /// <summary>
        /// Decrypt a string with given rsa keypair
        /// </summary>
        /// <param name="_rsa"> KeyPair</param>
        /// <param name="encryptedMsg">Should be Base64 string</param>
        /// <returns>Decrypted message</returns>
        public static string DecryptString(RSACryptoServiceProvider _rsa, string encryptedMsg)
        {
            byte[] decryptedMsg = _rsa.Decrypt(Convert.FromBase64String(encryptedMsg), RSAEncryptionPadding.Pkcs1);

            return Encoding.UTF8.GetString(decryptedMsg);
        }

    }
}
