using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MortalBlocks : MonoBehaviour
{
    [SerializeField] List<GameObject> _mortalBlocks = new List<GameObject>();
    bool _isDeath = false;
    [SerializeField] GameObject _script;
    DefeatScreen _screen;

    float _timerDuration = 3f;
    float _startTime;

    void Start()
    {
        _screen = _script.GetComponent<DefeatScreen>();
        _startTime = Time.time;
    }

    void Update()
    {
        if (_isDeath)
        {
            float elapsedTime = Time.time - _startTime;
            float remainingTime = _timerDuration - elapsedTime;

            if (remainingTime <= 0)
            {
                _screen._defeatScreen.SetActive(true);
                Destroy(gameObject);
            }        
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach (var block in _mortalBlocks)
        {
            var _colBlock = block.GetComponent<Collider2D>();

            if (col.collider == _colBlock)
            {
                _isDeath = true;
            }
        }
    }
}