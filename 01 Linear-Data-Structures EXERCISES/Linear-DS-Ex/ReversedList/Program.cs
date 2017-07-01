using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var reversedList = new ReversedList<int>();

            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);

            //reversedList.RemoveAt(2);

            //for (int i = 0; i < reversedList.Count; i++)
            //{
            //    Console.WriteLine(reversedList[i]);
            //}

            foreach (var item in reversedList)
            {
                Console.WriteLine(item);
            }

        }
    }
}
