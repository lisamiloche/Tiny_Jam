using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateTemporaryBlocks : MonoBehaviour
{
    [Header("Reference To Objects")]
    [SerializeField] GameObject _temporaryBlock;
    [SerializeField] GameObject _manager;
    ManagerTime _managerTime;

    [Header("Temporary Blocks")]
    [SerializeField] int _numberMaxOfBlocks;
    [SerializeField] float _blockSize;
    public int _numberOfActiveBlocks = 0;

    [Header("UI Blocks")]
    [SerializeField] Image _block01;
    [SerializeField] Image _block02;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _managerTime = _manager.GetComponent<ManagerTime>();
    }

    void Update()
    {
        Spawner();
        ManageUIBlocks();
    }

    private void ManageUIBlocks()
    {
        if (_numberOfActiveBlocks == 1)
        {
            _block01.color = Color.white;
            _block02.color = Color.grey;
        }
        else if (_numberOfActiveBlocks == 2)
        {
            _block01.color = Color.white;
            _block02.color = Color.white;
        }
        else
        {
            _block01.color = Color.grey;
            _block02.color = Color.grey;
        }
    }

    void Spawner()
    {
        if (Input.GetKeyDown("e") && !_managerTime._timeEnded)
        {
            if (_numberOfActiveBlocks < _numberMaxOfBlocks)
            {
                if (_characterController._isLookingRight)
                    Instantiate(_temporaryBlock, new Vector2(transform.position.x + _blockSize, transform.position.y + _blockSize/2), Quaternion.identity);
                else
                    Instantiate(_temporaryBlock, new Vector2(transform.position.x - _blockSize, transform.position.y + _blockSize/2), Quaternion.identity);

                _numberOfActiveBlocks++;

                //AudioManager.Instance.PlayMusic(1, false);
                //AudioManager.Instance.SetMusicVolume(1.2f);

            }
        }
    }
}