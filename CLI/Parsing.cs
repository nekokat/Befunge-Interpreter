namespace Befunge_Interpreter.CLI
{
    public class Parsing
    {
        public Parsing(string[] args)
        {
            Input = args[0];
            Output = args[1];
        }

        public string Input { get; set; }
        public string Output { get; set; }
    }
}