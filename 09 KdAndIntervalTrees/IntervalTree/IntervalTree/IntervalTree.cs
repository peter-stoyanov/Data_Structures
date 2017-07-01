using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        var x = this.root;
        while (x != null && !x.interval.Intersects(lo, hi))
        {
            if (x.left != null && x.left.max > lo)
            {
                x = x.left;
            }
            else
            {
                x = x.right;
            }

        }
        if (x == null)
        {
            return null;
        }
        return x.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        List<Interval> list = new List<Interval>();
        SearchAll(list, this.root, lo, hi);
        return list;
    }

    private void SearchAll(List<Interval> list, Node root, double lo, double hi)
    {
        if (root == null)
        {
            return;
        }
        var goLeft = root.left != null && root.left.interval.Lo < hi;
        var goRight = root.right != null && root.right.interval.Lo < hi;

        if (goLeft)
        {
            SearchAll(list, root.left, lo, hi);
        }
        if (root.interval.Intersects(lo, hi))
        {
            list.Add(root.interval);
        }
        if (goRight)
        {
            SearchAll(list, root.right, lo, hi);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }
        UpdateMax(node);
        return node;
    }

    private void UpdateMax(Node node)
    {
        var maxChild = GetMax(node.left, node.right);
        node.max = GetMax(node, maxChild).max;
    }

    private Node GetMax(Node left, Node right)
    {
        if (left == null)
        {
            return right;
        }
        if (right == null)
        {
            return left;
        }
        return left.max > right.max ? left : right;
    }
}
