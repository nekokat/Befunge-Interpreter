using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public record Command(string Name, string Description)
    {
        //FIXME
        public delegate void Execute();

        public string Name { get; init; } = Name;
        public string Description { get; init; } = Description;

        public Execute Exec { get; init; }

        public string[] Alias { get; init; }

        public override string ToString()
        {
            return $"  {Name}, {string.Join(", ", Alias)}            {Description}\n";
        }
    }
}
