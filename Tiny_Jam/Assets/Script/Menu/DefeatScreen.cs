using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    string _currentSceneName;
    public int _index;
    [SerializeField] public GameObject _defeatScreen;

    void Start()
    {
        _currentSceneName = "Level0";
        _defeatScreen.SetActive(false);

        AudioManager.Instance.PlaySFX(4);
        AudioManager.Instance.SetSFXVolume(1.0f);

    }
    public void ObClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickReload()
    {
        SceneManager.LoadScene(_currentSceneName + _index);
    }
}
