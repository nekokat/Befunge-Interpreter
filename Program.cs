using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        using (StreamWriter sw = new("befunge_output.txt"))
        {
            new BefungeInterpreter().Interpret("");
        }
        
    }
}
