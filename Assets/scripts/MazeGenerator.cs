using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    [SerializeField] private Vector2 _size = Vector2.one;
    [SerializeField] private Vector2Int _startPoint = new Vector2Int();
    [SerializeField] private Vector2Int _endPoint = new Vector2Int();

    [Header("Maze Elements Prefabs")]
    [SerializeField] GameObject _road = null;
    [SerializeField] GameObject _wall = null;

    private List<Vector2Int> _roadElemens = new List<Vector2Int>();
    private int _minSize = 4;
    private static System.Random _rand = new System.Random();

    public List<Vector2Int> RoadElements { get => _roadElemens; }

    private void Awake()
    {
        if (_size.x <= _minSize) _size.x = _minSize;
        if (_size.y <= _minSize) _size.y = _minSize;
    }

    private void Start()
    {
        if (_road != null && _wall != null)
        {
            BuildBorders();
            BuildFinishPath();
            FillEmptyArea();
        }
    }

    private void BuildFinishPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        List<Vector2Int> blockedBlocks = new List<Vector2Int>();
        Vector2Int current = _startPoint;

        path.Add(current);

        while (current != _endPoint)
        {
            List<Vector2Int> nightbars = new List<Vector2Int>();

            if (current.x - 1 >= 0 && !blockedBlocks.Contains(new Vector2Int(current.x - 1, current.y)))
            {
                nightbars.Add(new Vector2Int(current.x - 1, current.y));
            }
            if (current.x + 1 <= _size.x && !blockedBlocks.Contains(new Vector2Int(current.x + 1, current.y)))
            {
                nightbars.Add(new Vector2Int(current.x + 1, current.y));
            }
            if (current.y - 1 >= 0 && !blockedBlocks.Contains(new Vector2Int(current.x, current.y - 1)))
            {
                nightbars.Add(new Vector2Int(current.x, current.y - 1));
            }
            if (current.y + 1 <= _size.y && !blockedBlocks.Contains(new Vector2Int(current.x, current.y + 1)))
            { 
                nightbars.Add(new Vector2Int(current.x, current.y + 1)); 
            }

            if (nightbars.Count > 0)
            {
                current = nightbars[_rand.Next(0, nightbars.Count)];
                path.Add(current);
                continue;
            }

            blockedBlocks.Add(current);
            path.Remove(current);
            current = path.Last();
        }

        foreach (var block in path)
        {
            CreateBlockOnPos(_road, block.x, block.y);
            _roadElemens.Add(block);
        }
    }

    private void FillEmptyArea()
    {
        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                if (_roadElemens.Contains(new Vector2Int(x, y)))
                    continue;

                int blockType = _rand.Next(0, 4);

                switch (blockType)
                {
                    case 0:
                        CreateBlockOnPos(_road, x, y);
                        _roadElemens.Add(new Vector2Int(x, y));
                        break;
                    default:
                        CreateBlockOnPos(_wall, x, y);
                        break;
                }
            }
        }
    }

    private void BuildBorders()
    {
        for (int i = -1; i < _size.x + 2; ++i)
        {
            CreateBlockOnPos(_wall, i, (int)_size.y + 1);
            CreateBlockOnPos(_wall, i, -1);
        }

        for (int i = -1; i < _size.y + 1; ++i)
        {
            CreateBlockOnPos(_wall, (int)_size.x + 1, i);
            CreateBlockOnPos(_wall, -1, i);
        }
    }

    private void CreateBlockOnPos(GameObject block, int x, int y)
    {
        GameObject obj = Instantiate(block, transform);
        obj.transform.position = new Vector2(x, y);
    }
}
