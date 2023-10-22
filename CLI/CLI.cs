using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class CLI
    {
        public CLI()
        {
            Groups = new();
        }

        Dictionary<string, Group> Groups { get; set; }
        Dictionary<string, Action> Aliases { get; set; }
        Dictionary<string, List<Action>> Group { get; set; }


    }
}
