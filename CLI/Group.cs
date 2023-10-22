using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Befunge_Interpreter.CLI
{
    public struct Group
    {
        public string[] Data { get => Actions.Keys.ToArray(); }
        Dictionary<string, Action> Actions { get; set; }
        public string Description { get; set; }

        string Name { get; set; }

        public Group(string name) : this()
        {
            Name = name;
        }

        public Group(string name, Dictionary<string, Action> data) : this(name)
        {
            Name = name;
            Actions = data;
        }

        public Group()
        {
            Name = string.Empty;
            Actions = new();
            Description = string.Empty;
        }

        void Add(string name, Action func)
        {
            Actions.Add(name, func);
        }
    }
}
