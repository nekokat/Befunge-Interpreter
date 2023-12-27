using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    public record Command(string Name, string Description)
    {
        //FIXME
        /// <summary>
        /// Link to method for executing
        /// </summary>
        public delegate void Execute();

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; init; } = Name;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; init; } = Description;

        /// <summary>
        /// 
        /// </summary>
        public Execute Exec { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Alias { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"  {Name}, {string.Join(", ", Alias)}            {Description}\n";
        }
    }
}
