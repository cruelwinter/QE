using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QE.Services
{
    public class AccountService
    {
        public static string aesEncryptBase64(string SourceStr, string CryptoKey)
        {
            string encrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                
            }
            return encrypt;
        }

        public static string aesDecryptBase64(string SourceStr, string CryptoKey)
        {
            string decrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            return decrypt;
        }
    }
}