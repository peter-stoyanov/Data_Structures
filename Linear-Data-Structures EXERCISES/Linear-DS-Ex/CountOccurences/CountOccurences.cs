using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountOccurences
{
    class CountOccurences
    {
        static void Main(string[] args)
        {
            //3 4 4 2 3 3 4 3 2	     2 -> 2 times

            var numbers = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            var converted = numbers.ConvertAll(x => new { value = x, repeated = numbers.FindAll(y => y == x).Count });

            foreach (var i in converted.Distinct())
            {
                if (i.repeated >= 1)
                {
                    Console.WriteLine($"{i.value} -> {i.repeated} times");
                }
            }

        }
    }
}
