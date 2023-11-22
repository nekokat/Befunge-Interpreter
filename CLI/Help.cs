using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class Help
    {
        private readonly CLI _data;
        private string _help;

        public Help(){}

        public Help(CLI data) : this()
        {
            _data = data;
        }

        void Generate()
        {
            _help = _data.ToString();
        }

        List<Command> Commands { get; set; } = new List<Command>();

        public string Data => _help;
        public string Version { get; set; }

    }

}
