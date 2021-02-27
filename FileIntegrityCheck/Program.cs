using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FileIntegrityCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    throw new Exception("Invalid input");
                }
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}