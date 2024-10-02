using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _menuPause;
    bool _isMenuPause = false;
    
    void Start()
    {
        _menuPause.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            _menuPause.SetActive(true);
            _isMenuPause = true;
        }

        if (_isMenuPause && !_menuPause.activeSelf)
        {
            _isMenuPause = false;
        }
    }

    public void OnClickReturn()
    {
        Time.timeScale = 1.0f;
        _menuPause.SetActive(false);
        _isMenuPause = false;
    }
}
