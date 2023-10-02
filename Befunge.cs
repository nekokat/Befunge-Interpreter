using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter
{
    public class BefungeInterpreter
    {
        private string[] data;

        public BefungeInterpreter()
        {
            Strings = new Stack<string>();
            ASCIIstring = new Stack<string>();
            Row = 0;
            Col = 0;
            ASCIIMode = false;
        }

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

            while (true)
            {
                Col += 1;

                if (item == '@')
                {
                    break;
                }

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

        }

        void Addition()
        {

        }

        void Subtraction()
        {

        }

        void Multiplication()
        {

        }

        void Division()
        {

        }

        void Modulo()
        {

        }

        void LogicalNot()
        {

        }
        void GreaterThan()
        {

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
