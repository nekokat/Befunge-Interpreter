using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Befunge_Interpreter.CLI
{
    public class Group
    {
        public string[] Data { get => Actions.Keys.ToArray(); }
        public Dictionary<string, Command> Actions { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }

        public Group(string name) : this()
        {
            Name = name;
        }

        public Group(string name, Dictionary<string, Command> data) : this(name)
        {
            Actions = data;
        }

        public Group()
        {
            Name = string.Empty;
            Actions = new();
            Description = string.Empty;
        }

        public Group(string name, string description) : this(name, new Dictionary<string, Command>())
        {
            Description = description;
        }

        public void Add(string name, Command func)
        {
            Actions.Add(name, func);
        }

        public void Add(Command func)
        {
            Actions.Add(func.Name, func);
        }
    }
}
