using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedQueue<T>
{
    private class QueueNode<T>
    {
        public T Value { get; private set; }

        public QueueNode<T> NextNode { get; set; }

        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value)
        {
            this.Value = value;
        }
    }
    
    private QueueNode<T> head;
    private QueueNode<T> tail;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new QueueNode<T>(element);
        }
        else
        {
            var newTail = new QueueNode<T>(element);
            newTail.PrevNode = this.tail;
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Linked Queue is empty");
        }
        else if (this.Count == 1)
        {
            var returned = this.head;
            this.head = null;
            this.tail = null;
            this.Count = 0;
            return returned.Value;
        }
        else
        {
            var returned = this.head;
            this.head = this.head.NextNode;
            this.head.PrevNode = null;
            this.Count--;
            return returned.Value;
        }
    }

    public T[] ToArray()
    {
        var typeArr = new T[this.Count];
        var currentNode = this.head;
        int i = 0;
        while (currentNode != null)
        {
            typeArr[i] = currentNode.Value;
            i++;
            currentNode = currentNode.NextNode;
        }
        return typeArr;
    }
}