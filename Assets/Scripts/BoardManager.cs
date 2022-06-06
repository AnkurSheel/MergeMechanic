using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private int _width = 3;

    [SerializeField]
    private int _height = 3;

    [SerializeField]
    private GameObject _tile;

    [SerializeField]
    private List<Sprite> _characters = new List<Sprite>();

    private readonly List<TileElement> FullTiles = new List<TileElement>();

    private readonly List<TileElement> EmptyTiles = new List<TileElement>();

    public static BoardManager Instance { get; private set; }

    // Use this for initialization
    private void Start()
    {
        Instance = GetComponent<BoardManager>();

        Vector2 offset = _tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }

    private void CreateBoard(float offsetX, float offsetY)
    {
        var startX = transform.position.x;
        var startY = transform.position.y;

        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                var tile = Instantiate(_tile, new Vector3(startX + offsetX * i, startY + offsetY * j, 0), _tile.transform.rotation);
                tile.transform.parent = transform;
                EmptyTiles.Add(tile.GetComponentInChildren<TileElement>());
            }
        }
    }
}
