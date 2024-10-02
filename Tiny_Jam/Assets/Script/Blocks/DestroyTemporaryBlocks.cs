using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DestroyTemporaryBlocks : MonoBehaviour
{
    [Header("Timer Before Destroy")]
    [SerializeField] float _timerDuration;
    float _startTime;
    GameObject _player;
    InstantiateTemporaryBlocks _instantiateBlocks;
    GameObject _manager;
    ManagerTime _managerTime;

    // UI Text Timer
    GameObject _timer01;
    GameObject _timer02;
    TMP_Text _textTimer01;
    TMP_Text _textTimer02;
    bool _isOccuped = false;

    void Start()
    {
        FindReference();
        _startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - _startTime;
        float remainingTime = _timerDuration - elapsedTime;

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (_instantiateBlocks._numberOfActiveBlocks == 1)
        {
            _textTimer01.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            _textTimer02.text = "Press E";
        }
        else if (_instantiateBlocks._numberOfActiveBlocks == 2)
        {
            //_textTimer01.text = garder la valeur de l'autre instance
            _textTimer02.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            _textTimer01.text = "Press E";
            _textTimer02.text = "Press E";
        }

        if (remainingTime <= 0 || _managerTime._timeEnded)
        {
            _instantiateBlocks._numberOfActiveBlocks--;
            Destroy(gameObject);
        }
    }

    void FindReference()
    {
        _player = GameObject.Find("Player");
        _instantiateBlocks = _player.GetComponent<InstantiateTemporaryBlocks>();

        _manager = GameObject.Find("_scripts");
        _managerTime = _manager.GetComponent<ManagerTime>();

        _timer01 = GameObject.Find("TextTimer01");
        _textTimer01 = _timer01.GetComponent<TMP_Text>();

        _timer02 = GameObject.Find("TextTimer02");
        _textTimer02 = _timer02.GetComponent<TMP_Text>();
    }

}