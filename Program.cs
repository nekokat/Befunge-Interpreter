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
                { "--file", new Command("ReadFromFile", "Read data from file"){ Actions = () => { }, Aliases = new string[]{ "-f", "/f"} } },
                { "--string", new Command("ReadFromString", "Read data from string"){ Actions = () => { }, Aliases = new string[]{ "-s", "/s"} } }
            }
        };

        cli.Add(readgrp);
        //TODO: https://learn.microsoft.com/ru-ru/dotnet/standard/commandline/define-commands#define-options

        using (StreamWriter sw = new(parameters.Output))
        {
            var bi = new BefungeInterpreter();
            string code = parameters.Input;
            sw.Write(bi.Interpret(code));
        }        
    }
}

