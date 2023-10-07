using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        StreamWriter sw = new("befunge_output.txt");
        Console.SetOut(sw);
        Console.Out.Write(new BefungeInterpreter().Interpret(">987v>.v\r\nv456<  :\r\n>321 ^ _@"));
        Console.Out.Close();
        sw.Close();
        
    }
}
