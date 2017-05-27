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

    public Node head { get; private set; }
    public Node tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node(item);
            this.Count++;
        }
        else
        {
            var newHead = new Node(item);
            newHead.Next = this.head;
            this.head = newHead;
            this.Count++;
        }
    }

    public void AddLast(T item)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node(item);
            this.Count++;
        }
        else
        {
            var oldTail = this.tail;
            this.tail = new Node(item);
            oldTail.Next = this.tail;
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
            var returned = this.head;
            this.head = this.tail = null;
            return this.head.Value;
        }
        else
        {
            var returned = this.head;
            this.head = this.head.Next;
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
            var returned = this.head;
            this.head = this.tail = null;
            return this.head.Value;
        }
        else
        {
            var returned = this.tail;
            this.tail = this.GetSecondToLast();
            this.tail.Next = null;
            this.Count--;
            return returned.Value;
        }
    }

    private Node GetSecondToLast()
    {
        var currentNode = this.head;
        Node secondToLastNode = null;
        while (currentNode != null)
        {
            if (currentNode.Next == this.tail)
            {
                secondToLastNode = currentNode;
            }
            currentNode = currentNode.Next;
        }
        return secondToLastNode;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.head;
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

