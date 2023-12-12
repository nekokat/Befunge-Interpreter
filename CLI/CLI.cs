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
    public class CLI
    {
        //FIXME: REWRITE
        
        /// <summary>
        /// 
        /// </summary>
        public CLI()
        {
            Commands = new List<Command>();
            Groups = new();
            Help = new Help(Commands, Groups);

            
            Command cHelp = new("--help", "Show this help information and exit")
            {
                Exec = () => Console.WriteLine(this.Help),
                Alias = new string[] { "-h", "-H", "-?" }
            };

            Command cVersion = new("--version", "Print version information and exit")
            {
                Exec = () => Console.WriteLine(this.Version),
                Alias = new string[] { "-v", "-V" }
            };

            this.Add(cHelp);
            this.Add(cVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Parse Parser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(Group data)
        {
            Groups.Add(data.Name, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(Command data)
        {
            Commands.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(Command[] data)
        {
            Commands.AddRange(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Add(Group[] data)
        {
            foreach( Group item in data)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<Command> Commands { get; set; }

        //FIXME
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Group> Groups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Help Help { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Parse Parse { get; set; } = new Parse();


        //Dictionary<string, Action> Aliases { get; set; }
        //Dictionary<string, List<Action>> Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() { return string.Empty; }

    }
}
