using System.Collections.Generic;
using System.Security.Cryptography;
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
    private float _repeatRate = 2.0f;

    private readonly List<TileElement> _fullTiles = new List<TileElement>();

    private readonly List<TileElement> _emptyTiles = new List<TileElement>();

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
                var tile = Instantiate(_cell, new Vector3(startX + offsetX * i, startY + offsetY * j, 0), _cell.transform.rotation);
                tile.transform.parent = transform;
                var tileElement = tile.GetComponentInChildren<TileElement>();
                tileElement.Hide();               
                _emptyTiles.Add(tileElement);
            }
        }
    }

    private void CreateNewTile()
    {
        if (_emptyTiles.Count == 0)
        {
            Debug.Log("Game Ended!");
        }
        else
        {
            var tileElement = _emptyTiles[Random.Range(0, _emptyTiles.Count)];
            tileElement.Show();

            _fullTiles.Add(tileElement);
            _emptyTiles.Remove(tileElement);
        }
    }

    public void OnMerge(TileElement tileElement)
    {
        var tile = _fullTiles.Find(x => x == tileElement);
        _emptyTiles.Add(tile);
        _fullTiles.Remove(tile);
        tileElement.gameObject.SetActive(false);
    }
}
