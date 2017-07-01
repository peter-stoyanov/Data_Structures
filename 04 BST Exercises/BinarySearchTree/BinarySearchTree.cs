using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T:IComparable
{
    #region Nested class - data structure
    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; } //why no setter
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Count { get; set; }
    }
    #endregion

    #region Fields
    //defined by a single root element 
    private Node root;
    #endregion

    #region Constructors
    //ctor
    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }
    //ctor
    public BinarySearchTree()
    {
    }
    #endregion

    #region Find/Contains
    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }
    #endregion

    #region Insert
    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    private Node Insert(T element, Node node)
    {
        //node NULL means an epty space to put the new node
        if (node == null)
        {
            node = new Node(element);
            node.Count = 1;
            
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        //sort out Count prop
        if (node.Left != null && node.Right != null)
        {
            node.Count = node.Left.Count + node.Right.Count + 1;

        }
        else if (node.Left != null)
        {
            node.Count = node.Left.Count + 1;
        }
        else if(node.Right != null)
        {
            node.Count = node.Right.Count + 1;
        }

        return node;
    }
    #endregion

    #region Count
    public int Count()
    {
        return this.Count(this,root);
    }

    private int Count(BinarySearchTree<T> binarySearchTree, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }
    #endregion

    #region PreOrderCopy
    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }
    #endregion

    #region Traverse with Action<>
    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
    #endregion

    #region Range
    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }
    #endregion

    #region Search
    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }
    #endregion

    #region Delete
    public void Delete(T element)
    {
        throw new NotImplementedException();
    }

    public void DeleteMin()
    {
        //if (this.root == null)
        //{
        //    throw new InvalidOperationException();
        //}

        //Node current = this.root;
        //Node parent = null;
        //while (current.Left != null)
        //{
        //    parent = current;
        //    current = current.Left;
        //}

        //if (parent == null)
        //{
        //    this.root = this.root.Right;
        //}
        //else
        //{
        //    parent.Left = current.Right;
        //}

        //change to recursive
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {
        //base of the recursion
        if (node.Left == null)
        {
            return node.Right;
        }

        //on every level return connection to the next node
        node.Left = this.DeleteMin(node.Left); // recursive call

        //on the way back
        node.Count--;
        return node;
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node current = this.root;
        Node parent = null;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        //if only one node in the tree
        if (parent == null)
        {
            //this.root = this.root.Right;
            this.root = null;
        }
        else
        {
            parent.Right = current.Left;
        }
    }
    #endregion

    public int Rank(T element)
    {
        return this.Rank(this.root, element);
    }

    private int Rank(Node node, T element)
    {
        if (node == null)
        {
            return 0;
        }

        if (node.Value.CompareTo(element) == 0)
        {
            
        }
    }

    public T Select(int rank)
    {
        throw new NotImplementedException();
    }

    public T Ceiling(T element)
    {
        throw new NotImplementedException();
    }

    public T Floor(T element)
    {
        throw new NotImplementedException();
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(1);
        bst.Insert(3);
        bst.Insert(5);
        bst.Insert(10);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        //bool x = bst.Contains(37);

        bst.EachInOrder(Console.Write);
        Console.WriteLine();
        bst.DeleteMax();
        //bst.EachInOrder(Console.Write);
        Console.WriteLine(bst.Count());
        bst.DeleteMin();
        //bst.EachInOrder(Console.Write);
        Console.WriteLine(bst.Count());


    }
}