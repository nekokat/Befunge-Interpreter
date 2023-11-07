﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public record Command(string Name, string Description)
    {
        public string Name { get; init; } = Name;
        public string Description { get; init; } = Description;

        public Action Exec { get; init; }

        public string[] Alias { get; init; }

        public override string ToString() => $"\t{Name}, {string.Join(", ", Alias)}\t\t\t\t\t{Description}";
      
    }
}
