using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedStack<T>
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

    public int Count { get; private set; }

    public void Push(T item)
    {
        if (this.Count == 0)
        {
            this.Head = new Node(item);
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

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("LinkedStack is empty");
        }
        else if (this.Count == 1)
        {
            this.Count = 0;
            var returned = this.Head;
            this.Head = null;
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

    public T[] ToArray()
    {
        var typeArr = new T[this.Count];
        var currentNode = this.Head;
        int i = 0;
        while (currentNode != null)
        {
            typeArr[i] = currentNode.Value;
            i++;
            currentNode = currentNode.Next;
        }
        return typeArr;
    }

}

