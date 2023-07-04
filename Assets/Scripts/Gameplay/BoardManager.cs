using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviourSingleton<BoardManager>
{
    [SerializeField] private ItemSpriteStorage _itemSpriteStorage;
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private Transform _container;
    private Node[,] _board;
    private List<int> _values;
    private int _totalRows = 10, _totalCols = 7;
    private float _startX = 0, _startY = 0;
    private Item[,] _boardUI;
    private Node _startNode, _endNode;
   
    public int TotalItems => (_totalRows - 2) * (_totalCols - 2);

    public void CreateBoard(LevelConfig config)
    {
        _totalRows = config.Row + 2;//border null
        _totalCols = config.Col + 2;//border null
        SetListValues();
        MixValues(_values);
        SpawnItems(_values);
    }

    private void SetListValues()
    {
        _values =new List<int>();
        for (int i = 0; i < TotalItems; i++)
        {
            int val = UnityEngine.Random.Range(0, _itemSpriteStorage.Sprites.Count);
            _values.Add(val);
            _values.Add(val);
        }
    }

    private void MixValues(List<int> values)
    {
        for (int index = 0; index < values.Count; index++)
        {
            int temp = values[index];
            int randomIndex = UnityEngine.Random.Range(index, values.Count);
            values[index] = values[randomIndex];
            values[randomIndex] = temp;
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
                    item.name = $"Item[{row}_{col}]";
                    item.Init(row, col, _itemSpriteStorage.Sprites[val]);
                    item.transform.localScale = Vector3.one * .7f;
                    _board[row, col] = new Node(row, col, val);
                    _boardUI[row, col] = item;

                    id += 1;
                }
            }
        }
    }

    public void ChoseNode(int row, int col)
    {
        if (_startNode == null)
            _startNode = _board[row, col];
        else
        {
            _endNode = _board[row, col];
            GameManager.Instance.CheckingConnection();
        }
    }

    public Node GetNodeFrom(int row, int col) => _board[row, col];

    public bool CheckConnection(Node nodeA, Node nodeB)
    {
        return true;
    }

    public void Clear()
    {
        if (_boardUI != null)
            for (int i = _container.childCount - 1; i >= 0; i--) Destroy(_container.GetChild(i).gameObject);
        _boardUI = null;
        _board = null;
        _values = null;
    }
}
