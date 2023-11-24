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
            Help = new Help(Commands, Groups);
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

        public void Add(Group[] data)
        {
            foreach( Group item in data)
            {
                this.Add(item);
            }
        }

        protected List<Command> Commands { get; set; } = new List<Command>();
        protected Dictionary<string, Group> Groups { get; set; }

        public Help Help { get; set; }

        public Parse Parse { get; set; } = new Parse();


        //Dictionary<string, Action> Aliases { get; set; }
        //Dictionary<string, List<Action>> Group { get; set; }

        public override string ToString() { return string.Empty; }

    }
}
