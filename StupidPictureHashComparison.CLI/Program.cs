using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Перетащите один или несколько файлов на исполняемый файл.");
            return;
        }

        foreach (string filePath in args)
        {
            if (File.Exists(filePath))
            {
                string hash = ComputeSha256Hash(filePath);
                Console.WriteLine($"Файл: {filePath}");
                Console.WriteLine($"SHA-256 Хэш: {hash}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Файл не найден: {filePath}");
            }
        }

        Console.ReadLine();
    }

    static string ComputeSha256Hash(string filePath)
    {
        using (FileStream stream = File.OpenRead(filePath))
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
