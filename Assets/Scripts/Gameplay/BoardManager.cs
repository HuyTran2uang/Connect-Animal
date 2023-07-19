using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BoardManager : MonoBehaviourSingleton<BoardManager>
{
    [SerializeField] private ItemSpriteStorage _itemSpriteStorage;
    private Node[,] _board;
    [SerializeField] private List<int> _ids;
    private int _totalRows = 10, _totalCols = 7;
    private float _startX = 0, _startY = 0;
    private Item[,] _boardUI;
    private Vector3[,] _posGrid;
    private Node _startNode, _endNode;
    Matrix _matrix;
    List<Graph> _graphes = new List<Graph>();
    SpecialType _specialType;

    public int TotalItems => (_totalRows - 2) * (_totalCols - 2);
    public Node[,] Board =>_board;
    public Item[,] BoardUI => _boardUI;

    public Vector3 GetPosFrom(int row, int col) => _posGrid[row, col];
    //movement
    int _iMove;
    List<int> _quantityWhenChangeMap = new List<int>() { 10, 14, 20, 28, 34, 40, 44, 50 };
    //

    private void SetPosCam()
    {
        float minX = 9, maxX = 0, minY = 9, maxY = 0;
        float offsetY = -1;
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] == null) continue;
                if (minX > _posGrid[row, col].x) minX = _posGrid[row, col].x;
                if(maxX < _posGrid[row, col].x) maxX = _posGrid[row, col].x;
                if(minY > _posGrid[row, col].y) minY = _posGrid[row, col].y;
                if(maxY < _posGrid[row, col].y) maxY = _posGrid[row, col].y;
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
                if (_board[row, col].id != id) continue;
                nodes.Add(_board[row, col]);
            }
        }
        return nodes;
    }

    public List<Vector2Int> GetPathFrom(Node nodeA, Node nodeB)
    {
        return _matrix.GetPath(new Point(nodeA.x, nodeA.y), new Point(nodeB.x, nodeB.y));
    }

    public void CreateBoard(LevelConfig config)
    {
        Clear();
        TimerManager.Instance.ResetAutoHintTimer();
        _quantityWhenChangeMap = new List<int>() { 10, 14, 20, 28, 34, 40, 44, 50 };
        _totalRows = config.TotalRows;//border null
        _totalCols = config.TotalCols;//border null
        SetListValues(config.TotalVals);
        _ids.Shuffle();
        SpawnItems(_ids, config.Grid);
        this.SetMatrix();
        this.SetGraphs();
        SetPosCam();
        if (CheckExistCouple()) return;
        CreateBoard(config);
    }

    private void SetListValues(int totalIds)
    {
        _ids = new List<int>();
        for (int i = 0; i < totalIds / 2 - 1; i++)
        {
            int val = UnityEngine.Random.Range(1, _itemSpriteStorage.Sprites.Count);
            _ids.Add(val);
            _ids.Add(val);
        }
        int lastNodeId = 0;
        if (LevelManager.Instance.Level < 10)
        {
            if (LevelManager.Instance.Level % 4 == 0)
                _specialType = SpecialSpawner.Instance.GetRandomSpecialType();
            else
                lastNodeId = UnityEngine.Random.Range(1, _itemSpriteStorage.Sprites.Count);
        }
        else
        {
            if (LevelManager.Instance.Level % 2 == 0)
                _specialType = SpecialSpawner.Instance.GetRandomSpecialType();
            else
                _specialType = SpecialSpawner.Instance.GetRandomBombAndLightning();
        }
        _ids.Add(lastNodeId);
        _ids.Add(lastNodeId);
    }

    private void SpawnItems(List<int> ids, int[,] grid)
    {
        _board = new Node[_totalRows, _totalCols];
        _boardUI = new Item[_totalRows, _totalCols];
        _posGrid = new Vector3[_totalRows, _totalCols];
        int randId = 0; //randIndex
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                Vector3 position = new Vector3(_startX + col, _startY - row, 0);
                _posGrid[row, col] = position;
                if (grid[row, col] == 0)
                {
                    _board[row, col] = null;
                    _boardUI[row, col] = null;
                }
                else
                {
                    int id = ids[randId];
                    if (id == 0)
                    {
                        SpecialItem item = ItemSpawner.Instance.GetSpecialItemSpawned(position, _specialType);
                        item.Init(row, col, id);
                        _boardUI[row, col] = item;
                    }
                    else
                    {
                        Item item = ItemSpawner.Instance.GetItemSpawned(position);
                        item.Init(row, col, _itemSpriteStorage.Sprites[id], id);
                        _boardUI[row, col] = item;
                    }
                    _board[row, col] = new Node(row, col, id);
                    randId += 1;
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
        _posGrid = null;
        _ids.Clear();
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
        _boardUI[_startNode.x, _startNode.y]?.UnSelect();
        _boardUI[_endNode.x, _endNode.y]?.UnSelect();
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
                matrix[i, j] = _board[i, j] == null ? -1 : _board[i, j].id;
        _matrix = new Matrix(matrix, _totalRows, _totalCols);
    }

    private void SetGraphs()
    {
        _graphes = new List<Graph>();
        foreach (var id in _ids)
            _graphes.Add(new Graph(id));
    }

    private Node GetRandomNodeFromId(int id)
    {
        List<Node> nodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] != null && _board[row, col].id == id)
                    nodes.Add(_board[row, col]);
            }
        }
        if (nodes.Count == 0) return null;
        return nodes[UnityEngine.Random.Range(0, nodes.Count)];
    }

    private void RemoveAndFindToRemove(int row, int col)
    {
        if (_board[row, col] == null) return;
        int id = _board[row, col].id;
        ExplodeAndRemoveItem(row, col);
        Node nodeCoupled = GetRandomNodeFromId(id);
        ExplodeAndRemoveItem(nodeCoupled.x, nodeCoupled.y);
    }

    private void ExecuteSpecial(SpecialType specialType)
    {
        switch (specialType)
        {
            case SpecialType.Rocket:
                if (_ids.Count == 0) return;
                RocketSpawner.Instance.ShootRocket(_posGrid[_startNode.x, _startNode.y], _posGrid[_endNode.x, _endNode.y]);
                break;
            case SpecialType.Bomb:
                RemoveAndFindToRemove(_startNode.x + 1, _startNode.y);
                RemoveAndFindToRemove(_startNode.x - 1, _startNode.y);
                RemoveAndFindToRemove(_startNode.x, _startNode.y + 1);
                RemoveAndFindToRemove(_startNode.x, _startNode.y - 1);

                RemoveAndFindToRemove(_endNode.x + 1, _endNode.y);
                RemoveAndFindToRemove(_endNode.x - 1, _endNode.y);
                RemoveAndFindToRemove(_endNode.x, _endNode.y + 1);
                RemoveAndFindToRemove(_endNode.x, _endNode.y - 1);
                break;
            case SpecialType.Lightning:
                AudioManager.Instance.PlaySoundExplosion();
                FXSpawner.Instance.LightningStrikeFX(_posGrid[_startNode.x, _startNode.y]);
                FXSpawner.Instance.LightningStrikeFX(_posGrid[_endNode.x, _endNode.y]);
                if (_ids.Count == 0) return;
                RemoveRandomCouple();
                if (_ids.Count == 0) return;
                RemoveRandomCouple();
                if (_ids.Count == 0) return;
                RemoveRandomCouple();
                if (_ids.Count == 0) return;
                RemoveRandomCouple();
                break;
        }
    }

    private void CoupleSuccess()
    {
        AudioManager.Instance.PlaySoundConnectButton();
        RemoveItem(_startNode.x, _startNode.y);
        RemoveItem(_endNode.x, _endNode.y);
        if (_startNode.id == 0)
            ExecuteSpecial(_specialType);
    }

    private void CoupleFail()
    {
        AudioManager.Instance.PlaySoundConnectFailButton();
        _boardUI[_startNode.x, _startNode.y]?.UnSelect();
        _boardUI[_endNode.x, _endNode.y]?.UnSelect();
    }

    private void CompletedConnection()
    {
        _startNode = null;
        _endNode = null;
        DOVirtual.DelayedCall(.1f, LineSpawner.Instance.ClearLines);
        HintManager.Instance.UnHint();
        TimerManager.Instance.ResetAutoHintTimer();
        GridMove();
        if (!CheckCompletedMap()) return;
        CompletedLevel();
    }

    private void EndNodeDifStartNode()
    {
        Graph graph = GetGraphById(_startNode.id);
        var points = GetPathFrom(_startNode, _endNode);
        if (_startNode.id == _endNode.id)
        {
            if (points != null)
            {
                LineSpawner.Instance.Concatenate(points);
                CoupleSuccess();
                graph.RemovePathFrom(_startNode, _endNode);
                if (_ids.Count > 0)
                {
                    this.SetMatrix();
                    this.SetGraphs();
                }
                if (!this.CheckExistCouple())
                    this.Remap();
            }
            else
            {
                if (LevelManager.Instance.Level < 6)
                    MergeNodeFall.Instance.ShowPathFall(_startNode.x, _startNode.y, _endNode.x, _endNode.y, _matrix.GetMatrix);
                this.CoupleFail();
            }
        }
        else
        {
            this.CoupleFail();
        }
        this.CompletedConnection();
    }

    private void CompletedLevel()
    {
        Clear();
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

    private void SwapItem(int row1, int col1, int row2, int col2)
    {
        Item itemA = _boardUI[row1, col1];
        Item itemB = _boardUI[row2, col2];
        itemA?.SetCoordinate(row2, col2);
        itemB?.SetCoordinate(row1, col1);
        _boardUI[row2, col2] = itemA;
        _boardUI[row1, col1] = itemB;
        itemA?.transform.DOMove(_posGrid[row2, col2], 1f);
        itemB?.transform.DOMove(_posGrid[row1, col1], 1f);
    }

    private void Swap(List<Node> nodes)
    {
        if (nodes.Count == 0) return;
        int randIndex = UnityEngine.Random.Range(0, nodes.Count);
        int id = nodes[0].id;
        nodes[0].id = nodes[randIndex].id;
        nodes[randIndex].id = id;
        SwapItem(nodes[0].x, nodes[0].y, nodes[randIndex].x, nodes[randIndex].y);
        nodes.RemoveAt(randIndex);
        nodes.RemoveAt(0);
        Swap(nodes);
    }

    private void UnSelectAll()
    {
        ItemSpawner.Instance.UnSelectAll();
        _startNode = null;
        _endNode = null;
    }

    public void Remap()
    {
        UnSelectAll();
        CompletedConnection();
        this.SetNodeSwap();
        this.SetMatrix();
        this.SetGraphs();
        if (this.CheckExistCouple())
        {
            return;
        }
        Remap();
    }

    public Graph GetFirstGraphExistCouple()
    {
        foreach (var graph in _graphes)
        {
            if (graph.IsExistCouple())
                return graph;
        }
        return null;
    }

    public Couple GetRandomCouple()
    {
        int id = _ids[UnityEngine.Random.Range(0, _ids.Count)];
        List<Node> _sameNodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] != null && _board[row, col].id == id)
                _sameNodes.Add(_board[row, col]);
            }
        }
        if (_sameNodes.Count == 0)
            return null;
        Node node1 = _sameNodes[UnityEngine.Random.Range(0, _sameNodes.Count)];
        _sameNodes.Remove(node1);
        Node node2 = _sameNodes[UnityEngine.Random.Range(0, _sameNodes.Count)];
        Couple couple = new Couple(
            new Vector2Int(node1.x, node1.y),
            new Vector2Int(node2.x, node2.y)
        );
        return couple;
    }

    public void RemoveItem(int row, int col)
    {
        _boardUI[row, col].gameObject.SetActive(false);
        _ids.Remove(_board[row, col].id);
        _board[row, col] = null;
        _boardUI[row, col] = null;
    }

    public void ExplodeAndRemoveItem(int row, int col)
    {
        AudioManager.Instance.PlaySoundExplosion();
        FXSpawner.Instance.ExplodeFX(_posGrid[row, col]);
        StarSpawner.Instance.StarSpawned(_posGrid[row, col]);
        RemoveItem(row, col);
    }

    public void ResetDataMap()
    {
        this.SetMatrix();
        this.SetGraphs();
    }

    public bool CheckCompletedMap()
    {
        return _ids.Count == 0;
    }

    public void RemoveRandomCouple()
    {
        Couple randCouple = this.GetRandomCouple();
        StarSpawner.Instance.StarSpawned(_posGrid[randCouple.Coord1.x, randCouple.Coord1.y]);
        StarSpawner.Instance.StarSpawned(_posGrid[randCouple.Coord2.x, randCouple.Coord2.y]);
        RemoveItem(randCouple.Coord1.x, randCouple.Coord1.y);
        RemoveItem(randCouple.Coord2.x, randCouple.Coord2.y);
        this.SetMatrix();
        this.SetGraphs();
    }

    #region movement grid
    public void Up()
    {
        UnSelectAll();
        for (int row = 2; row < _totalRows - 1; row++)
        {
            for (int col = 1; col < _totalCols - 1; col++)
            {
                if (_board[row, col] == null || _board[row - 1, col] != null) continue;
                int f_row = row;
                while (true)
                {
                    if (f_row - 1 < 1 || _board[f_row - 1, col] != null) break;
                    f_row--;
                }
                _board[f_row, col] = new Node(f_row, col, _board[row, col].id);
                _board[row, col] = null;
                SwapItem(row, col, f_row, col);
            }
        }
        this.SetMatrix();
        this.SetGraphs();
    }

    public void Down()
    {
        UnSelectAll();
        for (int row = _totalRows - 3; row > 0; row--)
        {
            for (int col = 1; col < _totalCols - 1; col++)
            {
                if (_board[row, col] == null || _board[row + 1, col] != null) continue;
                int f_row = row;
                while (true)
                {
                    if (f_row + 1 > _totalRows - 2 || _board[f_row + 1, col] != null) break;
                    f_row++;
                }
                _board[f_row, col] = new Node(f_row, col, _board[row, col].id);
                _board[row, col] = null;
                SwapItem(row, col, f_row, col);
            }
        }
        this.SetMatrix();
        this.SetGraphs();
    }

    public void Left()
    {
        UnSelectAll();
        for (int row = 1; row < _totalRows - 1; row++)
        {
            for (int col = 2; col < _totalCols - 1; col++)
            {
                if (_board[row, col] == null || _board[row, col - 1] != null) continue;
                int f_col = col;
                while (true)
                {
                    if (f_col - 1 < 1 || _board[row, f_col - 1] != null) break;
                    f_col--;
                }
                _board[row, f_col] = new Node(row, f_col, _board[row, col].id);
                _board[row, col] = null;
                SwapItem(row, col, row, f_col);
            }
        }
        this.SetMatrix();
        this.SetGraphs();
    }

    public void Right()
    {
        UnSelectAll();
        for (int row = 1; row < _totalRows - 1; row++)
        {
            for (int col = _totalCols - 3; col > 0; col--)
            {
                if (_board[row, col] == null || _board[row, col + 1] != null) continue;
                int f_col = col;
                while (true)
                {
                    if (f_col + 1 > _totalCols - 2 || _board[row, f_col + 1] != null) break;
                    f_col++;
                }
                _board[row, f_col] = new Node(row, f_col, _board[row, col].id);
                _board[row, col] = null;
                SwapItem(row, col, row, f_col);
            }
        }
        this.SetMatrix();
        this.SetGraphs();
    }

    public void GridMove()
    {
        if (!_quantityWhenChangeMap.Contains(_ids.Count)) return;
        _quantityWhenChangeMap.Remove(_ids.Count);
        switch (_iMove)
        {
            case 0:
                Down();
                _iMove++;
                break;
            case 1:
                Left();
                _iMove++;
                break;
            case 2:
                Right();
                _iMove++;
                break;
            case 3:
                Up();
                _iMove = 0;
                break;
        }
        if (!CheckExistCouple())
            Remap();
    }
    #endregion
}
