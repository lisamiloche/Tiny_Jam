using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{

    [SerializeField] float _crouchHeight;
    [SerializeField] CharacterController _player;
    [SerializeField] Transform _headCheck;
    [SerializeField] float _headCheckLenght;
    [SerializeField] LayerMask _groundMask;

    private Vector2 _normalHeight;
    bool _isHeadHitting;

    void Start()
    {
        _normalHeight = transform.localScale;
    }

    
    void Update()
    {
        Crouching();
        _isHeadHitting = HeadDetecting();
    }

    private void Crouching()
    {
        if ((Input.GetKey(KeyCode.DownArrow) || _isHeadHitting) && _player._isGrounded)
        {
            if (transform.localScale.y != _crouchHeight)
                transform.localScale = new Vector2(_normalHeight.x, _crouchHeight);
        }
        else
        {
            if (transform.localScale.y != _normalHeight.y)
                transform.localScale = _normalHeight;
        }
    }

    bool HeadDetecting()
    {
        bool hit = Physics2D.Raycast(_headCheck.position, Vector2.up, _headCheckLenght, _groundMask);
        return hit;
    }

    private void OnDrawGizmos()
    {
        if (_headCheck == null)
            return;

        Vector2 from = _headCheck.position;
        Vector2 to = new Vector2(from.x, from.y+ _headCheckLenght);

        Gizmos.DrawLine(from, to);
    }

}
