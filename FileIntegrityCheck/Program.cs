using System;
using System.IO;

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

                var responses = GetResponses(args[0], args[1]);

                foreach (var response in responses)
                {
                    var message = GetMessage(response);

                    Console.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Response[] GetResponses(string pathToInputFile, string pathToDirectory)
        {
            if (!File.Exists(pathToInputFile))
            {
                throw new Exception("Not found input file");
            }

            var fileHandler = new FileHandler();
            
            var responses = fileHandler.CheckIntegrityFiles(pathToInputFile, pathToDirectory);

            return responses;
        }

        private static string GetMessage(Response response)
        {
            return response switch
            {
                Response.Ok => response.ToString().ToUpper(),
                Response.Fail => response.ToString().ToUpper(),
                Response.NotFound => "NOT FOUND",
                _ => throw new Exception("Undefined result")
            };
        }
    }
}