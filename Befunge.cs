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
            //TODO: & - Запросить у пользователя число и поместить его в стек
            //TODO: ~ - Запросить у пользователя символ и поместить в стек его ASCII - код
            Moving = Rigth;
            OutputStack = new();
            Row = 0;
            Col = 0;
            ASCIIMode = false;
            Output = new();
        }

        StringBuilder Output { get; set; }
        Action Moving { get; set; }
        public Stack<int> OutputStack { get; set; }
        char[][] Data { get => _data; set => _data = value; }
        int Row { get; set; }
        int Col { get; set; }
        bool ASCIIMode { get; set; }

        /// <summary>
        /// Converting program code to a character set
        /// </summary>
        /// <param name="code">executable instructions</param>
        void Parse(string code)
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
            Parse(code);

            char item = Data[0][0];

            while (item != '@')
            {
                Run(item);
                Moving.Invoke();
                item = Data[Row][Col];
                // Bridge: Skip next cell
                if (item == '#')
                {
                    Moving.Invoke();
                    Moving.Invoke();
                    item = Data[Row][Col];
                }
            }

            return Output.ToString();
        }

        /// <summary>
        /// If char is number than push this number onto the stack.
        /// </summary>
        void Run(char item)
        {
            if (item == '\"')
            {
                // Start string mode: push each character's ASCII value all the way up to the next "
                ASCIIMode = !ASCIIMode;
            }
            else if (Int32.TryParse(item.ToString(), out int number))
            {
                OutputStack.Push(number);
            }
            else if (ASCIIMode)
            {
                OutputStack.Push((int)item);
            }
            else if ("><^v?_|".Contains(item))
            {
                SetMove(item);
            }
            else
            {
                Operator(item).Invoke();
            }
        }

        Action Operator(char item)
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
                //Printing
                '.' => PrintN,
                ',' => PrintA,
                //Other
                ' ' => NoOperation,
                _ => throw new Exception($"Not imposible read instruction in position {Row}, {Col} with value '{item}'")
            };
        }

        /// <summary>
        /// Pop value and output as an integer followed by a space
        /// </summary>
        void PrintN()
        {
            Output.Append(OutputStack.Pop());
        }

        /// <summary>
        /// Pop value and output as ASCII character
        /// </summary>
        void PrintA()
        {
            Output.Append(Convert.ToChar(OutputStack.Pop()));
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
                '_' => OutputStack.Pop() == 0 ? Rigth : Left,
                //Pop a value; move down if value=0, up otherwise
                '|' => OutputStack.Pop() == 0 ? Down : Up,
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
            int y = OutputStack.Pop();
            int x = OutputStack.Pop();
            int v = OutputStack.Pop();
            Data[y][x] = Convert.ToChar(v);
        }

        /// <summary>
        /// A "get" call (a way to retrieve data in storage). Pop y and x,
        /// then push ASCII value of the character at that position in the program
        /// </summary>
        void Get()
        {
            int y = OutputStack.Pop();
            int x = OutputStack.Pop();
            char v = Data[y][x];
            OutputStack.Push((int)v);
        }

        /// <summary>
        /// Duplicate value on top of the stack
        /// </summary>
        void Duplicate()
        {
            int value = OutputStack.Count == 0 ? 0 : OutputStack.Peek();
            OutputStack.Push(value);
        }

        /// <summary>
        /// No-op. Does nothing
        /// </summary>
        void NoOperation()
        {
            Output.Append(string.Empty);
        }

        /// <summary>
        /// Pop value from the stack and discard it
        /// </summary>
        void Discard() => OutputStack.Pop();

        /// <summary>
        /// Swap two values on top of the stack
        /// </summary>
        void Swap()
        {
            int a = OutputStack.Pop();
            if (OutputStack.Count == 0)
            {
                OutputStack.Push(0);
                OutputStack.Push(a);
            }
            else
            {
                int b = OutputStack.Pop();
                OutputStack.Push(a);
                OutputStack.Push(b);
            }
        }

        /// <summary>
        /// Addition: Pop a and b, then push a+b
        /// </summary>
        void Addition()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(a + b);
        }

        /// <summary>
        /// Subtraction: Pop a and b, then push b-a
        /// </summary>
        void Subtraction()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(b - a);

        }

        /// <summary>
        /// Multiplication: Pop a and b, then push a*b
        /// </summary>
        void Multiplication()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(a * b);
        }

        /// <summary>
        /// Integer division: Pop a and b, then push b/a, rounded towards 0.
        /// </summary>
        void Division()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(a == 0 ? 0 : b / a);
        }

        /// <summary>
        /// Modulo: Pop a and b, then push the remainder of the integer division of b/a.
        /// </summary>
        void Modulo()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(a == 0 ? 0 : b % a);
        }

        /// <summary>
        /// Logical NOT: Pop a value. If the value is zero, push 1; otherwise, push zero.
        /// </summary>
        void LogicalNot()
        {
            OutputStack.Push(OutputStack.Pop() == 0 ? 1 : 0);
        }

        /// <summary>
        /// Greater than: Pop a and b, then push 1 if b>a, otherwise zero.
        /// </summary>
        void GreaterThan()
        {
            int a = OutputStack.Pop();
            int b = OutputStack.Pop();
            OutputStack.Push(b > a ? 1 : 0);
        }

        /// <summary>
        /// Start moving up
        /// </summary>
        void Up()
        {
            Row--;
            if (Row < 0)
            {
                Row = Data.Length - 1;
            }
        }

        /// <summary>
        /// Start moving down
        /// </summary>
        void Down()
        {
            Row++;
            if (Row >= Data.Length)
            {
                Row = 0;
            }
        }

        /// <summary>
        /// Start moving left
        /// </summary>
        void Left()
        {
            Col--;
            if (Col < 0)
            {
                Col = Data[Row].Length - 1;
            }
        }

        /// <summary>
        /// Start moving right
        /// </summary>
        void Rigth()
        {
            Col++;
            if (Col >= Data[Row].Length)
            {
                Col = 0;
            }

        }
    }
}
