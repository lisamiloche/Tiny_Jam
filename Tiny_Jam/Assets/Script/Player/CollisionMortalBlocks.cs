using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MortalBlocks : MonoBehaviour
{
    [SerializeField] List<GameObject> _mortalBlocks = new List<GameObject>();
    bool _isDeath = false;

    void Start()
    {

    }

    void Update()
    {
        if (_isDeath)
        {
            //Instantiate(VFXDeath, position, rotation);
            //PlayAnimDeath
            //Delais
            //Ouvrir page de défaite
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