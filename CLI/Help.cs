using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class Help
    {
        public Help(CLI data)
        {
            List = data;
        }

        CLI List { get; set; }

        void ToString()
        {
            List.ToString();
        }
    }
}
