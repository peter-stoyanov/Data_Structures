using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequence
{
    class Sequence
    {
        static void Main(string[] args)
        {
            //2	   =>      2,   3, 5, 4,   4, 7, 5,   6, 11, 7,   5, 9, 6,   5, 9, 6,   8, 15, 9,   6, 11, 7,   7, 13, 8, 12, 23, 13, 8, 15, 9, 6, 11, 7, 10, 19, 11, 7, 13, 8, 6, 11, 7, 10, 19, 11, 7, 13, 8, 9

            var n = int.Parse(Console.ReadLine());

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(n);
            
            int current = queue.Peek();

            int index = 1;
            while (queue.Count <= 50)
            {

                queue.Enqueue(current + 1);
                queue.Enqueue(2 * current + 1);
                queue.Enqueue(current + 2);

                current = current + index;
                index++;
            }

            Console.WriteLine(String.Join(", ", queue));

        }
    }
}
