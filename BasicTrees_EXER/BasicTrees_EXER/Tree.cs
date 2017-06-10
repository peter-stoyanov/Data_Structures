using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tree<T>
{
    public T Value { get; set; }
    public List<Tree<T>> Children { get; set; }
    public Tree<T> Parent { get; set; }
    public int Depth { get; set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
        foreach (var child in children)
        {
            child.Parent = this;
        }
    }
}
