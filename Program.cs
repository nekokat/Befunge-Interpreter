using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        StreamWriter sw = new("befunge_output.txt");
        Console.SetOut(sw);
        Console.Out.Write(new BefungeInterpreter().Interpret(">              v\r\nv  ,,,,,\"Hello\"<\r\n>48*,          v\r\nv,,,,,,\"World!\"<\r\n>25*,@"));
        Console.Out.Close();
        sw.Close();
        
    }
}
