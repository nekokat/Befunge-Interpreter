using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter.CLI
{
    public class Help
    {
        private readonly CLI _data;
        private string _help;

        public Help(CLI data){ _data = data; }

        void Generate()
        {
            _help = _data.ToString();
        }

        public string Data => _help;
        public string Version { get; set; }

    }

}
