//TODO: Create Interpritator
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Befunge World!");

        using (StreamWriter sw = new StreamWriter("befunge_output.txt"))
        {
            new BefungeInterpreter().Interpret(">987v>.v\r\nv456<  :\r\n>321 ^ _@");
        }
    }
}
