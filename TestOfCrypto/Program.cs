using CryptoLibrary;
using System;
using System.Security.Cryptography;

namespace TestOfCrypto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string publicKey = "<RSAKeyValue><Modulus>sdkzFr94Bt6yoyvyGICgbZmYvyN48fHiuEcHeffd6n1O1yIJ/Wguv8u/aXmKQvW1s8YqU7TtCFKygHnYDwkoEFG0mIyTTFcJGONhsZRm68A28AX0xxsTxwYZptTyvCK/RXXU/vcRU7Ym7vcLkyG6riOgb+GBJcGPYw4uJR6Rxn5Xt61yx41SRv+WmRVSnesu+/Mj3kJ3MVsHAVYzVN96X7W4Uyp7aY9WN/cBvWmkxh3ikTV7MKv0D4jLov4BgTOmVi/gnHfrsVyAVli+hCsCvwL1lF3CCftFdymodwhyZhwVRts3/rL+A6FpZ6BVOBodyt6DxCk6fD0BVgnso5wUKQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            //CryptoManager crypto = new CryptoManager();

            RSACryptoServiceProvider _rsa2 = CryptoManager.GetKeyPair(); // Create 2048 length keys
            string msg = "<RSAKeyValue><Modulus>q406F0liTnb5Abbym21S7q5DRj+AD+bbtZWDsLBcSVIWeXsIOH391sou/65GF27QYWbvOruqVFEV5JjjcY4O8ATb1cRDw/hQ7rA9oD7Sam+68yrHCA7dRzSUWKCJ6cqr5xuE8hPvMTZaP34DTOkWPmL/0R8Ivm+zrkX1u3sfyLrDi8BR3Xs07dP6lOaBAZKmZSSU/+4Ya2/IQdof8KGK0s4CwUFRQWKidzL5acF/u585d5Hh2pNGvZqHUPHLQK2beKpt0mVUqZ5JOC7xnPQmFaZS5680TgHBb/AroC50O7HTI4gMjPLKpgO51UhcYzfpCQkiKyTpW3F1Wa2toMFI/Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var test = _rsa2.ToXmlString(false);
            RSACryptoServiceProvider _rsa = CryptoManager.GetPublicKeyProvider(test); // Create 2048 length keys

            //var test2 = _rsa.ToXmlString(false);
            //var test3 = _rsa2.ToXmlString(false);
            //byte[] text = Encoding.UTF8.GetBytes("Hej");
            //var encryption = _rsa.Encrypt(text, RSAEncryptionPadding.Pkcs1);
            //string msg = "Hej med dig";
            string encryptedMsg = CryptoManager.EncryptString(_rsa, msg);
            Console.WriteLine(encryptedMsg);
            //var test2 = Encoding.UTF8.GetBytes(msg);

            //var test3 = Convert.ToBase64String(msg);
            //Console.WriteLine(test3);
            //Console.WriteLine("------------------Decoded------------------");

            //var test4 = Convert.ToBase64String(test2);
            //Console.WriteLine(test4);
            //Console.WriteLine("-------------");
            //var test5 = Encoding.UTF8.GetString(test2);
            //Console.WriteLine(test5);



            //var decryption = _rsa.Decrypt(encryption, );

            //Console.WriteLine(CryptoManager.DecryptString(_rsa, encryptedMsg));

            //Console.WriteLine(_rsa.ToXmlString(false));
            //RSA.ToXmlString(false);
        }
    }
}
