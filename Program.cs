using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {
        using (StreamWriter sw = new StreamWriter("./befunge_output.txt"))
        {
            //Console.SetOut(sw);
            new BefungeInterpreter().Interpret(">987v>.v\nv456<  :\n>321 ^ _@");
            //Console.Out.Close();
        }
    }
}
