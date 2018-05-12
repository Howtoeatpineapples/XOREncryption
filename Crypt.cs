using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XORCrypt
{
    class Crypt
    {
       
        public byte[] PrivateKey { get; private set; }
        

        public void InputPassphrase(string passPhrase)
        {
            HashAlgorithm sha256 = new SHA256CryptoServiceProvider();
            byte[] buffer = sha256.ComputeHash(Encoding.UTF8.GetBytes(passPhrase));
            PrivateKey = buffer;
        }


        public void LoadFile(string filePath)
        {
            GenerateKey(File.ReadAllBytes(filePath).Length);
        }

        public void EncryptDecryptFile(string filePath, string fileName)
        {
            File.Create(filePath + "output.txt").Close();

            File.WriteAllBytes(filePath + "output.txt", EncryptDecrypt(File.ReadAllBytes(filePath + fileName)));
            
            File.Delete(filePath + fileName);
            File.Move(filePath + "output.txt", filePath + fileName);
        }
         
        public byte[] EncryptDecrypt(byte[] input)
        {
            byte[] output = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte)(input[i] ^ PrivateKey[i % PrivateKey.Length]);
            }

            return output;
        }


        private void GenerateKey(int size)
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] keyBuffer = new byte[size];
            rNGCryptoServiceProvider.GetBytes(keyBuffer);
            PrivateKey = keyBuffer;
        }

        public static string FileToString(string filePath)
        {
            var reader = new System.IO.StreamReader(filePath, System.Text.Encoding.UTF8);
            var text = reader.ReadToEnd();

            reader.Close();
            return text;
        }

        public static void StringToFile(string filePath, string text)
        {
            var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8);

            writer.Write(text);
            writer.Close();
        }

    }
}
