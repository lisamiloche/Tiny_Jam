using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    string _currentSceneName;
    int _index = 1;
    [SerializeField] public GameObject _defeatScreen;

    void Start()
    {
        _currentSceneName = "Level0";
        _defeatScreen.SetActive(false);
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
