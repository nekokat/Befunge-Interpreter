using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    /// <summary>
    /// 
    /// </summary>
    public class Help
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Command> Commands { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, Group> Groups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="group"></param>
        public Help(List<Command> commands, Dictionary<string, Group> group)
        {
            Commands = commands;
            Groups = group;
        }

        /// <summary>
        /// 
        /// </summary>
        private string Data { get; set; }       

        /// <summary>
        /// 
        /// </summary>
        public void Generate()
        {
            StringBuilder data = new StringBuilder();

            Commands.ForEach(item => data.Append(item));

            Groups.Values.ToList().ForEach(item => data.Append(item));           

            Data = data.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            this.Generate();
            return Data;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

    }

}
