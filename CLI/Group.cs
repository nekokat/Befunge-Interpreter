using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Befunge_Interpreter.CLI
{
    /// <summary>
    /// 
    /// </summary>
    public class Group
    {
        /// <summary>
        /// 
        /// </summary>
        public string[] Data { get => Actions.Keys.ToArray(); }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Command> Actions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Group name</param>
        public Group(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Group name</param>
        /// <param name="data">List of commands</param>
        public Group(string name, Dictionary<string, Command> data) : this(name)
        {
            Actions = data;
        }

        /// <summary>
        /// Default сonstructor
        /// </summary>
        public Group()
        {
            Name = string.Empty;
            Actions = new();
            Description = string.Empty;
        }

        /// <summary>
        /// Description of the group
        /// </summary>
        /// <param name="name">Group name</param>
        /// <param name="description">Text description of the group</param>
        public Group(string name, string description) : this(name, new Dictionary<string, Command>())
        {
            Description = description;
        }

        /// <summary>
        /// Adds a Command to group with another name
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="func">Command instance</param>
        public void Add(string name, Command func)
        {
            Actions.Add(name, func);
        }

        /// <summary>
        /// Adds a Command to group
        /// </summary>
        /// <param name="func">command instance</param>
        public void Add(Command func)
        {
            Actions.Add(func.Name, func);
        }

        /// <summary>
        /// Show information about the group
        /// </summary>
        /// <returns>Displaying text information about the group</returns>
        public override string ToString()
        {
            string actions = string.Join(string.Empty, Actions.Values);
            return $"\n{Name} ({Description}):\n{actions}";
        }
    }
}
