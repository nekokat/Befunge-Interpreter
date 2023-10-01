using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Befunge_Interpreter
{
    public class BefungeInterpreter
    {
        private static string[] data;
        private static Stack<string> stack = new Stack<string>();
        private Stack<string> ASCIIstring = new Stack<string>();
        private static int rows = 0;
        private static int columns = 0;
        private bool ASCIIMode = false;

        private static void ToData(string code)
        {
            data = code.Split("\n");
        }

        public static string Interpret(string code)
        {
            ToData(code);

            char item = data[rows][columns];

            while (true)
            {
                columns += 1;

                if (item == '@')
                {
                    break;
                }

                if (columns == data[0].Length)
                {
                    columns = 0;
                    rows += 1;
                }
            }

            return string.Join("", stack);
        }

        /// <summary>
        /// If char is number than push this number onto the stack.
        /// </summary>
        private void IsNumber(char item)
        {

        }

        private void Addition()
        {

        }

        private void Subtraction()
        {

        }

        private void Multiplication()
        {

        }

        private void Division()
        {

        }

        private void Modulo()
        {

        }

        private void LogicalNot()
        {

        }
        private void GreaterThan()
        {

        }

        private void Up()
        {
            rows -= 1;
        }
        private void Down()
        {
            rows += 1;
        }
        private void Left()
        {
            columns -= 1;
        }

        private void Rigth()
        {
            columns += 1;
        }
        private void StringMode()
        {
            ASCIIMode = !ASCIIMode;
        }
    }
}
