using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SecuDEV.Manager
{
    public class CryptoManager
    {

        public static string AESEncrypt256(string text)
        {
            string result = EncryptByAES256(text, "secu13579");
            return result;
        }
        public static string AESDecrypt256(string cryptotext)
        {
            string result = DecryptByAES256(cryptotext, "secu13579");
            return result;
        }

        private static string EncryptByAES256(string text, string password)
        {
            // 사전 설정
            UTF8Encoding ue = new UTF8Encoding();
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.Mode = CipherMode.CBC;
            rijndael.KeySize = 256;

            // key 및 iv 설정
            byte[] pwdBytes = ue.GetBytes(password);
            byte[] keyBytes = new byte[32];
            byte[] IVBytes = new byte[16];
            int lenK = pwdBytes.Length;
            int lenIV = pwdBytes.Length;
            if (lenK > keyBytes.Length) { lenK = keyBytes.Length; }
            if (lenIV > IVBytes.Length) { lenIV = IVBytes.Length; }
            Array.Copy(pwdBytes, keyBytes, lenK);
            Array.Copy(pwdBytes, IVBytes, lenIV);
            rijndael.Key = keyBytes;
            rijndael.IV = IVBytes;

            byte[] message = ue.GetBytes(text);
            ICryptoTransform transform = rijndael.CreateEncryptor();
            // 암호화 수행 
            byte[] cipherBytes = transform.TransformFinalBlock(message, 0, message.Length);
            rijndael.Clear();

            // 16진수로 변환
            string hex = "";
            foreach (byte x in cipherBytes)
            {
                hex += x.ToString("x2");
            }
            return hex;
        }
        private static string DecryptByAES256(string cryptotext, string password)
        {
            // 사전 설정
            UTF8Encoding ue = new UTF8Encoding();
            RijndaelManaged rijndael = new RijndaelManaged();
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.Mode = CipherMode.CBC;
            rijndael.KeySize = 256;

            // 16진수 문자열을 바이트 배열로 변환
            byte[] cipherBytes = new byte[cryptotext.Length / 2];
            for (int i = 0; i < cipherBytes.Length; i++)
            {
                cipherBytes[i] = Convert.ToByte(cryptotext.Substring(i * 2, 2), 16);
            }

            // key 및 iv 설정
            byte[] pwdBytes = ue.GetBytes(password);
            byte[] keyBytes = new byte[32];
            byte[] IVBytes = new byte[16];
            int lenK = pwdBytes.Length;
            int lenIV = pwdBytes.Length;
            if (lenK > keyBytes.Length) { lenK = keyBytes.Length; }
            if (lenIV > IVBytes.Length) { lenIV = IVBytes.Length; }
            Array.Copy(pwdBytes, keyBytes, lenK);
            Array.Copy(pwdBytes, IVBytes, lenIV);
            rijndael.Key = keyBytes;
            rijndael.IV = IVBytes;

            ICryptoTransform transform = rijndael.CreateDecryptor();
            // 암호화 수행
            byte[] message = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            rijndael.Clear();

            return Encoding.UTF8.GetString(message);
        }
    
        public static string EncryptBySHA256(string text)
        {
            System.Security.Cryptography.SHA256 sha = new System.Security.Cryptography.SHA256Managed();

            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(text));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }


    }
}