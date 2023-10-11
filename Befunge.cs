using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Befunge_Interpreter
{
    public class BefungeInterpreter
    {
        private char[][] _data;
        //protected string _code;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public BefungeInterpreter()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
            Moving = Rigth;
            OutputStack = new Stack<int>();
            Row = 0;
            Col = 0;
            ASCIIMode = false;
            Output = new ();
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
            //_code = code;
            _data = code.Split("\r\n").Select(i => i.AsSpan().ToArray()).ToArray();
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
                    item = ' ';
                }
            }

            return Output.ToString();
        }

        /// <summary>
        /// Ask user for a number and push it
        /// </summary>
        void InputN()
        {
            string? input = Console.ReadLine();
            if (Int32.TryParse(input.AsSpan()[0].ToString(), out int number))
                OutputStack.Push(number);
        }

        /// <summary>
        /// Ask user for a character and push its ASCII value
        /// </summary>
        void InputS()
        {
            string? input = Console.ReadLine();
            OutputStack.Push((int)input.AsSpan()[0]);
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
                Move(item);
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
                '&' => InputN,
                '~' => InputS,
                ':' => Duplicate,
                '$' => Discard,
                '\\' => Swap,
                //Constant
                'p' => Put,
                'g' => Get,
                //Printing
                '.' => PrintN,
                ',' => PrintS,
                //Other
                // No-op. Does nothing
                ' ' => () => { },
                _ => throw new Exception($"Not imposible read instruction in position {Row}, {Col} with value '{item}'")
            }; ;
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
        void PrintS()
        {
            Output.Append(Convert.ToChar(OutputStack.Pop()));
        }

        /// <summary>
        /// Changing the direction of movement of the carriage
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        void Move(char item)
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
            /*
            int y = OutputStack.Pop();
            int x = OutputStack.Pop();
            int v = OutputStack.Pop();
            */
            Data[OutputStack.Pop()][OutputStack.Pop()] = Convert.ToChar(OutputStack.Pop());
        }

        /// <summary>
        /// A "get" call (a way to retrieve data in storage). Pop y and x,
        /// then push ASCII value of the character at that position in the program
        /// </summary>
        void Get()
        {
            /*
            int y = OutputStack.Pop();
            int x = OutputStack.Pop();
            char v = Data[OutputStack.Pop()][OutputStack.Pop()];
            */
            OutputStack.Push((int)Data[OutputStack.Pop()][OutputStack.Pop()]);
        }

        /// <summary>
        /// Duplicate value on top of the stack
        /// </summary>
        void Duplicate()
        {
            //int value = OutputStack.Count == 0 ? 0 : OutputStack.Peek();
            OutputStack.Push(OutputStack.Count == 0 ? 0 : OutputStack.Peek());
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
            int? b = OutputStack.Count == 0 ? null : OutputStack.Pop();
            OutputStack.Push(b is null ? 0 : a);
            OutputStack.Push(b ?? a);
        }

        /// <summary>
        /// Addition: Pop a and b, then push a+b
        /// </summary>
        void Addition()
        {
            //int a = OutputStack.Pop();
            //int b = OutputStack.Pop();
            OutputStack.Push(OutputStack.Pop() + OutputStack.Pop());
        }

        /// <summary>
        /// Subtraction: Pop a and b, then push b-a
        /// </summary>
        void Subtraction()
        {
            int a = OutputStack.Pop();
            //int b = OutputStack.Pop();
            OutputStack.Push(OutputStack.Pop() - a);
        }

        /// <summary>
        /// Multiplication: Pop a and b, then push a*b
        /// </summary>
        void Multiplication()
        {
            //int a = OutputStack.Pop();
            //int b = OutputStack.Pop();
            OutputStack.Push(OutputStack.Pop() * OutputStack.Pop());
        }

        /// <summary>
        /// Integer division: Pop a and b, then push b/a, rounded towards 0.
        /// </summary>
        void Division()
        {
            int a = OutputStack.Pop();
            OutputStack.Push(a == 0 ? 0 : OutputStack.Pop() / a);
        }

        /// <summary>
        /// Modulo: Pop a and b, then push the remainder of the integer division of b/a.
        /// </summary>
        void Modulo()
        {
            int a = OutputStack.Pop();
            //int b = OutputStack.Pop();
            OutputStack.Push(a == 0 ? 0 : OutputStack.Pop() % a);
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
            //int b = OutputStack.Pop();
            OutputStack.Push(OutputStack.Pop() > a ? 1 : 0);
        }

        /// <summary>
        /// Start moving up
        /// </summary>
        void Up()
        {
            if (--Row < 0)
            {
                Row = Data.Length - 1;
            }
        }

        /// <summary>
        /// Start moving down
        /// </summary>
        void Down()
        {
            if (++Row >= Data.Length)
            {
                Row = 0;
            }
        }

        /// <summary>
        /// Start moving left
        /// </summary>
        void Left()
        {
            if (--Col < 0)
            {
                Col = Data[Row].Length - 1;
            }
        }

        /// <summary>
        /// Start moving right
        /// </summary>
        void Rigth()
        {
            if (++Col >= Data[Row].Length)
            {
                Col = 0;
            }
        }
    }
}
