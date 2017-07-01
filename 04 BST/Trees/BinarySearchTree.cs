using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node(T value)
        {
            this.Value = value;
        }
    }

    private Node root;

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node root)
    {
        this.Copy(root);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T insertValue)
    {
        if (this.root == null)
        {
            this.root = new Node(insertValue);
            return;
        }

        Node parent = null;
        Node current = this.root;
        while (current != null)
        {
            if (insertValue.CompareTo(current.Value) < 0)
            {

                parent = current;
                current = current.Left;
            }
            else if (insertValue.CompareTo(current.Value) > 0)
            {
                parent = current;
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        Node newNode = new Node(insertValue);
        if (insertValue.CompareTo(parent.Value) < 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }
    }


    public bool Contains(T searchValue)
    {
        Node current = this.root;
        while (current != null)
        {
            if (searchValue.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (searchValue.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        Node current = this.root;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }
        if (parent != null)
        {
            if (current.Right != null)
            {
                parent.Left = current.Right;
            }
            else
            {
                parent.Left = null;
            }

        }
        else
        {
            this.root = null;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;
        while (current != null)
        {
            if (item.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (item.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> range = new Queue<T>();

        this.Range(startRange, endRange, range, this.root);

        return range;
    }

    private void Range(T startRange, T endRange, Queue<T> range, Node node)
    {
        if (node == null)
        {
            return;
        }
        int compareLow = startRange.CompareTo(node.Value); //-1 when in range
        int compareHigh = endRange.CompareTo(node.Value); // +1 when in range

        if (compareLow < 0) // not in range
        {
            this.Range(startRange, endRange, range, node.Left);
        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            range.Enqueue(node.Value);
        }

        if (compareHigh > 0)
        {
            this.Range(startRange, endRange, range, node.Right);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        //recursion break condition
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

}

public class Launcher
{
    public static void Main(string[] args)
    {
        //// Arrange
        //BinarySearchTree<int> bst = new BinarySearchTree<int>();
        //bst.Insert(2);
        //bst.Insert(1);
        //bst.Insert(3);

        //// Act
        //bool contains = bst.Contains(1);

        // Arrange
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(1);

        // Act
        bst.DeleteMin();
        List<int> nodes = new List<int>();
        bst.EachInOrder(nodes.Add);

        // Assert
        int[] expectedNodes = new int[] { };
        //CollectionAssert.AreEqual(expectedNodes, nodes);

    }
}
