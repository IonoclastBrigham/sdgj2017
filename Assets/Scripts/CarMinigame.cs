using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMinigame : MonoBehaviour {

    private GlobalState _globalState;

    public Color FadeInColor;
    public UnityEngine.UI.Image FadePanel;
    public float MinigameDuration = 30f;
    private bool _isRunning;
    private float _startTime;
	// Use this for initialization
	void Start () {

        StartCoroutine(FadeToColor(FadeInColor, 1.0f));
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
            StartCoroutine(FadeToColor(Color.black, 0.0f));
            EndMinigame();
            _isRunning = false;
        }
    }

    public void Crash()
    {
        if (_isRunning)
        {
            _isRunning = false;
            StartCoroutine(FadeToColor(Color.red, 0.0f));
            EndMinigame();
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

    IEnumerator FadeToColor( Color c, float startAlpha )
    {
        var startColor = c;
        c.a = startAlpha;
        FadePanel.color = startColor;
        float duration = 2.0f;
        float t = duration;
        while (t > 0)
        {
            FadePanel.color = Color.Lerp(startColor, c, Mathf.Clamp01(t / duration));
            yield return new WaitForEndOfFrame();

            t -= Time.deltaTime;
        }

        yield return null;
    }

}
