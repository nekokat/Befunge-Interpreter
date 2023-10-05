//TODO: Create Interpritator
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Befunge World!");

        using (StreamWriter sw = new StreamWriter("befunge_output.txt"))
        {
            //new BefungeInterpreter().Interpret("7^DN>vA\r\nv_#v? v\r\n7^<\"\"\"\"\r\n3  ACGT\r\n90!\"\"\"\"\r\n4*:>>>v\r\n+8^-1,<\r\n> ,+,@)");
            new BefungeInterpreter().Interpret("5 :>>#;1-:48*+01p*01g48*-#;1#+-#1:#<_$.@");
        }
    }
}
