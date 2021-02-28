using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace FileIntegrityCheck
{
    public class FileHandler
    {
        public List<Response> Check(string pathToDirectory, string pathToInputFile)
        {
            var options = Read(pathToInputFile);

            return options
                .Select(option => Check(option, pathToDirectory))
                .ToList();
        }

        private List<string[]> Read(string path)
        {
            using (var sr = new StreamReader(path))
            {
                string line;

                var output = new List<string[]>();

                while ((line = sr.ReadLine()) != null)
                {
                    output.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }

                return output;
            }
        }

        private Response Check(string[] options, string pathToDirectory)
        {
            if (options.Length < 3)
            {
                throw new Exception("Invalid options");
            }

            var path = pathToDirectory + options[0];

            if (!File.Exists(path))
            {
                return Response.NotFound;
            }

            bool isEqual;

            var hashAlgorithm = options[1];

            var hashSum = options[2];

            using (var sr = new StreamReader(path))
            {
                var text = sr.ReadToEnd();

                switch (hashAlgorithm)
                {
                    case "md5":
                        var md5 = new MD5CryptoServiceProvider();
                        isEqual = IsEqual(md5, text, hashSum);
                        break;
                    case "sha1":
                        var sha1 = new SHA1CryptoServiceProvider();
                        isEqual = IsEqual(sha1, text, hashSum);
                        break;
                    case "sha256":
                        var sha256 = new SHA256CryptoServiceProvider();
                        isEqual = IsEqual(sha256, text, hashSum);
                        break;
                    default:
                        throw new Exception("No such hashing algorithm found");
                }
            }

            return isEqual ? Response.Ok : Response.Fail;
        }

        private bool IsEqual(HashAlgorithm hashAlgorithm, string text, string hashSum)
        {
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text));

            return Convert.ToHexString(hash).ToLower() == hashSum;
        }
    }
}