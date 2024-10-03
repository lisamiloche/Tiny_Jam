using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Reference To Windows")]
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _credits;
    
    private void Start()
    {
        _mainMenu.SetActive(true);
        _credits.SetActive(false);
    }
    public void OnClickLV01()
    {
        SceneManager.LoadScene("Level01");
    }

    public void OnClickLV02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void OnClickCredit()
    {
        _mainMenu.SetActive(false);
        _credits.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickBack()
    {
        _mainMenu.SetActive(true);
        _credits.SetActive(false);
    }
}
