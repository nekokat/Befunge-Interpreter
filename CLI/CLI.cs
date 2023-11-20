using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class CLI
    {
        //FIXME: REWRITE 
        public CLI()
        {
            Groups = new();
        }

        public void Add(Group data)
        {
            Groups.Add(data.Name, data);
        }

        public void Add(Command data)
        {
            Commands.Add(data);
        }

        public void Add(Command[] data)
        {
            Commands.AddRange(data);
        }

        List<Command> Commands { get; set; }

        Dictionary<string, Group> Groups { get; set; }
        Dictionary<string, Action> Aliases { get; set; }
        Dictionary<string, List<Action>> Group { get; set; }

        public override string ToString() { return string.Empty; }

    }
}
