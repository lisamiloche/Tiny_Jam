using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] float _checkRadius;
    [SerializeField] LayerMask _groundLayer;

    [Header("UI Blocks")]
    [SerializeField] Image _block01;
    [SerializeField] Image _block02;
    [SerializeField] TextMeshProUGUI _textBlock01;
    [SerializeField] TextMeshProUGUI _textBlock02;


    CharacterController _characterController;
    bool _isBlocksRight = false;
    bool _isBlocksLeft = false;
    public Vector2 _boxSize = new Vector2(0.00000001f, 0.00000001f);
    BoxCollider2D _temporaryBlockCollider;

    List<float> _remainingTimes = new List<float>();


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _managerTime = _manager.GetComponent<ManagerTime>();
        _temporaryBlockCollider = _temporaryBlock.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Spawner();
        UpdateRemainingTimes();
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

            if(_remainingTimes.Count > 0)
                _textBlock01.text = FormatTimer(_remainingTimes[0]);
            else
                _textBlock01.text = "E";

            _textBlock02.text = "E";
        }
        else if (_numberOfActiveBlocks == 2)
        {
            _block01.color = Color.white;
            _block02.color = Color.white;

            if (_remainingTimes.Count > 0)
                _textBlock01.text = FormatTimer(_remainingTimes[0]);
            else
                _textBlock01.text = "E";

            if (_remainingTimes.Count > 1)
                _textBlock02.text = FormatTimer(_remainingTimes[1]);
            else
                _textBlock02.text = "E";
        }
        else
        {
            _block01.color = Color.grey;
            _block02.color = Color.grey;
            _textBlock01.text = "E";
            _textBlock02.text = "E";
        }
    }

    string FormatTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateRemainingTimes()
    {
        for (int i = _remainingTimes.Count - 1; i >= 0; i--)
        {
            if (_remainingTimes[i] > 0)
            {
                _remainingTimes[i] -= Time.deltaTime;
            }
            else
            {
                _remainingTimes.RemoveAt(i);
            }
        }
    }

    void Spawner()
    {
        if (Input.GetKeyDown("e") && !_managerTime._timeEnded)
        {
            if (_numberOfActiveBlocks < _numberMaxOfBlocks)
            {
                GameObject block = null;
                if (_characterController._isLookingRight && !_isBlocksRight)
                {
                    block = Instantiate(_temporaryBlock, new Vector2(transform.position.x + _blockSize, transform.position.y + _blockSize / 2), Quaternion.identity);
                }
                else if (!_characterController._isLookingRight && !_isBlocksLeft)
                {
                    block = Instantiate(_temporaryBlock, new Vector2(transform.position.x - _blockSize, transform.position.y + _blockSize / 2), Quaternion.identity);
                }

                if(block != null)
                {
                    float timerDuration = block.GetComponent<DestroyTemporaryBlocks>()._timerDuration;
                    _remainingTimes.Add(timerDuration);
                    _numberOfActiveBlocks++;
                }
                else
                {
                    //ajouter message "vous ne pouvez pas créer de block"
                }

                //AudioManager.Instance.PlaySFX(0);
                //AudioManager.Instance.SetSFXVolume(1.0f);

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