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
        /// <param name="name"></param>
        public Group(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public Group(string name, Dictionary<string, Command> data) : this(name)
        {
            Actions = data;
        }

        /// <summary>
        /// 
        /// </summary>
        public Group()
        {
            Name = string.Empty;
            Actions = new();
            Description = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Group(string name, string description) : this(name, new Dictionary<string, Command>())
        {
            Description = description;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="func"></param>
        public void Add(string name, Command func)
        {
            Actions.Add(name, func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        public void Add(Command func)
        {
            Actions.Add(func.Name, func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string actions = string.Join(string.Empty, Actions.Values);
            return $"\n{Name} ({Description}):\n{actions}";
        }
    }
}
