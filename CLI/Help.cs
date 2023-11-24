using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class Help
    {
        private readonly List<Command> _commands;
        private readonly Dictionary<string, Group> _group;

        public Help(){}

        public Help(List<Command> commands, Dictionary<string, Group> group)
        {
            _commands = commands;
            _group = group;
        }

        void Generate()
        {
        }

        public string Version { get; set; }

    }

}
