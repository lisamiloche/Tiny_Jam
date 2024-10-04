using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    string _currentSceneName;
    [SerializeField] public GameObject _victoryScreen;
    DefeatScreen _defeatScreen;
    [SerializeField] GameObject _defeatSereen;

    void Start()
    {
        _currentSceneName = "Level0";
        _defeatScreen._index = 1;
        _victoryScreen.SetActive(false);

        AudioManager.Instance.PlaySFX(3);
        AudioManager.Instance.SetSFXVolume(1.0f);

        _defeatScreen = _defeatSereen.GetComponent<DefeatScreen>();
    }

    public void ObClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickReload()
    {
        SceneManager.LoadScene(_currentSceneName + _defeatScreen._index);
    }

    public void OnClickNext()
    {
        SceneManager.LoadScene(_currentSceneName + (_defeatScreen._index + 1));
    }
}
