using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptBlock : MonoBehaviour
{
    [SerializeField] GameObject _corruptBlock;
    BoxCollider2D _collider;
    public bool _currupted = false;

    private void Start()
    {
        _collider = _corruptBlock.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var _colBlock = _collider;

        if (col.collider == _colBlock)
        {
            _currupted = true;
            Debug.Log(_currupted);
        }
    }
}
