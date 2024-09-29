using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateTemporaryBlocks : MonoBehaviour
{
    [Header("Reference To Prefab")]
    [SerializeField] GameObject _temporaryBlock;

    [Header("Temporary Blocks")]
    [SerializeField] int _numberMaxOfBlocks;
    [SerializeField] float _blockSize;
    public List<GameObject> _temporaryBlocksList = new List<GameObject>();

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GameObject _prefabBlock = _temporaryBlock;
        Spawner(_prefabBlock);
    }

    void Spawner(GameObject gameObject)
    {
        if (Input.GetKeyDown("e"))
        {
            if (_temporaryBlocksList.Count < _numberMaxOfBlocks)
            {
                if (_characterController._isLookingRight == true)
                {
                    Instantiate(gameObject, new Vector2(transform.position.x + _blockSize, transform.position.y), Quaternion.identity);
                    _temporaryBlocksList.Add(gameObject);
                }
                else
                {
                    Instantiate(gameObject, new Vector2(transform.position.x - _blockSize, transform.position.y), Quaternion.identity);
                    _temporaryBlocksList.Add(gameObject);
                }
            }
        }
    }
}