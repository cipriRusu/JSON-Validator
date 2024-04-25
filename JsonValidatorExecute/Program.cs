using System;

namespace JsonValidatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileTextContent = System.IO.File.ReadAllText(args[0]);
            var jsonFileValidator = new JsonValidator(fileTextContent);
            Console.WriteLine(jsonFileValidator.IsValid());
        }
    }
}
