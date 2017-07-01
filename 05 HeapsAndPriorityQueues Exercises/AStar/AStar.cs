using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;
    public AStar(char[,] map)
    {
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);
        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var cost = new Dictionary<Node, int>();
        var parent = new Dictionary<Node, Node>();
        var queue = new PriorityQueue<Node>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            var neighbours = GetNeighbours(current);
            foreach (var neighbour in neighbours)
            {
                var newCost = cost[current] + 1;
                if (cost.ContainsKey(neighbour) || newCost < cost[neighbour])
                {
                    cost[neighbour] = newCost;
                    neighbour.F = newCost + GetH(neighbour, goal);
                    queue.Enqueue(neighbour);
                    parent[neighbour] = current;
                }
            }
        }
        return GetPath(parent, goal);
    }

    private IEnumerable<Node> GetNeighbours(Node current)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<Node> GetPath(Dictionary<Node, Node> parent, Node goal)
    {
        throw new NotImplementedException();
    }
}

