using System;

public class ArrayList<T>
{
    private T[] _contentArr; //stores internally the items
    private int _pointer; //shows the next empty place for inserting new elements
    private const int _initialCapacity = 2;

    public ArrayList()
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

    public T this[int index]
    {
        get
        {
            if (index >= 0 && index < this._pointer)
            {
                return this._contentArr[index];
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
                this._contentArr[index] = value;
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
        if (index >= 0 && index <= this._pointer)
        {
            var returned = this._contentArr[index];

            for (int i = 0; i < this._pointer; i++)
            {
                if (i >= index)
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
}
