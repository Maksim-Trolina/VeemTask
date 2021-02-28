using System;
using System.Linq;

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

                var fileHandler = new FileHandler();
                
                var pathToInputFile = args[0];

                var pathToDirectory = args[1];

                var responses = fileHandler.Check(pathToInputFile,pathToDirectory);

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