using System;
using Befunge_Interpreter;

class Program
{
    static void Main(string[] args)
    {

        StreamWriter sw = new("befunge_output.txt");
        Console.SetOut(sw);
        var k = new BefungeInterpreter();
        Console.Out.Write(k.Interpret("^v3:-1$$_,#! #:<\\*52\",Take one down, pass it around,\"*520     <\r\n^     >0\"elttob erom oN\">:#,_$\"s\"\\1-#v_$>0\"reeb fo \">:#,_$:2-!|\r\n>>\\:!#^_:.>0\"elttob\"    ^            >, ^\r\n^1:_@#:,,,\".\":<_v#!-3\\*25$_,#! #:<\" on the wall\"0             <\r\n^2:,,\",\"        <\r\n<v1:*9+29"));
        Console.Out.Close();
        sw.Close();
        
    }
}
