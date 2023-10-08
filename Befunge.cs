using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Befunge_Interpreter
{
    public class BefungeInterpreter
    {
        private char[][] _data;
        string _code;

        public BefungeInterpreter()
        {
            Moving = Rigth;
            Storage = new Stack<(int, int)>();
            Out = new Stack<int>();
            Row = 0;
            Col = 0;
            ASCIIMode = false;       
        }

        Action Moving { get; set; }
        Stack<int> Out { get; set; }
        Stack<(int, int)> Storage { get; set; }
        char[][] Data { get => _data; set => _data = value; }
        int Row { get; set; }
        int Col { get; set; }
        bool ASCIIMode { get; set; }

        /// <summary>
        /// Converting program code to a character set
        /// </summary>
        /// <param name="code">executable instructions</param>
        void ToData(string code)
        {
            _code = code;
            _data = code.Split("\r\n").Select(i => i.ToCharArray()).ToArray();
        }

        /// <summary>
        /// Interpreting program code
        /// </summary>
        /// <param name="code">executable instructions</param>
        /// <returns>stack data as a string</returns>
        public string Interpret(string code)
        {
            ToData(code);
            StringWriter output = new();

            Console.SetOut(output);

            char item = Data[0][0];

            while (item != '@')
            {
                IsNumber(item);
                Moving.Invoke();
                item = Data[Row][Col];
            }

            Console.Out.Close();
            output.Close();
            return output.ToString();
        }

        /// <summary>
        /// If char is number than push this number onto the stack.
        /// </summary>
        void IsNumber(char item)
        {
            if (item == '\"')
            {
                StringMode();
            }
            else if (Int32.TryParse(item.ToString(), out int number))
            {
                Out.Push(number);
            }
            else if (ASCIIMode)
            {
                Out.Push((int)item);
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
                'p' => Put,
                'g' => Get,
                '.' => PrintN,
                ',' => PrintA,
                ' ' => NoOperation,
                _ =>  throw new Exception($"Not imposible read instruction in position {Row}, {Col} with value '{item}'")
            };
        }

        /// <summary>
        /// Start string mode: push each character's ASCII value all the way up to the next "
        /// </summary>
        void StringMode()
        {
            ASCIIMode = !ASCIIMode;
        }

        /// <summary>
        /// Pop value and output as an integer followed by a space
        /// </summary>
        void PrintN()
        {
            Console.Out.Write(Out.Pop());
        }

        /// <summary>
        /// Pop value and output as ASCII character
        /// </summary>
        void PrintA()
        {
            Console.Out.Write((char)Out.Pop());
        }

        /// <summary>
        /// Changing the direction of movement of the carriage
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        void SetMove(char item)
        {
            Moving = item switch
            {
                '>' => Rigth,
                '<' => Left,
                '^' => Up,
                'v' => Down,
                //Start moving in a random cardinal direction
                '?' => new Action[] { Rigth, Left, Up, Down }[new Random().Next(4)],
                //Pop a value; move right if value=0, left otherwise
                '_' => Out.Pop() == 0 ? Rigth : Left,
                //Pop a value; move down if value=0, up otherwise
                '|' => Out.Pop() == 0 ? Up : Down,
                //Bridge: Skip next cell
                '#' => Skip,
                _ => throw new Exception()
            };
        }

        /// <summary>
        /// A "put" call (a way to store a value for later use). Pop y, x, and v,
        /// then change the character at (x,y)
        /// in the program to the character with ASCII value v
        /// </summary>
        void Put()
        {
            Storage.Push((Col, Row));
            Data[Row][Col] = 'v';
        }

        /// <summary>
        /// A "get" call (a way to retrieve data in storage). Pop y and x,
        /// then push ASCII value of the character at that position in the program
        /// </summary>
        void Get()
        {
            //TODO: Create: Get
            //(int x, int y) = Storage.Pop();
            //Data[y][x];
        }

        /// <summary>
        /// Duplicate value on top of the stack
        /// </summary>
        void Duplicate()
        {
            if (Out.Count == 0)
            {
                Out.Push(0);
            }
            else
            {
                Out.Push(Out.Peek());
            }
        }

        /// <summary>
        /// No-op. Does nothing
        /// </summary>
        void NoOperation() => Console.Out.Write(string.Empty);

        /// <summary>
        /// Bridge: Skip next cell
        /// </summary>
        void Skip() => Moving();

        /// <summary>
        /// Pop value from the stack and discard it
        /// </summary>
        void Discard() => Out.Pop();

        /// <summary>
        /// Swap two values on top of the stack
        /// </summary>
        void Swap()
        {
            int a;
            int b;

            a = Out.Pop();
            if (Out.Count == 0)
            {
                Out.Push(0);
                Out.Push(a);
            }
            else
            {
                b = Out.Pop();
                Out.Push(a);
                Out.Push(b);
            }
        }

        /// <summary>
        /// Addition: Pop a and b, then push a+b
        /// </summary>
        void Addition()
        {
            Out.Push(Out.Pop() + Out.Pop());
        }

        /// <summary>
        /// Subtraction: Pop a and b, then push b-a
        /// </summary>
        void Subtraction()
        {
            int a = Out.Pop();
            int b = Out.Pop();
            Out.Push(b - a);

        }

        /// <summary>
        /// Multiplication: Pop a and b, then push a*b
        /// </summary>
        void Multiplication()
        {
            Out.Push(Out.Pop() * Out.Pop());
        }

        /// <summary>
        /// Integer division: Pop a and b, then push b/a, rounded towards 0.
        /// </summary>
        void Division()
        {
            int a = Out.Pop();
            int b = Out.Pop();
            Out.Push(a == 0 ? 0 : b / a);
        }

        /// <summary>
        /// Modulo: Pop a and b, then push the remainder of the integer division of b/a.
        /// </summary>
        void Modulo()
        {
            int a = Out.Pop();
            int b = Out.Pop();
            Out.Push(a == 0 ? 0 : b % a);
        }

        /// <summary>
        /// Logical NOT: Pop a value. If the value is zero, push 1; otherwise, push zero.
        /// </summary>
        void LogicalNot()
        {
            Out.Push(Out.Pop() == 0 ? 1 : 0);
        }

        /// <summary>
        /// Greater than: Pop a and b, then push 1 if b>a, otherwise zero.
        /// </summary>
        void GreaterThan()
        {
            int a = Out.Pop();
            int b = Out.Pop();
            Out.Push(b > a ? 1 : 0);
        }

        /// <summary>
        /// Start moving up
        /// </summary>
        void Up() => Row = (Data.Length + Row - 1) % Data.Length;

        /// <summary>
        /// Start moving down
        /// </summary>
        void Down() => Row = (Row+1)%Data.Length;

        /// <summary>
        /// Start moving left
        /// </summary>
        void Left() => Col = (Col-1 + Data[Row].Length) % Data[Row].Length;

        /// <summary>
        /// Start moving right
        /// </summary>
        void Rigth() => Col = (Col+1) % Data[Row].Length;
    }
}
