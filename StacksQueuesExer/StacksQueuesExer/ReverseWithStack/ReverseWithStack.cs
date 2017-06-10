using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseWithStack
{
    class ReverseWithStack
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();

            var stack = new Stack<int>();

            foreach (var item in numbers)
            {
                stack.Push(item);
            }

            while (stack.Count != 0)
            {
                Console.Write(stack.Pop() + " ");
            }
            Console.WriteLine();
        }
    }
}
