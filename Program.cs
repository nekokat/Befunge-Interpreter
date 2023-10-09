using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        using (StreamWriter sw = new("befunge_output.txt"))
        {
            new BefungeInterpreter().Interpret("0>:00p58*`#@_0>:01p78vv$$<\r\n@^+1g00,+55_v# !`\\+*9<>4v$\r\n@v30p20\"?~^\"< ^+1g10,+*8<$\r\n@>p0\\>\\::*::882**02g*0v >^\r\n`*:*\" d\":+*:-*\"[Z\"+g3 < |<\r\nv-*\"[Z\"+g30*g20**288\\--\\<#\r\n>2**5#>8*:*/00g\"P\"*58*:*v^\r\nv*288 p20/**288:+*\"[Z\"+-<:\r\n>*%03 p58*:*/01g\"3\"* v>::^\r\n   \\_^#!:-1\\+-*2*:*85<^");
        }
        
    }
}
