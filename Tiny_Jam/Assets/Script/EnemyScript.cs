using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Device;

public class EnemyScript : MonoBehaviour
{
    public bool isAttacking = false;
    AnimManager _animManager;
    [SerializeField] GameObject _player;
    Collider2D _collider;
    float _timerDuration = 10f;
    float _startTime;

    private void Awake()
    {
        _animManager = GetComponent<AnimManager>();
        _collider = _player.GetComponent<Collider2D>();
    }
    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        PlayAnimation();

        if(isAttacking)
        {
            float elapsedTime = Time.time - _startTime;
            float remainingTime = _timerDuration - elapsedTime;

            if (remainingTime <= 0)
            {
                isAttacking = false;
            }
        }
    }

    private void PlayAnimation()
    {
        _animManager.SetBool("isAttacking", isAttacking);

        if (isAttacking)
        {
            _animManager.PlayAnimation("enemy_attack");
        }
        else
        {
            _animManager.PlayAnimation("enemy_idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var _colPlayer = _collider;

        if (col== _colPlayer)
        {
            isAttacking = true;
            Debug.Log("collision");
        }
    }
}
