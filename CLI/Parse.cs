namespace Befunge_Interpreter.CLI
{
    public class Parse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public Parse(string[] args)
        {
            Input = args[0];
            Output = args[1];
        }

        /// <summary>
        /// 
        /// </summary>
        public Parse(){}

        /// <summary>
        /// 
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Output { get; set; }
    }
}