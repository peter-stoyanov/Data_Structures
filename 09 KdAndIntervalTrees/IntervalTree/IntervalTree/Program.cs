public class Program
{
    static void Main(string[] args)
    {
        IntervalTree tree = new IntervalTree();
        tree.Insert(20, 36);
        tree.Insert(3, 41);
        tree.Insert(29, 99);
        tree.Insert(0, 1);
        tree.Insert(10, 15);
        tree.Insert(25, 30);
        tree.Insert(60, 72);
        var test = tree.SearchAll(10, 50);
        System.Console.WriteLine(test);
    }
}
