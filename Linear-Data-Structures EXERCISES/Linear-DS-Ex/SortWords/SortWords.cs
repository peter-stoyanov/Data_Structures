using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortWords
{
    class SortWords
    {
        static void Main(string[] args)
        {
            //wow softuni alpha	-> alpha softuni wow

            var words = Console.ReadLine().Split().ToList();
            words.Sort();
            Console.WriteLine(String.Join(" ",words));

        }
    }
}
