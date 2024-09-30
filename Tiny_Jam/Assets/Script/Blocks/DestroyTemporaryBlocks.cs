using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporaryBlocks : MonoBehaviour
{
    [Header("Timer Before Destroy")]
    [SerializeField] float _timerDuration;
    float _startTime;
    [SerializeField] GameObject _player;
    InstantiateTemporaryBlocks _instantiateBlocks;

    void Start()
    {
        _player = GameObject.Find("Player");
        _instantiateBlocks = _player.GetComponent<InstantiateTemporaryBlocks>();
        _startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - _startTime;
        float remainingTime = _timerDuration - elapsedTime;

        if (remainingTime <= 0)
        {
            _instantiateBlocks._numberOfActiveBlocks--;
            Destroy(gameObject);
        }
    }
}