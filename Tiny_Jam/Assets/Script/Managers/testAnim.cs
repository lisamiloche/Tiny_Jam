using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnim : MonoBehaviour
{
    private AnimManager _animManager;

    private void Awake()
    {
        _animManager = GetComponent<AnimManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Appuyer sur la barre d'espace
        {
            _animManager.PlayAnimation("player_idle"); // Remplace par le nom d'une animation existante
            Debug.Log("Jouer animation idle");
        }
    }
}
