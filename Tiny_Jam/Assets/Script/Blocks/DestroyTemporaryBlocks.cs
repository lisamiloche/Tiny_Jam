using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporaryBlocks : MonoBehaviour
{
    [Header("Timer Before Destroy")]
    [SerializeField] float _timerDuration;
    
    float _startTime;
    public bool _isDestroyed = false;
    
    void Start()
    { 
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - _startTime;
        float remainingTime = _timerDuration - elapsedTime;

        if (remainingTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}