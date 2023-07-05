using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviourSingleton<BoardManager>
{
    [SerializeField] private ItemSpriteStorage _itemSpriteStorage;
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _linePrefab;
    private Node[,] _board;
    [SerializeField] private List<int> _values;
    private int _totalRows = 10, _totalCols = 7;
    private float _startX = 0, _startY = 0;
    private Item[,] _boardUI;
    private Node _startNode, _endNode;
    [SerializeField] Matrix _matrix;
    [SerializeField] List<Graph> _graphes = new List<Graph>();

    public int TotalItems => (_totalRows - 2) * (_totalCols - 2);

    public List<Node> GetNodesById(int id)
    {
        List<Node> nodes = new List<Node>();
        for (int row = 0; row < _totalRows; row++)
        {
            for (int col = 0; col < _totalCols; col++)
            {
                if (_board[row, col] == null) continue;
                if (_board[row, col].Val == id) continue;
                nodes.Add(_board[row, col]);
            }
        }
        return nodes;
    }

    public string GetPathFrom(Node nodeA, Node nodeB)
    {
        return _matrix.GetPath(new Point(nodeA.X, nodeA.Y), new Point(nodeB.X, nodeB.Y));
    }

    public void CreateBoard(LevelConfig config)
    {
        _totalRows = config.Row + 2;//border null
        _totalCols = config.Col + 2;//border null
        SetListValues();
        _values.Shuffle();
        SpawnItems(_values);
        //
        int[,] matrix = new int[_totalRows, _totalCols];
        for (int i = 0; i < _totalRows; i++)
            for (int j = 0; j < _totalCols; j++)
                matrix[i, j] = _board[i, j] == null ? -1 : _board[i, j].Val;
        _matrix = new Matrix(matrix);
        //
        foreach (var id in _values)
            _graphes.Add(new Graph(id));
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
                    Item item = Instantiate(_itemPrefab, position, Quaternion.identity, _container);
                    item.name = $"Item[{row}_{col}_{val}]";
                    item.Init(row, col, _itemSpriteStorage.Sprites[val]);
                    item.transform.localScale = Vector3.one * .7f;
                    _board[row, col] = new Node(row, col, val);
                    _boardUI[row, col] = item;
                    id += 1;
                }
            }
        }
    }

    public void Clear()
    {
        if (_boardUI != null)
            for (int i = _container.childCount - 1; i >= 0; i--) Destroy(_container.GetChild(i).gameObject);
        _boardUI = null;
        _board = null;
        _values = null;
    }

    public void SelectNode(int row, int col)
    {
        if (_startNode == null)
        {
            _startNode = _board[row, col];
        }
        else
        {
            _endNode = _board[row, col];
            if (_endNode == _startNode)
            {
                _startNode = null;
                _endNode = null;
                return;
            }
            GameManager.Instance.CheckingConnection();
            var dir = _matrix.GetPath(new Point(_startNode.X, _startNode.Y), new Point(_endNode.X, _endNode.Y));
            //
            if(dir != null)
            {
                _board[_startNode.X, _startNode.Y] = null;
                _board[_endNode.X, _endNode.Y] = null;
                Destroy(_boardUI[_startNode.X, _startNode.Y].gameObject);
                Destroy(_boardUI[_endNode.X, _endNode.Y].gameObject);
                _boardUI[_startNode.X, _startNode.Y] = null;
                _boardUI[_endNode.X, _endNode.Y] = null;
                _matrix.CheckSuccess(
                    new Point(_startNode.X, _startNode.Y),    
                    new Point(_endNode.X, _endNode.Y)
                );
                _values.Remove(_startNode.Val);
                _values.Remove(_startNode.Val);
                if(CheckHasCouple())
                    Remap();
            }
            else
            {
                Debug.Log("Fail");
            }
            //
            GameManager.Instance.CompletedCheckConnection();
            _startNode = null;
            _endNode = null;
        }
    }

    private bool CheckHasCouple()
    {
        foreach (var graph in _graphes)
            if (!graph.IsUnCouple()) return false;
        return true;
    }

    private void Remap()
    {

    }

    public void Detect()
    {
        if (GameManager.Instance.GameState != GameState.OnBattle) return;
        if (GameManager.Instance.BattleState != BattleState.None) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        hit.collider?.GetComponent<Item>()?.Select();
    }
}
