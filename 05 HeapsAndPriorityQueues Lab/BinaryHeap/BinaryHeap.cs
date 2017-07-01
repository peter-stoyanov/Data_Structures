using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.Count - 1);
    }

    private void HeapifyUp(int current)
    {
        int parent = GetParent(current);
        while (current > 0 && IsGreater(parent, current))
        {
            this.Swap(current, parent);
            current = GetParent(current);
            parent = GetParent(current);
        }
    }

    private static int GetParent(int current)
    {
        return (current - 1) / 2;
    }

    private void Swap(int current, int index)
    {
        T element = this.heap[current];
        this.heap[current] = this.heap[index];
        this.heap[index] = element;
    }

    private bool IsGreater(int parent, int current)
    {
        return this.heap[parent].CompareTo(this.heap[current]) < 0;
    }

    public T Peek()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T item = this.heap[0];
        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown(0);
        return item;
    }

    private void HeapifyDown(int current)
    {
        while (current < this.Count / 2)
        {
            int child = 2 * current + 1;
            if (child + 1 < this.Count && IsGreater(child, child+1))
            {
                child++;
            }
            if (IsGreater(child, current))
            {
                break;
            }
            Swap(child, current);
            current = child;
        }
    }
}
