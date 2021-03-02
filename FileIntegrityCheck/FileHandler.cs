using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;


namespace FileIntegrityCheck
{
    public class FileHandler
    {
        public Response[] CheckIntegrityFiles(string pathToInputFile, string pathToDirectory)
        {
            var options = ReadInputFile(pathToInputFile);

            return options
                .Select(option => CheckIntegrityFile(option, pathToDirectory))
                .ToArray();
        }

        private List<string[]> ReadInputFile(string path)
        {
            using (var sr = new StreamReader(path))
            {
                string line;

                var options = new List<string[]>();

                while ((line = sr.ReadLine()) != null)
                {
                    options.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }

                return options;
            }
        }

        private Response CheckIntegrityFile(string[] options, string pathToDirectory)
        {
            if (options.Length < 3)
            {
                throw new Exception("Invalid options");
            }

            var fileName = options[0];

            var path = pathToDirectory + fileName;

            if (!File.Exists(path))
            {
                return Response.NotFound;
            }

            var isFileIntegrity = IsFileIntegrity(path, options[1], options[2]);

            return isFileIntegrity ? Response.Ok : Response.Fail;
        }

        private bool IsFileIntegrity(string path, string hashAlgorithm, string hashSum)
        {
            var content = new FileStream(path, FileMode.Open);

            switch (hashAlgorithm)
            {
                case "md5":
                    var md5 = new MD5CryptoServiceProvider();
                    return IsFileIntegrity(md5, content, hashSum);
                case "sha1":
                    var sha1 = new SHA1CryptoServiceProvider();
                    return IsFileIntegrity(sha1, content, hashSum);
                case "sha256":
                    var sha256 = new SHA256CryptoServiceProvider();
                    return IsFileIntegrity(sha256, content, hashSum);
                default:
                    throw new Exception("No such hashing algorithm found");
            }
        }

        private bool IsFileIntegrity(HashAlgorithm hashAlgorithm, FileStream content, string hashSum)
        {
            var hash = hashAlgorithm.ComputeHash(content);

            return Convert.ToHexString(hash).ToLower() == hashSum;
        }
    }
}