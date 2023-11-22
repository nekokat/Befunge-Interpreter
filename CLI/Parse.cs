namespace Befunge_Interpreter.CLI
{
    public class Parse
    {
        public Parse(string[] args)
        {
            Input = args[0];
            Output = args[1];
        }

        public Parse(){}

        public string Input { get; set; }
        public string Output { get; set; }
    }
}