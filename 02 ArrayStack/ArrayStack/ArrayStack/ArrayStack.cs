using System;

public class ArrayStack<T>
{
    private const int DefaultCapacity = 16;

    private T[] elements;
    private int inserIndex = 0;

    public int Count { get; private set; }

    public ArrayStack(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Push(T element)
    {
        if (this.inserIndex == this.elements.Length - 1)
        {
            this.Grow();
        }

        this.elements[inserIndex] = element;
        this.inserIndex++;
        this.Count++;
    }

    //grows the underlying array
    private void Grow()
    {
        var newElements = new T[this.elements.Length * 2];
        this.elements.CopyTo(newElements, 0);
        this.elements = newElements;
        this.inserIndex = this.Count;
    }

    private void Resize()
    {
        // TODO
        throw new NotImplementedException();
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The stack is empty");
        }

        var returned = this.elements[inserIndex - 1];
        this.Count--;
        this.inserIndex--;
        return returned;
    }

    public T[] ToArray()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The stack is empty");
        }
        var returnedArr = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            returnedArr[i] = this.elements[i];
        }
        return returnedArr;
    }
}


public class Example
{
    public static void Main()
    {
        var stack = new ArrayStack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);
        stack.Push(6);

        while (stack.Count != 0)
        {
            Console.WriteLine(stack.Pop());
        }
    }
}
