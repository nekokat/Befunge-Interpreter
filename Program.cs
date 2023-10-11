using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {
        using (StreamWriter sw = new("befunge_output.txt"))
        {
            BefungeInterpreter bi = new();
            string code = "1.01>:888***++\\1+:67+`#@_\\:888**%\\888**/\\:.v\r\n    ^                                      <";
            sw.Write(bi.Interpret(code));
        }
        
    }
}
