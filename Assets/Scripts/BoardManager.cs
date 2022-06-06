using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private int _width = 3;

    [SerializeField]
    private int _height = 3;

    [SerializeField]
    private GameObject _cell;

    [SerializeField]
    private GameObject _tile;

    [SerializeField]
    private float _repeatRate = 2.0f;

    private readonly List<GameObject> FullTiles = new List<GameObject>();

    private readonly List<GameObject> EmptyTiles = new List<GameObject>();

    public static BoardManager Instance { get; private set; }

    // Use this for initialization
    private void Start()
    {
        Instance = GetComponent<BoardManager>();

        Vector2 offset = _cell.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);

        InvokeRepeating(nameof(CreateNewTile), 0.0f, _repeatRate);
    }

    private void CreateBoard(float offsetX, float offsetY)
    {
        var startX = transform.position.x;
        var startY = transform.position.y;

        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                var tile = Instantiate(_cell, new Vector3(startX + offsetX * i, startY + offsetY * j, 0), _tile.transform.rotation);
                tile.transform.parent = transform;
                EmptyTiles.Add(tile);
            }
        }
    }

    private void CreateNewTile()
    {
        if (EmptyTiles.Count == 0)
        {
            Debug.Log("Game Ended!");
        }
        else
        {
            var tileElement = EmptyTiles[Random.Range(0, EmptyTiles.Count)];
            var tile = Instantiate(
                _tile,
                tileElement.transform.position,
                tileElement.transform.rotation,
                tileElement.transform);

            FullTiles.Add(tileElement);
            EmptyTiles.Remove(tileElement);
        }
    }
}
