namespace MIJ_CryptoTest
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class DES
    {
        private string iv = "www.motioninjoy.com";
        private string key = "MotionJy";

        public string Decrypt(string encryptedString)
        {
            string str;
            if (string.IsNullOrEmpty(encryptedString))
            {
                throw new ArgumentNullException("The string can not be null.");
            }
            byte[] bytes = Encoding.Default.GetBytes(this.key);
            byte[] rgbIV = Encoding.Default.GetBytes(this.iv);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] buffer = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write))
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                    }
                    str = Encoding.Default.GetString(stream.ToArray());
                }
                catch
                {
                    throw;
                }
            }
            return str;
        }

        public string Encrypt(string sourceString)
        {
            string str;
            if (string.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentNullException("The string can not be null.");
            }
            byte[] bytes = Encoding.Default.GetBytes(this.key);
            byte[] rgbIV = Encoding.Default.GetBytes(this.iv);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] buffer = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write))
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                    }
                    str = Convert.ToBase64String(stream.ToArray());
                }
                catch
                {
                    throw;
                }
            }
            return str;
        }

        public string IV
        {
            get
            {
                return this.iv;
            }
            set
            {
                this.iv = value;
            }
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }
    }
}

