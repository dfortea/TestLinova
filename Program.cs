using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestLinova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nombreFichero;
            Console.WriteLine("Escribe el nombre del fichero");
            nombreFichero = Console.ReadLine();

            var textoFichero = File.ReadAllText(nombreFichero);

            var textoFicheroJson = new MD5Model
            {
                NombreFichero = nombreFichero,
                HashString = CreateMD5(textoFichero)
            };

            Console.WriteLine($"La clave cifrada en md5 es {CreateMD5(textoFichero)}");


            File.WriteAllText("hash.json", JsonConvert.SerializeObject(textoFicheroJson));
        }

        public static string MD5Hash(string input)
        {
            using var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            return Encoding.ASCII.GetString(result);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
