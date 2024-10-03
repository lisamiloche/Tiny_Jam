using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    bool _isBlocksRight = false;
    bool _isBlocksLeft = false;
    [SerializeField] float _checkRadius;
    [SerializeField] LayerMask _groundLayer;
    public Vector2 _boxSize = new Vector2(0.00000001f, 0.00000001f);
    BoxCollider2D _temporaryBlockCollider;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _managerTime = _manager.GetComponent<ManagerTime>();
        _temporaryBlockCollider = _temporaryBlock.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Spawner();
        ManageUIBlocks();
        _isBlocksRight = CheckBlocksRight();
        _isBlocksLeft = CheckBlocksLeft();
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
                if (_characterController._isLookingRight && !_isBlocksRight)
                {
                    Instantiate(_temporaryBlock, new Vector2(transform.position.x + _blockSize, transform.position.y + _blockSize / 2), Quaternion.identity);
                    _numberOfActiveBlocks++;
                }
                else if (!_characterController._isLookingRight && !_isBlocksLeft)
                {
                    Instantiate(_temporaryBlock, new Vector2(transform.position.x - _blockSize, transform.position.y + _blockSize / 2), Quaternion.identity);
                    _numberOfActiveBlocks++;
                }
                else
                {
                    //ajouter message "vous ne pouvez pas créer de block"
                }

                AudioManager.Instance.PlaySFX(0);
                AudioManager.Instance.SetSFXVolume(1.0f);

            }
        }
    }

    bool CheckBlocksRight()
    {
        Collider2D[] _collidersGround = new Collider2D[2];
        bool currentGrounded =
            Physics2D.OverlapBoxNonAlloc(new Vector2(transform.position.x + _blockSize, transform.position.y + _blockSize / 2), _boxSize, 0, _collidersGround, _groundLayer) > 0;
        return currentGrounded;
    }

    bool CheckBlocksLeft()
    {
        Collider2D[] _collidersGround = new Collider2D[2];
        bool currentGrounded =
            Physics2D.OverlapBoxNonAlloc(new Vector2(transform.position.x - _blockSize, transform.position.y + _blockSize / 2), _boxSize, 0, _collidersGround, _groundLayer) > 0;
        return currentGrounded;
    }

    private void OnDrawGizmos()
    {
        if (_temporaryBlockCollider == null) return;

        Gizmos.DrawCube(new Vector2(transform.position.x + _blockSize, transform.position.y + _blockSize / 2), _boxSize);
        Gizmos.DrawCube(new Vector2(transform.position.x - _blockSize, transform.position.y + _blockSize / 2), _boxSize);
    }
}