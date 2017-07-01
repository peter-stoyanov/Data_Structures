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

        //AddDepth(0);
        var deepest = GetDeepestNode(rootNode);
        Console.WriteLine($"Deepest node: {deepest.Value}");

        //var longestPath = GetLongestPath();
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

    public static void AddDepth(int depth = 0)
    {
        var rootNode = GetRootNode();

        AddDepth(rootNode, depth);

    }

    private static void AddDepth(Tree<int> node, int depth)
    {
        node.Depth = depth;

        if (node.Children != null)
        {
            foreach (var tree in node.Children)
            {
                AddDepth(tree, depth + 1);
            }
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
        Tree<int> deepest = node;

        GetDeepestnodeRecursive(node, biggestDepth, deepest);

        return deepest ?? null;

    }

    private static void GetDeepestnodeRecursive(Tree<int> node, int biggestDepth, Tree<int> deepest)
    {
        if (node.Depth >= biggestDepth)
        {
            deepest = node;
            foreach (var tree in node.Children)
            {
                GetDeepestnodeRecursive(tree, deepest.Depth, deepest);
            }

        }
    }

    private static void CalculateDepth(Tree<int> currentNode, int currentDepth, Dictionary<int, List<Tree<int>>> nodesByDepth)
    {
        if (!nodesByDepth.ContainsKey(currentDepth))
        {
            nodesByDepth[currentDepth] = new List<Tree<int>>();
        }

        foreach (var child in currentNode.Children)
        {
            nodesByDepth[currentDepth].Add(child);
            CalculateDepth(child, currentDepth + 1, nodesByDepth);
        }
    }

    public static Tree<int> GetDeepestNode()
    {
        Dictionary<int, List<Tree<int>>> nodesByDepth = new Dictionary<int, List<Tree<int>>>();
        nodesByDepth.Add(0, new List<Tree<int>>());

        Tree<int> rootNode = GetRootNode();

        nodesByDepth[0].Add(rootNode);

        CalculateDepth(rootNode, 1, nodesByDepth);

        return nodesByDepth.LastOrDefault(x => x.Value.Count > 0).Value.FirstOrDefault();
    }

    private static void CalculatePaths(Tree<int> currentNode, List<Tree<int>> currentPath, List<Tree<int>> maxPath)
    {
        foreach (var child in currentNode.Children)
        {
            currentPath.Add(child);
            CalculatePaths(child, currentPath, maxPath);
        }

        if (currentPath.Count > maxPath.Count)
        {
            maxPath.Clear();

            foreach (var node in currentPath)
            {
                maxPath.Add(node);
            }
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    private static List<Tree<int>> GetLongestPath()
    {
        List<Tree<int>> currentPath = new List<Tree<int>>();
        List<Tree<int>> maxPath = new List<Tree<int>>();

        Tree<int> rootNode = GetRootNode();
        currentPath.Add(rootNode);

        CalculatePaths(rootNode, currentPath, maxPath);

        return maxPath;
    }

    public static List<Tree<int>> GetMiddleNodes()
    {
        return nodeByValue.Values
            .Where(x => x.Children.Count != 0 && x.Parent != null).ToList();
    }

    private static void CalculatePathsWithGivenSum(int sum, Tree<int> currentNode, List<Tree<int>> currentPath, List<List<Tree<int>>> allPaths)
    {
        foreach (var child in currentNode.Children)
        {
            currentPath.Add(child);
            CalculatePathsWithGivenSum(sum, child, currentPath, allPaths);
        }

        int currentSum = 0;

        foreach (var child in currentPath)
        {
            currentSum += child.Value;
        }

        if (currentSum == sum)
        {
            allPaths.Add(new List<Tree<int>>(currentPath));
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    private static List<List<Tree<int>>> GetAllPathsWithGivenSum(int sum)
    {
        List<Tree<int>> currentPath = new List<Tree<int>>();
        List<List<Tree<int>>> allPaths = new List<List<Tree<int>>>();

        Tree<int> rootNode = GetRootNode();
        currentPath.Add(rootNode);

        CalculatePathsWithGivenSum(sum, rootNode, currentPath, allPaths);

        return allPaths;
    }

    private static int GetTreeSum(Tree<int> currentTree)
    {
        int sum = currentTree.Value;

        foreach (var child in currentTree.Children)
        {
            sum += GetTreeSum(child);
        }

        return sum;
    }

    private static List<Tree<int>> GetTreeInPreOrder(Tree<int> rootNode, List<Tree<int>> resultList)
    {
        resultList.Add(rootNode);

        foreach (var child in rootNode.Children)
        {
            GetTreeInPreOrder(child, resultList);
        }

        return resultList;
    }

    private static void CalculateSubtreeSums(int sum, Tree<int> currentTree, List<List<Tree<int>>> allTrees)
    {
        if (GetTreeSum(currentTree) == sum)
        {
            allTrees.Add(GetTreeInPreOrder(currentTree, new List<Tree<int>>()));
        }

        foreach (var child in currentTree.Children)
        {
            CalculateSubtreeSums(sum, child, allTrees);
        }
    }

    private static List<List<Tree<int>>> GetSubTreesWithGivenSum(int sum)
    {
        List<List<Tree<int>>> allTrees = new List<List<Tree<int>>>();

        CalculateSubtreeSums(sum, GetRootNode(), allTrees);

        return allTrees;
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
