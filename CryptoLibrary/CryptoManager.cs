using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoLibrary
{
    public class CryptoManager
    {
        /// <summary>
        /// Create private/public key pair 
        /// When 
        /// </summary>
        /// <returns>Public/Private Keypair</returns>
        public static RSACryptoServiceProvider GetKeyPair()
        {
            RSACryptoServiceProvider _rsa;

            // Create both public and private key
            _rsa = new RSACryptoServiceProvider(4096);
            _rsa.PersistKeyInCsp = false;

            return _rsa;
        }


        /// <summary>
        /// Use to create a provider to encrypt a message
        /// </summary>
        /// <param name="publicKey"> XML string of the public key</param>
        /// <returns>Public/Private Keypair</returns>
        public static RSACryptoServiceProvider GetPublicKeyProvider(string publicKey)
        {

            RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider(4096);

            // Import public key 
            _rsa.FromXmlString(publicKey);

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
            byte[] encryptedMsg = _rsa.Encrypt(Encoding.UTF8.GetBytes(message), false);
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
            byte[] decryptedMsg = _rsa.Decrypt(Convert.FromBase64String(encryptedMsg), false);

            return Encoding.UTF8.GetString(decryptedMsg);
        }

    }
}
