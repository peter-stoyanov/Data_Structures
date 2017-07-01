using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class ListNode<T>
    {
        public T Value { get; private set; }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }
    }

    private ListNode<T> head;
    private ListNode<T> tail;

    public int Count { get; private set; }

    private bool IsEmpty()
    {
        return this.Count == 0;
    }

    public void AddFirst(T element)
    {
        if (IsEmpty())
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            //create new node
            var newHead = new ListNode<T>(element);
            //connect to old head
            newHead.NextNode = this.head;
            //old head now has prev node
            this.head.PrevNode = newHead;
            //ref. to head is changed to new node
            this.head = newHead;
        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        if (IsEmpty())
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newTail = new ListNode<T>(element);
            newTail.PrevNode = this.tail;
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is empty");
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

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("List is empty");
        }
        else if (this.Count == 1)
        {
            var returned = this.tail;
            this.head = null;
            this.tail = null;
            this.Count = 0;
            return returned.Value;
        }
        else
        {
            var returned = this.tail;
            this.tail = this.tail.PrevNode;
            this.tail.NextNode = null;
            this.Count--;
            return returned.Value;
        }
    }

    public void ForEach(Action<T> action) //visitor paterrn
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.NextNode;
        }
    }

    //non-generic enumerator:
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
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


class Example
{
    static void Main()
    {

        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
