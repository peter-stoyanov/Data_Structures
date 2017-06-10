using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeepestNode
{
    static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    static void Main(string[] args)
    {
        ReadTree();
        //Console.WriteLine($"Root node: {GetRootNode().Value}");

        var rootNode = GetRootNode();
        //Print(rootNode, 0);

        //var leafs = GetLeafNodes().Select(x => x.Value).OrderBy(x => x).ToList();
        //Console.WriteLine($"Leaf nodes: {String.Join(" ", leafs)}");

        //var middles = GetMiddleNodes().Select(x => x.Value).OrderBy(x => x).ToList();
        //Console.WriteLine($"Middle nodes: {String.Join(" ", middles)}");

        AddDepth(rootNode, 0);
        var deepest = GetDeepestNode(rootNode);
        Console.WriteLine($"Deepest node: {deepest.Value}");

    }

    public static void Print(Tree<int> node, int indent = 0)
    {
        //print the root value at bigger indent each time
        Console.WriteLine(new string(' ', indent) + node.Value);

        foreach (var tree in node.Children)
        {
            Print(tree, indent + 2);
        }
    }

    public static void AddDepth(Tree<int> node, int depth = 0)
    {
        node.Depth = depth;

        foreach (var tree in node.Children)
        {
            AddDepth(tree, depth++);
        }
    }

    /// <summary>
    /// Sets and retrives a tree node
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    public static void AddEdge(int parentValue, int childValue)
    {
        //take out of the Dict and connect
        var childNode = GetTreeNodeByValue(childValue);
        var parentNode = GetTreeNodeByValue(parentValue);

        childNode.Parent = parentNode;
        parentNode.Children.Add(childNode);
    }

    public static Tree<int> GetRootNode()
    {
        return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
    }

    public static List<Tree<int>> GetLeafNodes()
    {
        return nodeByValue.Values.Where(x => x.Children.Count == 0).ToList();
    }

    public static Tree<int> GetDeepestNode(Tree<int> node)
    {
        if (node == null)
        {
            return null;
        }

        int biggestDepth = 0;

        GetDeepestnodeRecursive(node, biggestDepth);
        
    }

    private static void GetDeepestnodeRecursive(Tree<int> node, int biggestDepth)
    {
        foreach (var tree in node.Children)
        {
            GetDeepestnodeRecursive(tree);
        }
    }

    public static List<Tree<int>> GetMiddleNodes()
    {
        return nodeByValue.Values
            .Where(x => x.Children.Count != 0 && x.Parent != null).ToList();
    }

    public static void ReadTree()
    {
        //ReadTree();
        int nodeCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < nodeCount - 1; i++)
        {
            string[] edge = Console.ReadLine().Split(' ');
            AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
        }
    }
}
