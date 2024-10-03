using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptBlock : MonoBehaviour
{
    [SerializeField] GameObject _corruptBlock;
    BoxCollider2D _collider;
    public bool _currupted = false;
    [SerializeField] Image _UICorruption;
    [SerializeField] Color _color;

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
            _UICorruption.color = _color;
            Debug.Log(_currupted);

            AudioManager.Instance.PlaySFX(2);
            AudioManager.Instance.SetSFXVolume(1.0f);

        }
    }
}
