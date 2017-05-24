using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_DS_Ex
{
    class SumAndAverage
    {
        static void Main(string[] args)
        {
            //4 5 6	Sum=15; Average=5.00

            var numbers = Console.ReadLine().Split().Select(x => int.Parse(x)).ToList();
            Console.Write($"Sum={numbers.Sum()}; ");
            Console.Write($"Average={numbers.Average():f2}");
            Console.WriteLine();
        }
    }
}
