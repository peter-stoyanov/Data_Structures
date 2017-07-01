using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveOddOccurences
{
    class RemoveOddOccurences
    {
        static void Main(string[] args)
        {
            //1 2 3 4 1	1 1

            var numbers = Console.ReadLine().Split().Select(x => int.Parse(x)).ToList();

            var converted = numbers.ConvertAll(x => new { value = x, odd = numbers.FindAll(y => y == x).Count % 2 != 0 });

            foreach (var i in converted)
            {
                if (i.odd == false)
                {
                    Console.Write($"{i.value} ");
                }
            }
            Console.WriteLine();
        }
    }
}
