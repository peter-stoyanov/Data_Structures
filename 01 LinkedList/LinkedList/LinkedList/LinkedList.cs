using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            this.Value = value;
        }
    }

    public Node Head { get; private set; }
    public Node Tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        if (this.Count == 0)
        {
            this.Head = this.Tail = new Node(item);
            this.Count++;
        }
        else
        {
            var newHead = new Node(item);
            newHead.Next = this.Head;
            this.Head = newHead;
            this.Count++;
        }
    }

    public void AddLast(T item)
    {
        if (this.Count == 0)
        {
            this.Head = this.Tail = new Node(item);
            this.Count++;
        }
        else
        {
            var oldTail = this.Tail;
            this.Tail = new Node(item);
            oldTail.Next = this.Tail;
            this.Count++;
        }
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }
        else if (this.Count == 1)
        {
            this.Count = 0;
            var returned = this.Head;
            this.Head = this.Tail = null;
            return this.Head.Value;
        }
        else
        {
            var returned = this.Head;
            this.Head = this.Head.Next;
            this.Count--;
            return returned.Value;
        }
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }
        else if (this.Count == 1)
        {
            this.Count = 0;
            var returned = this.Head;
            this.Head = this.Tail = null;
            return this.Head.Value;
        }
        else
        {
            var returned = this.Tail;
            this.Tail = this.GetSecondToLast();
            this.Tail.Next = null;
            this.Count--;
            return returned.Value;
        }
    }

    private Node GetSecondToLast()
    {
        var currentNode = this.Head;
        Node secondToLastNode = null;
        while (currentNode != null)
        {
            if (currentNode.Next == this.Tail)
            {
                secondToLastNode = currentNode;
            }
            currentNode = currentNode.Next;
        }
        return secondToLastNode;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.Head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

