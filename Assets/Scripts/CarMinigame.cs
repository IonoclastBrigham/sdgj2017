using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMinigame : MonoBehaviour {

    private GlobalState _globalState;

    public float MinigameDuration = 60f;
    private bool _isRunning;
    private float _startTime;
	// Use this for initialization
	void Start () {
        _globalState = GameObject.FindObjectOfType<GlobalState>();
        _isRunning = true;
        _startTime = Time.time;
	}
	
    void Update()
    {
        if (!_isRunning)
            return;

        if (Time.time - _startTime >= MinigameDuration)
        {
            EndMinigame();
            _isRunning = false;
        }
    }

    void EndMinigame()
    {
        if (_globalState)
        {
            _globalState.CompletedCarMinigame = true;
            _globalState.GoToNextMinigame();
        }
    }
}
