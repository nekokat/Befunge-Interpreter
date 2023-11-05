using System;
using System.ComponentModel;
using Befunge_Interpreter;
using Befunge_Interpreter.CLI;

class Program
{
    static void Main(string[] args)
    {
        var parameters = new Parsing(args);
        Console.WriteLine(string.Join(" ; ", args));
        CLI cli = new();

        Command cFile = new("--file", "Read data from file") {
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
            Name = "read",
            Description = "Read from"
        };

        readgrp.Add(cFile);
        readgrp.Add(cString);

        Command cOutput = new("--output", "Output data to file")
        {
            Exec = () => { },
            Alias = new string[] { "-o", "/o" }
        };

        Command cTerminal = new("--string", "Output data in terminal")
        {
            Exec = () => { },
            Alias = new string[] { "-t", "/t" }
        };

        Group outgrp = new()
        {
            Name = "Output",
            Description = "Output to file oe terminal"
        };

        outgrp.Add(cTerminal);
        outgrp.Add(cOutput);

        cli.Add(readgrp);
        cli.Add(outgrp);

        //TODO: https://learn.microsoft.com/ru-ru/dotnet/standard/commandline/define-commands#define-options

        using (StreamWriter sw = new(parameters.Output))
        {
            var bi = new BefungeInterpreter();
            string code = parameters.Input;
            sw.Write(bi.Interpret(code));
        }        
    }
}

