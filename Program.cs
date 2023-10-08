using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        StreamWriter sw = new("befunge_output.txt");
        Console.SetOut(sw);
        var k = new BefungeInterpreter();
        Console.Out.Write(k.Interpret("2>:3g\" \"-!v\\  g30          <\r\n |!`\"O\":+1_:.:03p>03g+:\"O\"`|\r\n @               ^  p3\\\" \":<\r\n2 234567890123456789012345678901234567890123456789012345678901234567890123456789"));
        Console.Out.Close();
        sw.Close();
        
    }
}
