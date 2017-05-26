using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private T[] _contentArr; //stores internally the items
    private int _pointer; //shows the next empty place for inserting new elements
    private const int _initialCapacity = 2;

    public ReversedList()
    {
        this._contentArr = new T[_initialCapacity];
    }

    public int Count
    {
        get
        {
            return this._pointer;
        }

        private set { }
    }

    public int Capacity
    {
        get
        {
            return this._contentArr.Length;
        }

        private set { }
    }


    public T this[int index]
    {
        get
        {
            if (index >= 0 && index < this._pointer)
            {
                return this._contentArr[this._pointer - index - 1];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        set
        {
            if (index >= 0 && index < this._contentArr.Length)
            {
                this._contentArr[this._pointer - index - 1] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void Add(T item)
    {
        if (this._pointer == this._contentArr.Length - 1)
        {
            Resize();
        }
        this._contentArr[this._pointer] = item;
        this._pointer++;
    }

    private void Resize()
    {
        var newArr = new T[this._contentArr.Length * 2];
        this._contentArr.CopyTo(newArr, 0);
        this._contentArr = newArr;
        newArr = null;
    }

    public T RemoveAt(int index)
    {
        if (index >= 0 && index <= this._pointer - 1)
        {
            var returned = this._contentArr[this._pointer - index - 1];

            for (int i = 0; i < this._pointer; i++)
            {
                if (i >= this._pointer - index - 1)
                {
                    this._contentArr[i] = this._contentArr[i + 1];
                }
            }
            this._pointer--;
            return returned;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this._pointer; i++)
        {
            yield return this._contentArr[this._pointer - i - 1];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // Lets call the generic version here
        return this.GetEnumerator();
    }
}
