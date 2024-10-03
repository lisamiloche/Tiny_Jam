using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using static Unity.Collections.AllocatorManager;

public class CollisionEchap : MonoBehaviour
{
    [SerializeField] GameObject _echap;
    [SerializeField] GameObject _script;

    private VictoryScreen _vScreen;
    private Collider2D _colliderEchap;
    CorruptBlock _corruptBlock;
    bool _win;
    float _timerDuration = 10f;
    float _startTime;


    void Start()
    {
        _colliderEchap = _echap.GetComponent<Collider2D>();
        _vScreen = _script.GetComponent<VictoryScreen>();
        _corruptBlock = GetComponent<CorruptBlock>();
        _startTime = Time.time;
    }

    void Update()
    {
        if(_win && _corruptBlock._currupted == true)
        {
            _vScreen._victoryScreen.SetActive(true);
        }
        if(_win)
        {
            float elapsedTime = Time.time - _startTime;
            float remainingTime = _timerDuration - elapsedTime;

            if (remainingTime <= 0)
            {
                _win = false;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider == _colliderEchap)
        {
            Debug.Log("Collision");
            _win = true;
        }
    }
}