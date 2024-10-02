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
    bool _win;

    void Start()
    {
        _colliderEchap = _echap.GetComponent<Collider2D>();
        _vScreen = _script.GetComponent<VictoryScreen>();
    }

    void Update()
    {
        if(_win)
        {
            _vScreen._victoryScreen.SetActive(true);
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