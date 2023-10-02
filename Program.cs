//TODO: Create Interpritator
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Befunge World!");

        using (StreamWriter sw = new StreamWriter("befunge_output.txt"))
        {
            BefungeInterpreter().Interpret(">& : 2v2:    <\n      `      /\n      !      2 \n@,\"t\" _  :2%!|\n             >\"f\",@");
        }
    }
}
