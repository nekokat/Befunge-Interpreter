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

        Group readgrp = new()
        {
            Name = "read",
            Description = "Read from",
            Actions = new() {
                { "--file", new Command("--file", "Read data from file"){ Actions = () => { }, Aliases = new string[]{ "-f", "/f"} } },
                { "--path", new Command("--path", "file path"){ Actions = () => { }, Aliases = new string[]{ "-p", "/p"} } },
                { "--string", new Command("--string", "Read data from string"){ Actions = () => { }, Aliases = new string[]{ "-s", "/s"} } }
            }
        };

        Group outgrp = new()
        {
            Name = "Output",
            Description = "Output to",
            Actions = new() {
                { "--output", new Command("--output", "Output data to file"){ Actions = () => { }, Aliases = new string[]{ "-o", "/o"} } },
                { "--path", new Command("--path", "file path"){ Actions = () => { }, Aliases = new string[]{ "-p", "/p"} } },
                { "--terminal", new Command("--string", "Output data in terminal"){ Actions = () => { }, Aliases = new string[]{ "-t", "/t"} } }
            }
        };

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

