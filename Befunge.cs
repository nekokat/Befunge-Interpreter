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
        string[] Data { get => data; set => data = value; }
        int Row { get; set; }
        int Col { get; set; }
        Stack<int> Numbers { get; set; }
        Stack<string> ASCIIstring { get; set; }
        bool ASCIIMode { get; set; }

        void ToData(string code)
        {
            data = code.Split("\r\n");
        }

        public string Interpret(string code)
        {
            ToData(code);

            char item = Data[Row][Col];
            while (item != '@')
            {
                item = Data[Row][Col];
                IsNumber(item);
                Moving.Invoke();
            }

            return string.Join("", Out);
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
            else if (ASCIIMode)
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
                '\"' => StringMode,
                'p' => Put,
                'g' => Get,
                '.' => PrintN,
                ',' => PrintA,
                ' ' => NoOperation,
                _ => throw new Exception($"Not imposible read instruction in position {Row}, {Col} with value '{item}'")
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
            Random random = new();
            Moving = item switch
            {
                '>' => Rigth,
                '<' => Left,
                '^' => Up,
                'v' => Down,
                '?' => new Action[] { Rigth, Left, Up, Down }[random.Next(4)],
                '_' => Numbers.Peek() == 0 ? Rigth : Left,
                '|' => Numbers.Peek() == 0 ? Up : Down,
                '#' => Skip,
                _ => throw new Exception()
            };
        }

        void Put()
        {
            Storage.Push((Col, Row));
            Data[Row] = Data[Row].Remove(Col, 1).Insert(Col, "v");
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
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(a + b);
        }

        void Subtraction()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(b - a);

        }

        void Multiplication()
        {
            int a = Numbers.Pop();
            int b = Numbers.Pop();
            Numbers.Push(b * a);

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

        void Up() => Row--;

        void Down() => Row++;

        void Left() => Col--;

        void Rigth() => Col++;
    }
}
