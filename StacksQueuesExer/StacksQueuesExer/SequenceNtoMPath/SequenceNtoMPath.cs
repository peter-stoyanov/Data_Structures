using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceNtoMPath
{
    class SequenceNtoMPath
    {
        static void Main(string[] args)
        {

            var input = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            var n = input[0];
            var m = input[1];

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(new Node(n, null));

            while (queue.Count != 0)
            {
                var x = queue.Dequeue();

                if (x.Value == m)
                {
                    PrintSolution(x);
                    break;
                }
                else if (x.Value < m)
                {
                    queue.Enqueue(new Node(x.Value + 1, x));
                    queue.Enqueue(new Node(x.Value + 2, x));
                    queue.Enqueue(new Node(x.Value * 2, x));
                }
                //else if()
                //{
                //    Console.WriteLine("(no solution)");
                //    break;
                //}
            }
        }

        private static void PrintSolution(Node x)
        {
            var printList = new Stack<int>();

            while (x.Previous != null)
            {
                printList.Push(x.Value);
                x = x.Previous;
            }

            printList.Push(x.Value);

            Console.WriteLine(String.Join(" -> ", printList));
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Previous { get; set; }
        public Node(int value, Node previous)
        {
            this.Value = value;
            this.Previous = previous;
        }
    }
}
