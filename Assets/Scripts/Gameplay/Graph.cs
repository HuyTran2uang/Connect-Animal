using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    private int _id;
    private List<Node> _nodes = new List<Node>();
    private Dictionary<Couple, List<Vector2Int>> _pathDict = new Dictionary<Couple, List<Vector2Int>>();

    public Graph(int id)
    {
        _id = id;
        _nodes = BoardManager.Instance.GetNodesById(_id);
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = 0; j < _nodes.Count; j++)
            {
                if (_nodes[j] == _nodes[i]) continue;
                Couple key = new Couple(new Vector2Int(_nodes[j].x, _nodes[j].y), new Vector2Int(_nodes[i].x, _nodes[i].y));
                if (_pathDict.ContainsKey(key)) continue;
                List<Vector2Int> points = BoardManager.Instance.GetPathFrom(_nodes[j], _nodes[i]);
                if (points == null || points.Count == 0) continue;
                _pathDict.Add(key, points);
            }
        }
    }

    public int Id => _id;

    public List<Vector2Int> GetPathFrom(Node nodeA, Node nodeB)
    {
        Couple key = new Couple(new Vector2Int(nodeA.x, nodeA.y), new Vector2Int(nodeB.x, nodeB.y));
        return _pathDict.ContainsKey(key) ? _pathDict[key] : null;
    }

    public void RemovePathFrom(Node nodeA, Node nodeB)
    {
        Couple key = new Couple(new Vector2Int(nodeA.x, nodeA.y), new Vector2Int(nodeB.x, nodeB.y));
        _pathDict.Remove(key);
        Couple keyReverse = new Couple(new Vector2Int(nodeB.x, nodeB.y), new Vector2Int(nodeA.x, nodeA.y));
        _pathDict.Remove(keyReverse);
    }

    public bool IsExistCouple() => _pathDict.Keys.Count > 0;

    public Couple GetGraphKeyFirst()
    {
        if (_pathDict.Count == 0) return null;
        return _pathDict.First().Key;
    }
}

public class Couple
{
    private Vector2Int _coord1;
    private Vector2Int _coord2;

    public Couple(Vector2Int coord1, Vector2Int coord2)
    {
        _coord1 = coord1;
        _coord2 = coord2;
    }

    public Vector2Int Coord1 => _coord1;
    public Vector2Int Coord2 => _coord2;
}
