using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    bool _isCol = false;
    [SerializeField] LayerMask _defaultMask;
    [SerializeField] float _colRadius;

    private void Update()
    {
        if (_isCol)
        {
            // Jouer l'animation d'attaque de l'ennemi (voir en fonction de l'orientation)
        }
    }

    void CheckPlayer()
    {
        Collider2D[] _collidersPlayer = new Collider2D[2];

        if(transform.localScale == new Vector3 (1, 1, 1))
        {
            bool currentPlayer =
            Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x + 0.15f, transform.position.y - 0.25f), _colRadius, _collidersPlayer, _defaultMask) > 0;
            _isCol = currentPlayer;
        }
        else
        {
            bool currentPlayer =
            Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x - 0.15f, transform.position.y - 0.25f), _colRadius, _collidersPlayer, _defaultMask) > 0;
            _isCol = currentPlayer;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector2(transform.position.x + 0.15f, transform.position.y - 0.25f), _colRadius);
    }
}
