using System;
using Befunge_Interpreter;
using CLI;

class Program
{
    static void Main(string[] args)
    {
        var parameters = new Parsing(args);
        Console.WriteLine(string.Join(" ; ", args));
        //TODO: https://learn.microsoft.com/ru-ru/dotnet/standard/commandline/define-commands#define-options
        using (StreamWriter sw = new(parameters.Output))
        {
            BefungeInterpreter bi = new();
            string code = parameters.Input;
            sw.Write(bi.Interpret(code));
        }
        
    }
}
