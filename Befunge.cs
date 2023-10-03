using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Befunge_Interpreter
{
    public class BefungeInterpreter
    {
        private string[] data;

        public BefungeInterpreter()
        {
            Strings = new Stack<string>();
            ASCIIstring = new Stack<string>();
            Storage = new Stack<(int, int)> (1);
            Row = 0;
            Col = 0;
            ASCIIMode = false;
        }
        Stack<(int, int)> Storage { get; set; }
        string[] Data { get => data; set => data = value; }
        int Row { get; set; }
        int Col { get; set; }
        Stack<string> Strings { get; set; }
        Stack<string> ASCIIstring { get; set; }
        bool ASCIIMode { get; set; }

        void ToData(string code)
        {
            data = code.Split("\n");
        }

        public string Interpret(string code)
        {
            ToData(code);

            char item = Data[Row][Col];

            while (item == '@')
            {
                Col += 1;

                if (Col == data[0].Length)
                {
                    Col = 0;
                    Row += 1;
                }
            }

            return string.Join("", Strings);
        }

        /// <summary>
        /// If char is number than push this number onto the stack.
        /// </summary>
        void IsNumber(char item)
        {
            if (Char.IsNumber(item))
                Strings.Push(item.ToString());
            else
                IsOperator(item);
        }

        Func<string, string> IsOperator(char item)
        {
            return (item) switch
            {
                //Moving
                '>' => Rigth,
                '<' => Left,
                '^' => Up,
                'v' => Down,
                '?' => IsOperator(new char[] { '>', '<', '^', 'v' }[new Random(4).Next()]),
                '_' => Strings.Peek() == "0" ? Rigth : Left,
                '|' => Strings.Peek() == "0" ? Up : Down,
                '#' => Skip,
                //Math
                '+' => Addition,
                '-' => Subtraction,
                '*' => Multiplication,
                '/' => Division,
                '%' => Modulo,
                //Logical
                '!' => LogicalNot,
                '' => GreaterThan, 
                //Stack
                ':' => Duplicate,
                '$' => Discard,
                '\\' => Swap,
                //Constant
                '"' => StringMode,
                'p' => Put,
                'g' => Get
            };;
        }
        
        void Put()
        {
            Storage.Push((Col, Row));
            Data[Row].Remove(Col, 1).Insert(Col, "v");
        }

        void Get()
        {
            (int x, int y) = Storage.Pop();
            //Data[y][x];


        }

        void Duplicate()
        {
            if (Strings.Count == 0)
            {
                Strings.Push("0");
            }
            else
            {
                Strings.Push(Strings.Peek());
            }
        }

        void Skip()
        {
            Col += 1;
        }

        void Discard()
        {
            Strings.Pop();
        }

        void Swap()
        {
            string a;
            string b;

            a = Strings.Pop();
            if (Strings.Count == 0)
            {
                Strings.Push("0");
                Strings.Push(a);
            }
            else
            {
                b = Strings.Pop();
                Strings.Push(a);
                Strings.Push(b);
            }
        }

        void Addition(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push($"{a + b}");
        }

        void Subtraction(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push($"{b - a}");

        }

        void Multiplication(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push($"{b * a}");

        }

        void Division(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push(a == 0 ? "0" : $"{Math.Floor((double)b / a)}");
        }

        void Modulo(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push(a == 0 ? "0" : $"{b % a}");
        }

        void LogicalNot(string numbera)
        {
            Strings.Push(numbera == "0" ? "1" : "0");
        }

        void GreaterThan(string numbera, string numberb)
        {
            int a = Int32.Parse(numbera);
            int b = Int32.Parse(numberb);
            Strings.Push(b > a ? "1" : "0");
        }

        void Up()
        {
            Row -= 1;
        }
        void Down()
        {
            Row += 1;
        }
        void Left()
        {
            Col -= 1;
        }

        void Rigth()
        {
            Col += 1;
        }
        void StringMode()
        {
            ASCIIMode = !ASCIIMode;
        }
    }
}
