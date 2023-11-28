using System;
using Befunge_Interpreter;
using Befunge_Interpreter.CLI;

class Program
{
    static void Main(string[] args)
    {
        //var parameters = new Parse(args);
        //Console.WriteLine(string.Join(" ; ", args));
        CLI cli = new();

        Command cFile = new("--file", "Read data from file")
        {
            Exec = () => { },
            Alias = new string[] { "-f", "/f" } 
        };

        Command cString = new("--string", "Read data from string")
        {
            Exec = () => { },
            Alias = new string[] { "-s", "/s" }
        };

        Group readgrp = new()
        {
            Name = "Read",
            Description = "Read from"
        };

        readgrp.Add(cFile);
        readgrp.Add(cString);

        Command cOutput = new("--output", "Output data to file")
        {
            Exec = OutputToFile,
            Alias = new string[] { "-o", "/o" }
        };

        Command cTerminal = new("--string", "Output data in terminal")
        {
            Exec = OutputToConsole,
            Alias = new string[] { "-t", "/t" }
        };

        Group outgrp = new()
        {
            Name = "Output",
            Description = "Output to file or terminal"
        };

        Command cHelp = new("--help", "Show this help information and exit")
        {
            Exec = () => { },
            Alias = new string[] { "-h", "-H", "-?" }
        };

        Command cVersion = new("--version", "Print version information and exit")
        {
            Exec = () => { },
            Alias = new string[] { "-v", "-V" }
        };

        outgrp.Add(cTerminal);
        outgrp.Add(cOutput);

        cli.Add(new Command[] { cHelp, cVersion });

        cli.Add(new Group[] { readgrp, outgrp });

        Console.WriteLine(cli.Help);

        //TODO: https://learn.microsoft.com/ru-ru/dotnet/standard/commandline/define-commands#define-options

        /*
        using (StreamWriter sw = new(parameters.Output))
        {
            var bi = new BefungeInterpreter();
            string code = parameters.Input;
            sw.Write(bi.Interpret(code));
        }
        */

        static void OutputToFile()
        {
            string filename = "output.txt";
            using (StreamWriter sw = new(filename))
            {
                var bi = new BefungeInterpreter();
                string code = "022p25*\":ereh drow ruoy retnE\">,# :# _>~:25*-#v_v>22g1+:98+-#v_v\r\n                                      ^p3g22              p22<\r\n  0                                           >  ^\r\n***************** v                             <              <\r\n    0             >22g1+22p>22g1-:22p#v_25*,@\r\n     0\r\n                           ^               p 3g55\"*\",<\r\n                                      v                <\r\n                                        >:55p3g:\"*\"-#^_^\r\n\r\n                                      v ^  <\r\n                               >94+   #    ^\r\n                               v      <    ^\r\n                               #  >5*    > ^\r\n                                  2      6 ^\r\n                              v?vv?v# ?#v?7^\r\n                              999999  # 58 ^\r\n                              76532   >1   ^\r\n                              +++++  v?v   ^\r\n                                     234   ^\r\n                              >>>>>>>>>>>>>^";
                sw.Write(bi.Interpret(code));
            }
        }

        static void OutputToConsole()
        {
            //string filename = "output.txt";
            var bi = new BefungeInterpreter();
            string code = "022p25*\":ereh drow ruoy retnE\">,# :# _>~:25*-#v_v>22g1+:98+-#v_v\r\n                                      ^p3g22              p22<\r\n  0                                           >  ^\r\n***************** v                             <              <\r\n    0             >22g1+22p>22g1-:22p#v_25*,@\r\n     0\r\n                           ^               p 3g55\"*\",<\r\n                                      v                <\r\n                                        >:55p3g:\"*\"-#^_^\r\n\r\n                                      v ^  <\r\n                               >94+   #    ^\r\n                               v      <    ^\r\n                               #  >5*    > ^\r\n                                  2      6 ^\r\n                              v?vv?v# ?#v?7^\r\n                              999999  # 58 ^\r\n                              76532   >1   ^\r\n                              +++++  v?v   ^\r\n                                     234   ^\r\n                              >>>>>>>>>>>>>^";
            Console.WriteLine(bi.Interpret(code));
        }    
        
        //cli.Groups["Read"].Actions["--file"].Exec.Invoke();        
        OutputToConsole();
    }
}

