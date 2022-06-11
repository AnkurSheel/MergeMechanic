using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int _width = 3;

    [SerializeField]
    private int _height = 3;

    [SerializeField]
    private GameObject _cell;

    [SerializeField]
    private float _repeatRate = 2.0f;

    private void Start()
    {
        var spriteSize = _cell.GetComponent<SpriteRenderer>().bounds.size;
        BoardManager.Instance.CreateBoard(
            _width,
            _height,
            transform.position,
            spriteSize,
            InstantiateGameObjectFunc);

        InvokeRepeating(nameof(CreateNewTile), 0.0f, _repeatRate);
    }

    private TileElement InstantiateGameObjectFunc(Vector3 position)
    {
        var tile = Instantiate(_cell, position, _cell.transform.rotation);
        tile.transform.parent = transform;
        var tileElement = tile.GetComponentInChildren<TileElement>();
        return tileElement;
    }

    public void CreateNewTile()
    {
        BoardManager.Instance.CreateNewTile();
    }
}
