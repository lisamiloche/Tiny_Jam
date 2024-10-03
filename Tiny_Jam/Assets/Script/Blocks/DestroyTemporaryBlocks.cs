using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporaryBlocks : MonoBehaviour
{
    [Header("Timer Before Destroy")]
    [SerializeField] float _timerDuration;

    float _startTime;
    public bool _isDestroyed = false;
    GameObject _player;
    InstantiateTemporaryBlocks _instantiateBlocks;

    void Start()
    {
        _startTime = Time.time;
        _player = GameObject.Find("Player");
        _instantiateBlocks = _player.GetComponent<InstantiateTemporaryBlocks>();
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
