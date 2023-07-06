using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Unity.VisualScripting;

public class BoardManager : MonoBehaviourSingleton<BoardManager>
{
    [SerializeField] private ItemSpriteStorage _itemSpriteStorage;
    [SerializeField] private Transform _container;
    private Node[,] _board;
    private List<int> _values;
    private int _totalRows = 10, _totalCols = 7;
    private float _startX = 0, _startY = 0;
    private Item[,] _boardUI;
    private Node _startNode, _endNode;
    Matrix _matrix;
    List<Graph> _graphes = new List<Graph>();

    public int TotalItems => (_totalRows - 2) * (_totalCols - 2);

    public List<Node> GetNodesById(int id)
    {
        List<Node> nodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] == null) continue;
                if (_board[row, col].Val != id) continue;
                nodes.Add(_board[row, col]);
            }
        }
        return nodes;
    }

    public List<Vector2> GetPathFrom(Node nodeA, Node nodeB)
    {
        return _matrix.GetPath(new Point(nodeA.X, nodeA.Y), new Point(nodeB.X, nodeB.Y));
    }

    public void CreateBoard(LevelConfig config)
    {
        Clear();
        _totalRows = config.Row + 2;//border null
        _totalCols = config.Col + 2;//border null
        SetListValues();
        _values.Shuffle();
        SpawnItems(_values);
        this.SetMatrix();
        this.SetGraphs();
        if (CheckExistCouple()) return;
        CreateBoard(config);
    }

    private void SetListValues()
    {
        _values = new List<int>();
        for (int i = 0; i < TotalItems / 2; i++)
        {
            int val = UnityEngine.Random.Range(0, _itemSpriteStorage.Sprites.Count);
            _values.Add(val);
            _values.Add(val);
        }
    }

    private void SpawnItems(List<int> values)
    {
        _board = new Node[_totalRows, _totalCols];
        _boardUI = new Item[_totalRows, _totalCols];
        int id = 0; //index
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (col == 0 || col == _totalCols - 1 || row == 0 || row == _totalRows - 1)
                {
                    _board[row, col] = null;
                    _boardUI[row, col] = null;
                }
                else
                {
                    int val = values[id];
                    Vector3 position = new Vector3(_startX + col, _startY - row, 0.0f);
                    Item item = ItemSpawner.Instance.GetItemSpawned(position);
                    item.Init(row, col, _itemSpriteStorage.Sprites[val], val);
                    _board[row, col] = new Node(row, col, val, position);
                    _boardUI[row, col] = item;
                    id += 1;
                }
            }
        }
    }

    public void Clear()
    {
        if (_boardUI != null)
            ItemSpawner.Instance.ClearItems();
        _boardUI = null;
        _board = null;
        _values = null;
        _graphes = null;
        _matrix = null;
        _startNode = null;
        _endNode = null;
    }

    private void SetStartNode(int row, int col)
    {
        _startNode = _board[row, col];
    }

    private void SetEndNode(int row, int col)
    {
        _endNode = _board[row, col];
    }

    private void EndNodeEqualStartNode()
    {
        _boardUI[_startNode.X, _startNode.Y]?.UnSelect();
        _boardUI[_endNode.X, _endNode.Y]?.UnSelect();
        _startNode = null;
        _endNode = null;
    }

    private Graph GetGraphById(int id)
    {
        foreach (var graph in _graphes)
            if (graph.Id == id)
                return graph;
        return null;
    }

    private void SetMatrix()
    {
        int[,] matrix = new int[_totalRows, _totalCols];
        for (int i = 0; i < _totalRows; i++)
            for (int j = 0; j < _totalCols; j++)
                matrix[i, j] = _board[i, j] == null ? -1 : _board[i, j].Val;
        _matrix = new Matrix(matrix, _totalRows, _totalCols);
    }

    private void SetGraphs()
    {
        _graphes = new List<Graph>();
        foreach (var id in _values)
            _graphes.Add(new Graph(id));
    }

    private void CoupleSuccess()
    {
        _board[_startNode.X, _startNode.Y] = null;
        _board[_endNode.X, _endNode.Y] = null;
        _boardUI[_startNode.X, _startNode.Y].gameObject.SetActive(false);
        _boardUI[_endNode.X, _endNode.Y].gameObject.SetActive(false);
        _boardUI[_startNode.X, _startNode.Y] = null;
        _boardUI[_endNode.X, _endNode.Y] = null;
        _matrix.CheckSuccess(
            new Point(_startNode.X, _startNode.Y),
            new Point(_endNode.X, _endNode.Y)
        );
        _values.Remove(_startNode.Val);
        _values.Remove(_endNode.Val);
    }

    private void CoupleFail()
    {
        _boardUI[_startNode.X, _startNode.Y]?.UnSelect();
        _boardUI[_endNode.X, _endNode.Y]?.UnSelect();
    }

    private void UnSelectUIAll()
    {
        for (int row = 0; row < _totalRows; row++)
            for (int col = 0; col < _totalCols; col++)
                _boardUI[row, col]?.UnSelect();
    }

    private void CompletedConnection()
    {
        _startNode = null;
        _endNode = null;
        DOVirtual.DelayedCall(.1f, LineSpawner.Instance.ClearLines);
        GameManager.Instance.CompletedCheckConnection();
        if (_values.Count > 0) return;
        CompletedLevel();
    }

    private void EndNodeDifStartNode()
    {
        GameManager.Instance.CheckingConnection();
        Graph graph = GetGraphById(_startNode.Val);
        var points = GetPathFrom(_startNode, _endNode);
        if (points != null)
        {
            LineSpawner.Instance.Concatenate(points);
            CoupleSuccess();
            graph.RemovePathFrom(_startNode, _endNode);
            if (!this.CheckExistCouple())
                this.Remap();
            else
            {
                this.SetMatrix();
                this.SetGraphs();
                GameManager.Instance.CompletedRemap();
            }
        }
        else
            this.CoupleFail();
        this.CompletedConnection();
    }

    private void CompletedLevel()
    {
        Debug.Log("Completed Level");
    }

    public void SelectNode(int row, int col)
    {
        if (_startNode == null)
        {
            SetStartNode(row, col);
            return;
        }
        else
            SetEndNode(row, col);
        if (_endNode == _startNode)
            EndNodeEqualStartNode();
        else
            EndNodeDifStartNode();
    }

    private bool CheckExistCouple()
    {
        foreach (var graph in _graphes)
            if (graph.IsExistCouple()) return true;
        return false;
    }

    private void SetNodeSwap()
    {
        List<Node> nodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
            for (int col = 0; col < _totalCols; col++)
                if(_board[row, col] != null)
                    nodes.Add(_board[row, col]);
        Swap(nodes);
    }

    private void Swap(List<Node> nodes)
    {
        if(nodes.Count == 0)
        {
            return;
        }
        Node node = nodes[0];
        Node nodeSwap = nodes[UnityEngine.Random.Range(0, nodes.Count)];
        nodes.Remove(nodeSwap);
        node.SetNodeSwap(nodeSwap);
        nodeSwap.SetNodeSwap(node);
        //swap val
        int t = node.Val;
        node.ChangeVal(nodeSwap.Val);
        nodeSwap.ChangeVal(t);
        //
        nodes.RemoveAt(0);
        _boardUI[node.X, node.Y].transform.position = nodeSwap.Pos;
        _boardUI[nodeSwap.X, nodeSwap.Y].transform.position = node.Pos;
        Item item = _boardUI[node.X, node.Y];
        _boardUI[node.X, node.Y] = _boardUI[nodeSwap.X, nodeSwap.Y];
        _boardUI[nodeSwap.X, nodeSwap.Y] = item;
        _boardUI[node.X, node.Y].SetCoordinate(node.X, node.Y);
        _boardUI[nodeSwap.X, nodeSwap.Y].SetCoordinate(nodeSwap.X, nodeSwap.Y);
        Swap(nodes);
    }

    public void Remap()
    {
        UnSelectUIAll();
        CompletedConnection();
        GameManager.Instance.Remap();
        this.SetNodeSwap();
        this.SetMatrix();
        this.SetGraphs();
        if (this.CheckExistCouple())
        {
            GameManager.Instance.CompletedRemap();
            return;
        }
        Remap();
    }
}
