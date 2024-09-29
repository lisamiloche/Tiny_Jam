using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    string currentSceneName;
    [SerializeField] public GameObject _defeatScreen;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        _defeatScreen.SetActive(false);
    }
    public void ObClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickReload()
    {
        SceneManager.LoadScene(currentSceneName);
    }
}
