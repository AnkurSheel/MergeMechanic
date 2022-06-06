using System.Collections.Generic;
using UnityEngine;

public class TileElement : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _shapes = new List<Sprite>();

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var newSprite = _shapes[0];
        _spriteRenderer.sprite = newSprite;
    }
}
