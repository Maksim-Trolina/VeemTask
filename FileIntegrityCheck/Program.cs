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

                var responses = fileHandler.Check(args[1], args[0]);

                foreach (var output in responses.Select(response => response switch
                {
                    Response.Ok => response.ToString().ToUpper(),
                    Response.Fail => response.ToString().ToUpper(),
                    Response.NotFound => "NOT FOUND",
                    _ => throw new Exception("Undefined result")
                }))
                {
                    Console.WriteLine(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}