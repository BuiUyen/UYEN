using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Globalization;

namespace Sanita.Utility.Encryption
{
    public static class CryptorEngine
    {
        public static String DecriptMediCoin(String strNewKey)
        {
            StringBuilder txtKeyBuilder = new StringBuilder();

            try
            {
                if (string.IsNullOrEmpty(strNewKey))
                {
                    return null;
                }

                if ((strNewKey.Length % 2 != 0) || (strNewKey.Length == 0))
                {
                    return null;
                }

                for (int i = 0; i < strNewKey.Length; i += 2)
                {
                    String text = strNewKey.Substring(i, 2);
                    int int_text = 0;
                    int_text = Convert.ToInt32(text, 16);

                    if (int_text == 0)
                    {
                        return null;
                    }
                    txtKeyBuilder.Append((char)int_text);
                }
                String strKeyEncript = txtKeyBuilder.ToString();
                return CryptorEngine.Decrypt(strKeyEncript, true);                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static String EncriptMediCoin(String strInput)
        {
            String strKey = CryptorEngine.Encrypt(strInput, true);
            String strNewKey = "";

            for (int i = 0; i < strKey.Length; i++)
            {
                int x = strKey[i];
                strNewKey += string.Format("{0:X2}", x);
            }

            return strNewKey;
        }

        public static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string CreateSH1Hash(string input)
        {
            // Use input string to calculate MD5 hash
            SHA1 md5 = System.Security.Cryptography.SHA1.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string DoEncrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            string key = "HACKERANCUTMEDIBOX@$$%%*&^%)(!&#hkjhkjsd^(*&2098423423423";
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            if (String.IsNullOrEmpty(toEncrypt))
            {
                return toEncrypt;
            }

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                string key = "HACKERANCUTMEDIBOX@$$%%*&^%)(!&#hkjhkjsd^(*&2098423423423";
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return toEncrypt;
            }
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string cipherString, bool useHashing)
        {
            if (String.IsNullOrEmpty(cipherString))
            {
                return cipherString;
            }

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key = "HACKERANCUTMEDIBOX@$$%%*&^%)(!&#hkjhkjsd^(*&2098423423423";

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherString;
            }
        }

        public static string Encrypt(string key, string toEncrypt, bool useHashing)
        {
            if (String.IsNullOrEmpty(toEncrypt))
            {
                return toEncrypt;
            }

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                string key2 = key;
                if (String.IsNullOrEmpty(key2))
                {
                    key2 = "HACKERANCUTMEDIBOX@$$%%*&^%)(!&#hkjhkjsd^(*&2098423423423";
                }
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key2));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key2);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return toEncrypt;
            }
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string Decrypt(string key, string cipherString, bool useHashing)
        {
            if (String.IsNullOrEmpty(cipherString))
            {
                return cipherString;
            }

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key2 = key;
                if (String.IsNullOrEmpty(key2))
                {
                    key2 = "HACKERANCUTMEDIBOX@$$%%*&^%)(!&#hkjhkjsd^(*&2098423423423";
                }
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key2));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key2);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return cipherString;
            }
        }
    }
}
