using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class Help
    {
        private List<Command> Commands { get; set; }
        private Dictionary<string, Group> Groups { get; set; }

        public Help(){}

        public Help(List<Command> commands, Dictionary<string, Group> group)
        {
            Commands = commands;
            Groups = group;
        }

        private string Data { get; set; }       

        public void Generate()
        {
            StringBuilder data = new StringBuilder();

            Commands.ForEach(item => data.Append(item));

            foreach (string item in Groups.Keys)
            {
                data.Append(Groups[item]);
            }

            Data = data.ToString();
        }

        public override string ToString()
        {
            this.Generate();
            return Data;
        }

        public string Version { get; set; }

    }

}
