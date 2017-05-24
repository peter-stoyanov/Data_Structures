using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestSubsequence
{
    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            //12 2 7 4 3 3 8	-> 3 3

            var numbers = Console.ReadLine().Split().Select(x => int.Parse(x)).ToList();

            int start = 0;
            int maxStart = 0;
            int length = 1;
            int maxLength = 1;

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] == numbers[i-1])
                {
                    length++;
                }
                else
                {
                    if (length > maxLength)
                    {
                        maxLength = length;
                        maxStart = start;
                    }
                    start = i;
                    length = 1;
                }

                //edge case when end of list is reached
                if (i == numbers.Count - 1)
                {
                    if (length > maxLength)
                    {
                        maxLength = length;
                        maxStart = start;
                    }
                }
            }
            Console.WriteLine(String.Join(" ", numbers.Skip(maxStart).Take(maxLength)));
        }
    }
}
