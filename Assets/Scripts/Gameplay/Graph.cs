using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    [SerializeField] private int _id;
    private List<Node> _nodes = new List<Node>();
    private Dictionary<string, string> _pathDict = new Dictionary<string, string>();

    public Graph(int id)
    {
        _id = id;
        _nodes = BoardManager.Instance.GetNodesById(_id);
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = 0; j < _nodes.Count; j++)
            {
                if (_nodes[j] == _nodes[i]) continue;
                string key = $"[{_nodes[j].X},{_nodes[j].Y}][{_nodes[i].X},{_nodes[i].Y}]";
                string path = BoardManager.Instance.GetPathFrom(_nodes[j], _nodes[i]);
                _pathDict.Add(key, path);
            }
        }
    }

    public string GetPathFrom(Node nodeA, Node nodeB)
    {
        string key = $"[{nodeA.X},{nodeA.Y}][{nodeB.X},{nodeB.Y}]";
        return _pathDict[key];
    }

    public void RemovePathFrom(Node nodeA, Node nodeB)
    {
        string key = $"[{nodeA.X},{nodeA.Y}][{nodeB.X},{nodeB.Y}]";
        _pathDict.Remove(key);
    }

    public bool IsUnCouple()
    {
        return _pathDict.Keys.Count > 0;
    }
}
