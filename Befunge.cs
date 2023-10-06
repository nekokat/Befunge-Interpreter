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
        private char[][] _data;
        public BefungeInterpreter()
        {
            Moving = Rigth;
            Numbers = new Stack<int>();
            ASCIIstring = new Stack<string>();
            Storage = new Stack<(int, int)>();
            Out = new Stack<string>();
            Row = 0;
            Col = 0;
            ASCIIMode = false;
        }

        Action Moving { get; set; }
        Stack<string> Out { get; set; }
        Stack<(int, int)> Storage { get; set; }
        char[][] Data { get => _data; set => _data = value; }
        int Row { get; set; }
        int Col { get; set; }
        Stack<int> Numbers { get; set; }
        Stack<string> ASCIIstring { get; set; }
        bool ASCIIMode { get; set; }

        void ToData(string code)
        {
            _data = code.Split("\r\n").Select(i => i.ToCharArray()).ToArray();
        }

        public string Interpret(string code)
        {
            ToData(code);

            char item = Data[Row][Col];
            while (item != '@')
            {
                item = Data[Row][Col];
                IsNumber(item);
            }

            return string.Join("", Out.Count == 0 ? ASCIIstring.Count == 0 ? Numbers.Count == 0 ? "" : Numbers : ASCIIstring : Out);
        }

        /// <summary>
        /// If char is number than push this number onto the stack.
        /// </summary>
        void IsNumber(char item)
        {
            if (Int32.TryParse(item.ToString(), out int number))
            {
                Numbers.Push(number);
            }
            else if (ASCIIMode && item != '"')
            {
                ASCIIstring.Push(item.ToString());
            }
            else if ("><^v?_|#".Contains(item))
            {
                SetMove(item);
            }
            else
            {
                IsOperator(item).Invoke();
            }

            Moving.Invoke();
        }

        Action IsOperator(char item)
        {
            return item switch
            {
                //Math
                '+' => Addition,
                '-' => Subtraction,
                '*' => Multiplication,
                '/' => Division,
                '%' => Modulo,
                //Logical
                '!' => LogicalNot,
                '`' => GreaterThan,
                //Stack
                ':' => Duplicate,
                '$' => Discard,
                '\\' => Swap,
                //Constant
                'p' => Put,
                'g' => Get,
                '.' => PrintN,
                ',' => PrintA,
                ' ' => NoOperation,
                '"' => StringMode,
                _ =>  throw new Exception($"Not imposible read instruction in position {Row}, {Col} with value '{item}'")
            };
        }

        void StringMode()
        {
            ASCIIMode = !ASCIIMode;
        }

        void PrintN()
        {
            Out.Push(Numbers.Pop().ToString());
        }

        void PrintA()
        {
            Out.Push(ASCIIstring.Pop());
        }

        void SetMove(char item)
        {
            Moving = item switch
            {
                '>' => Rigth,
                '<' => Left,
                '^' => Up,
                'v' => Down,
                '?' => new Action[] { Rigth, Left, Up, Down }[new Random().Next(4)],
                '_' => Numbers.Peek() == 0 ? Rigth : Left,
                '|' => Numbers.Peek() == 0 ? Up : Down,
                '#' => Skip,
                _ => throw new Exception()
            };
        }

        void Put()
        {
            Storage.Push((Col, Row));
            Data[Row][Col] = 'v';
        }

        void Get()
        {
            //(int x, int y) = Storage.Pop();
            //Data[y][x];
        }

        void Duplicate()
        {
            if (Numbers.Count == 0)
            {
                Numbers.Push(0);
            }
            else
            {
                Numbers.Push(Numbers.Peek());
            }
        }

        void NoOperation() => Moving();

        void Skip() => Moving();

        void Discard() => Numbers.Pop();

        void Swap()
        {
            int a;
            int b;

            a = Numbers.Pop();
            if (Numbers.Count == 0)
            {
                Numbers.Push(0);
                Numbers.Push(a);
            }
            else
            {
                b = Numbers.Pop();
                Numbers.Push(a);
                Numbers.Push(b);
            }
        }

        void Addition()
        {
            Numbers.Push(Numbers.Pop() + Numbers.Pop());
        }

        void Subtraction()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(b - a);

        }

        void Multiplication()
        {
            Numbers.Push(Numbers.Pop() * Numbers.Pop());
        }

        void Division()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(a == 0 ? 0 : b / a);
        }

        void Modulo()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(a == 0 ? 0 : b % a);
        }

        void LogicalNot()
        {
            Numbers.Push(Numbers.Pop() == 0 ? 1 : 0);
        }

        void GreaterThan()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(b > a ? 1 : 0);
        }

        void Up() => Row = (Data.Length + Row - 1) % Data.Length;

        void Down() => Row = (Row+1)%Data.Length;

        void Left() => Col = (Col-1 + Data[Row].Length) % Data[Row].Length;

        void Rigth() => Col = (Col+1) % Data[Row].Length;
    }
}
