﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var stringHistory = new Stack<string>();
            stringHistory.Push(string.Empty);

            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine().Split(' ');
                var operation = int.Parse(tokens[0]);

                switch (operation)
                {
                    case 1:
                        {
                            var textAppend = tokens[1];
                            var update = stringHistory.Peek() + textAppend;
                            stringHistory.Push(update);
                            break;
                        }
                    case 2:
                        {
                            var count = int.Parse(tokens[1]);
                            var update = stringHistory.Peek();
                            update = update.Remove(update.Length - count);
                            stringHistory.Push(update);
                            break;
                        }
                    case 3:
                        {
                            var index = int.Parse(tokens[1]);
                            var lastUpdate = stringHistory.Peek();
                            Console.WriteLine(lastUpdate[index - 1]);
                            break;
                        }
                    case 4:
                        {
                            stringHistory.Pop();
                            break;
                        }
                }
            }
        }
    }
}
