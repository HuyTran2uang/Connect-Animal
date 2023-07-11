using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BoardManager : MonoBehaviourSingleton<BoardManager>
{
    [SerializeField] private ItemSpriteStorage _itemSpriteStorage;
    private Node[,] _board;
    private List<int> _values;
    private int _totalRows = 10, _totalCols = 7;
    private float _startX = 0, _startY = 0;
    private Item[,] _boardUI;
    private Node _startNode, _endNode;
    Matrix _matrix;
    List<Graph> _graphes = new List<Graph>();
    SpecialType _specialType;

    public int TotalItems => (_totalRows - 2) * (_totalCols - 2);
    public Node[,] Board => _board;
    public Item[,] BoardUI => _boardUI;

    private void SetPosCam()
    {
        float minX = 9, maxX = 0, minY = 9, maxY = 0;
        float offsetY = -1;
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] == null) continue;
                if (minX > _board[row, col].Pos.x) minX = _board[row, col].Pos.x;
                if(maxX < _board[row, col].Pos.x) maxX = _board[row, col].Pos.x;
                if(minY > _board[row, col].Pos.y) minY = _board[row, col].Pos.y;
                if(maxY < _board[row, col].Pos.y) maxY = _board[row, col].Pos.y;
            }
        }
        Camera.main.transform.position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2 + offsetY, -10);
    }

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
        SetPosCam();
        if (CheckExistCouple()) return;
        CreateBoard(config);
    }

    private void SetListValues()
    {
        _values = new List<int>();
        for (int i = 0; i < TotalItems / 2 - 1; i++)
        {
            int val = UnityEngine.Random.Range(1, _itemSpriteStorage.Sprites.Count);
            _values.Add(val);
            _values.Add(val);
        }
        if (LevelManager.Instance.Level < 12)
        {
            if (LevelManager.Instance.Level % 4 == 0)
            {
                _values.Add(0);
                _values.Add(0);
                _specialType = SpecialSpawner.Instance.GetRandomSpecialType();
            }
        }
        else
        {
            if (LevelManager.Instance.Level % 3 == 1)
            {
                _values.Add(0);
                _values.Add(0);
                _specialType = SpecialSpawner.Instance.GetRandomSpecialType();
            }
            else
            {
                int val = UnityEngine.Random.Range(1, _itemSpriteStorage.Sprites.Count);
                _values.Add(val);
                _values.Add(val);
            }
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
                    if (val == 0)
                    {
                        SpecialItem item = ItemSpawner.Instance.GetSpecialItemSpawned(position, _specialType);
                        item.Init(row, col, val);
                        _boardUI[row, col] = item;
                    }
                    else
                    {
                        Item item = ItemSpawner.Instance.GetItemSpawned(position);
                        item.Init(row, col, _itemSpriteStorage.Sprites[val], val);
                        _boardUI[row, col] = item;
                    }
                    _board[row, col] = new Node(row, col, val, position);
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

    private Node GetRandomNodeFromId(int id)
    {
        List<Node> nodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] != null && _board[row, col].Val == id)
                    nodes.Add(_board[row, col]);
            }
        }
        if (nodes.Count == 0) return null;
        return nodes[UnityEngine.Random.Range(0, nodes.Count)];
    }

    private void RemoveAndFindToRemove(int row, int col)
    {
        if (_board[row, col] == null) return;
        int id = _board[row, col].Val;
        ExplodeAndRemoveItem(row, col);
        Node nodeCoupled = GetRandomNodeFromId(id);
        ExplodeAndRemoveItem(nodeCoupled.X, nodeCoupled.Y);
    }

    private void ExecuteSpecial(SpecialType specialType)
    {
        switch (specialType)
        {
            case SpecialType.Rocket:
                RocketSpawner.Instance.ShootRocket(_startNode.Pos, _endNode.Pos);
                break;
            case SpecialType.Bomb:
                RemoveAndFindToRemove(_startNode.X + 1, _startNode.Y);
                RemoveAndFindToRemove(_startNode.X - 1, _startNode.Y);
                RemoveAndFindToRemove(_startNode.X, _startNode.Y + 1);
                RemoveAndFindToRemove(_startNode.X, _startNode.Y - 1);

                RemoveAndFindToRemove(_endNode.X + 1, _endNode.Y);
                RemoveAndFindToRemove(_endNode.X - 1, _endNode.Y);
                RemoveAndFindToRemove(_endNode.X, _endNode.Y + 1);
                RemoveAndFindToRemove(_endNode.X, _endNode.Y - 1);
                break;
            case SpecialType.Lightning:
                AudioManager.Instance.PlaySoundExplosion();
                FXSpawner.Instance.LightningStrikeFX(_startNode.Pos);
                FXSpawner.Instance.LightningStrikeFX(_endNode.Pos);
                if (_values.Count == 0) return;
                RemoveRandomCouple();
                //if (_values.Count == 0) return;
                //RemoveRandomCouple();
                //if (_values.Count == 0) return;
                //RemoveRandomCouple();
                //if (_values.Count == 0) return;
                //RemoveRandomCouple();
                break;
        }
    }

    private void CoupleSuccess()
    {
        AudioManager.Instance.PlaySoundConnectButton();
        RemoveItem(_startNode.X, _startNode.Y);
        RemoveItem(_endNode.X, _endNode.Y);
        if (_startNode.Val == 0)
            ExecuteSpecial(_specialType);
    }

    private void CoupleFail()
    {
        AudioManager.Instance.PlaySoundConnectFailButton();
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
        HintManager.Instance.UnHint();
        GameManager.Instance.Wait();
        if (!CheckCompletedMap()) return;
        CompletedLevel();
    }

    private void EndNodeDifStartNode()
    {
        GameManager.Instance.Wait();
        Graph graph = GetGraphById(_startNode.Val);
        var points = GetPathFrom(_startNode, _endNode);
        if (points != null)
        {
            LineSpawner.Instance.Concatenate(points);
            CoupleSuccess();
            graph.RemovePathFrom(_startNode, _endNode);
            if (_values.Count > 0)
            {
                this.SetMatrix();
                this.SetGraphs();
            }
            if (!this.CheckExistCouple())
            {
                Debug.Log("No Exist Couple");
                this.Remap();
            }
        }
        else
            this.CoupleFail();
        this.CompletedConnection();
        GameManager.Instance.ResumeGame();
    }

    private void CompletedLevel()
    {
        GameManager.Instance.Win();
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
        Debug.Log("Remap");
        UnSelectUIAll();
        CompletedConnection();
        GameManager.Instance.Wait();
        this.SetNodeSwap();
        this.SetMatrix();
        this.SetGraphs();
        if (this.CheckExistCouple())
        {
            GameManager.Instance.ResumeGame();
            return;
        }
        Remap();
    }

    public Graph GetGraphFirst()
    {
        return _graphes[0];
    }

    public Couple GetRandomCouple()
    {
        int id = _values[UnityEngine.Random.Range(0, _values.Count)];
        List<Node> _sameNodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] != null && _board[row, col].Val == id)
                _sameNodes.Add(_board[row, col]);
            }
        }
        if (_sameNodes.Count == 0)
            return null;
        Node node1 = _sameNodes[UnityEngine.Random.Range(0, _sameNodes.Count)];
        _sameNodes.Remove(node1);
        Node node2 = _sameNodes[UnityEngine.Random.Range(0, _sameNodes.Count)];
        Couple couple = new Couple(
            new Vector2Int(node1.X, node1.Y),
            new Vector2Int(node2.X, node2.Y)
        );
        return couple;
    }

    public void RemoveItem(int row, int col)
    {
        _boardUI[row, col].gameObject.SetActive(false);
        _values.Remove(_board[row, col].Val);
        _board[row, col] = null;
        _boardUI[row, col] = null;
    }

    public void ExplodeAndRemoveItem(int row, int col)
    {
        AudioManager.Instance.PlaySoundExplosion();
        FXSpawner.Instance.ExplodeFX(_board[row, col].Pos);
        StarSpawner.Instance.StarSpawned(_board[row, col].Pos);
        RemoveItem(row, col);
    }

    public void ResetDataMap()
    {
        GameManager.Instance.Wait();
        this.SetMatrix();
        this.SetGraphs();
        GameManager.Instance.ResumeGame();
    }

    public bool CheckCompletedMap()
    {
        return _values.Count == 0;
    }

    public void RemoveRandomCouple()
    {
        Couple randCouple = this.GetRandomCouple();
        StarSpawner.Instance.StarSpawned(_board[randCouple.Coord1.x, randCouple.Coord1.y].Pos);
        StarSpawner.Instance.StarSpawned(_board[randCouple.Coord2.x, randCouple.Coord2.y].Pos);
        RemoveItem(randCouple.Coord1.x, randCouple.Coord1.y);
        RemoveItem(randCouple.Coord2.x, randCouple.Coord2.y);
        ResetDataMap();
    }
}
