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
            Commands = new List<Command>();
            Groups = new();
            Help = new Help(Commands, Groups);

            Command cHelp = new("--help", "Show this help information and exit")
            {
                Exec = () => Console.WriteLine(this.Help),
                Alias = new string[] { "-h", "-H", "-?" }
            };

            Command cVersion = new("--version", "Print version information and exit")
            {
                Exec = () => Console.WriteLine(this.Version),
                Alias = new string[] { "-v", "-V" }
            };

            this.Add(cHelp);
            this.Add(cVersion);
        }

        public string Version { get; set; }

        public Parse Parser { get; set; }

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

        protected List<Command> Commands { get; set; }

        //FIXME
        public Dictionary<string, Group> Groups { get; set; }

        public Help Help { get; set; }

        public Parse Parse { get; set; } = new Parse();


        //Dictionary<string, Action> Aliases { get; set; }
        //Dictionary<string, List<Action>> Group { get; set; }

        public override string ToString() { return string.Empty; }

    }
}
