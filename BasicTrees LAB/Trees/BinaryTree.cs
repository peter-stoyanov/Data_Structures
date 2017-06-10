using System;

public class BinaryTree<T>
{
    private T value;

    public BinaryTree<T> LeftChild { get; set; }
    public BinaryTree<T> RightChild { get; set; }

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        //pre-order: root->left->right
        Console.WriteLine(new string(' ', indent) + this.value);
        if (this.LeftChild != null) { this.LeftChild.PrintIndentedPreOrder(indent + 2); }
        if (this.RightChild != null) { this.RightChild.PrintIndentedPreOrder(indent + 2); }
    }

    public void EachInOrder(Action<T> action)
    {
        //in-order: left->root->right
        if (this.LeftChild != null) { this.LeftChild.EachInOrder(action); }
        action(this.value);
        if (this.RightChild != null) { this.RightChild.EachInOrder(action); }
    }

    public void EachPostOrder(Action<T> action)
    {
        //in-order: left->right->root
        if (this.LeftChild != null) { this.LeftChild.EachPostOrder(action); }
        if (this.RightChild != null) { this.RightChild.EachPostOrder(action); }
        action(this.value);
    }
}
