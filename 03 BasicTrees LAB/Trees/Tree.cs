using System;
using System.Collections.Generic;

public class Tree<T>
{
    private T value;
    private List<Tree<T>> children;

    public Tree(T value, params Tree<T>[] children)
    {
        this.value = value;
        this.children = new List<Tree<T>>(children);
    }

    public T Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }

    public void Print(int indent = 0)
    {
        //print the root value at bigger indent each time
        Console.WriteLine(new string (' ', indent) + this.Value);

        foreach (var tree in this.children)
        {
            tree.Print(indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);
        foreach (var child in this.children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        //return the node values in the order of accessing by he DFS algo
        var result = new List<T>();

        this.DFS(this, result);

        return result;
    }

    private void DFS(Tree<T> tree, List<T> result)
    {
        foreach (var child in tree.children)
        {
            this.DFS(child, result);
        }
        result.Add(tree.value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();
        var queue = new Queue<Tree<T>>();

        queue.Enqueue(this);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current.value);
            foreach (var child in current.children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}
