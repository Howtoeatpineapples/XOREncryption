using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XORCrypt
{
    class Program
    {
       
        static void Main(string[] args)
        {
            

            Crypt crypt = new Crypt();
            
            Console.Write("Please enter your passphrase: ");
            crypt.InputPassphrase(Console.ReadLine());

            Console.Clear();

            string dir = string.Empty;
            string fileName = string.Empty;
            do {
                Console.Clear();
                // E.g. : C:\Users\user\Desktop\
                Console.Write("Please write the full directory the file is in: ");
                dir = Console.ReadLine();
                // E.g. : file.png
                Console.Write("Please write the name of the file you wish to encrypt: ");
                fileName = Console.ReadLine();
            } while (!File.Exists(dir + fileName));

            while (true)
            {
                Console.WriteLine("Press any key to encrypt/decrypt");
                Console.ReadKey();
                crypt.EncryptDecryptFile(dir, fileName);
                Console.Clear();
                Console.WriteLine("File encrypted/decrypted");
            }
        }
    }
}
