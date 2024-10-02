using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    [SerializeField] float _timerDuration;
    float _startTime;
    public bool _timeEnded = false;
    [SerializeField] GameObject _generalTimer;
    TMP_Text _textGeneralTimer;

    void Start()
    {
        _startTime = Time.time;
        _textGeneralTimer = _generalTimer.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - _startTime;
        float remainingTime = _timerDuration - elapsedTime;

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        _textGeneralTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0)
        {
            _timeEnded = true;
            _textGeneralTimer.text = "END";
        }
    }
}
