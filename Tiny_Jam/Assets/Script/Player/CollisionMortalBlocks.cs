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

    void Start()
    {
        _screen = _script.GetComponent<DefeatScreen>();
    }

    void Update()
    {
        if (_isDeath)
        {
            //Instantiate(VFXDeath, position, rotation);
            //PlayAnimDeath
            //Delais
            _screen._defeatScreen.SetActive(true);
            Destroy(gameObject); // à changer
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach (var block in _mortalBlocks)
        {
            var _colBlock = block.GetComponent<Collider2D>();

            if (col.collider == _colBlock)
            {
                //Debug.Log("Collision");
                _isDeath = true;
            }
        }
    }
}