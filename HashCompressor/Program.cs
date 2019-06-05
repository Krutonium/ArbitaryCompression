using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
namespace HashCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            UTF8Encoding temp = new UTF8Encoding(true);
            if (File.Exists(args[0])){
                Console.WriteLine("Reading File into Memory...");
                byte[] file = File.ReadAllBytes(args[0]);
                byte[] toHash = new byte[240];
                Console.WriteLine("Beginning Hashing...");
                int incomingOffset = 0;
                using (StreamWriter write = new StreamWriter("output.txt"))
                {
                    while (incomingOffset < file.Length)
                    {
                        int length =
                           Math.Min(toHash.Length, file.Length - incomingOffset);

                        // Changed from Array.Copy as per Marc's suggestion
                        Buffer.BlockCopy(file, incomingOffset,
                                         toHash, 0,
                                         length);

                        incomingOffset += length;

                        //Console.WriteLine(temp.GetString(toHash));
                        write.WriteLine(Hash(toHash));
                    }
                    write.Close();
                }
                Console.WriteLine("Done!");

                
                //Console.WriteLine(temp.GetString(file));
            }

            Console.ReadKey();
        }
        static string Hash(byte[] array)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(array);
            UTF8Encoding temp = new UTF8Encoding(true);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
