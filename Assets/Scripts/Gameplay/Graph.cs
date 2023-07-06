using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private int _id;
    private List<Node> _nodes = new List<Node>();
    private Dictionary<string, List<Vector2>> _pathDict = new Dictionary<string, List<Vector2>>();

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
                if (_pathDict.ContainsKey(key)) continue;
                List<Vector2> points = BoardManager.Instance.GetPathFrom(_nodes[j], _nodes[i]);
                if (points == null || points.Count == 0) continue;
                _pathDict.Add(key, points);
            }
        }
    }

    public int Id => _id;

    public List<Vector2> GetPathFrom(Node nodeA, Node nodeB)
    {
        string key = $"[{nodeA.X},{nodeA.Y}][{nodeB.X},{nodeB.Y}]";
        return _pathDict.ContainsKey(key) ? _pathDict[key] : null;
    }

    public void RemovePathFrom(Node nodeA, Node nodeB)
    {
        string key = $"[{nodeA.X},{nodeA.Y}][{nodeB.X},{nodeB.Y}]";
        _pathDict.Remove(key);
        string keyReverse = $"[{nodeB.X},{nodeB.Y}][{nodeA.X},{nodeA.Y}]";
        _pathDict.Remove(keyReverse);
    }

    public bool IsExistCouple() => _pathDict.Keys.Count > 0;
}
