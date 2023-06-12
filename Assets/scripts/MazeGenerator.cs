using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    [SerializeField] private Vector2 _size = Vector2.one;

    [Header("Maze Elements Prefabs")]
    [SerializeField] GameObject _road = null;
    [SerializeField] GameObject _wall = null;

    private List<Vector2> _roadElemens = new List<Vector2>();
    private int _minSize = 4;

    public List<Vector2> RoadElements { get => _roadElemens; }

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
            CreateFinishPath();
        }
    }

    private void CreateFinishPath()
    {
        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                CreateBlockOnPos(_road, x, y);
                _roadElemens.Add(new Vector2((int)x, (int)y));
            }
        }
    }

    private void BuildBorders()
    {
        for (int i = -1; i < _size.x + 1; ++i)
        {
            CreateBlockOnPos(_wall, i, (int)_size.y);
            CreateBlockOnPos(_wall, i, -1);
        }

        for (int i = -1; i < _size.y + 1; ++i)
        {
            CreateBlockOnPos(_wall, (int)_size.x,i);
            CreateBlockOnPos(_wall, -1, i);
        }
    }

    private void CreateBlockOnPos(GameObject block, int x, int y)
    {
        GameObject obj = Instantiate(block, transform);
        obj.transform.position = new Vector2(x, y);
    }
}
