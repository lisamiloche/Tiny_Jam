using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    string _currentSceneName;
    int _index;
    [SerializeField] public GameObject _victoryScreen;

    void Start()
    {
        _currentSceneName = "Level0";
        _index = 1;
        _victoryScreen.SetActive(false);

        //AudioManager.Instance.PlayMusic(0, false);
        //AudioManager.Instance.SetMusicVolume(1.2f);
    }

    public void ObClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickReload()
    {
        SceneManager.LoadScene(_currentSceneName + _index);
    }

    public void OnClickNext()
    {
        SceneManager.LoadScene(_currentSceneName + (_index+1));
    }
}
