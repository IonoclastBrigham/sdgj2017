using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEMinigame : MonoBehaviour {

    public RectTransform Button;
    public UnityEngine.UI.Image FadePanel;

    public AudioSource Audio;
    public AudioClip SuccessClip;
    public AudioClip FailClip;
    public Rigidbody Controller;
    public Transform SuccessPosition;

    private float _startScale;
    private bool _acceptingInput;
    private bool _buttonPressed = false;

	// Use this for initialization
	void Start () {

        Controller.isKinematic = true;
        _startScale = Button.localScale.x;
        Button.gameObject.SetActive(false);

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedQTEMinigame = true;
            globalState.GoToNextMinigame();
        }

        StartCoroutine(SetQTEWindow(Random.Range(0.2f, 2), 1));
	}

    private IEnumerator SetQTEWindow( float delay, float windowDuration )
    {
        yield return new WaitForSeconds(delay);

        Button.gameObject.SetActive(true);

        _acceptingInput = true;

        yield return new WaitForSeconds(windowDuration);

        Button.gameObject.SetActive(false);
        _acceptingInput = false;

        EvaluateGoal();

        yield return new WaitForSeconds(2.0f);

        EndGame();
    }

    void EvaluateGoal()
    {
        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedQTEMinigame = _buttonPressed;
        }

        if (_buttonPressed)
        {
            StartCoroutine(FadeToColor(Color.black));
        } else
        {
            StartCoroutine(FadeToColor(Color.red));
            Audio.PlayOneShot(FailClip);

            Time.timeScale = 0.8f;
            Controller.isKinematic = false;
        }
    }

    void EndGame()
    {
        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.GoToNextMinigame();
        }
    }

    void Update()
    {
        if (_acceptingInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _buttonPressed = true;
                Audio.PlayOneShot(SuccessClip);
                Controller.transform.position = SuccessPosition.position;
                Controller.transform.rotation = SuccessPosition.rotation;
            }
        }

        var scale = Button.localScale;
        scale.x = _startScale + 0.1f * Mathf.Sin(10f * Time.time);
        scale.y = _startScale + 0.1f * Mathf.Sin(10f * Time.time);
        Button.localScale = scale;
    }

       IEnumerator FadeToColor( Color c )
    {
        yield return new WaitForSeconds(0.5f);

        var startColor = c;
        c.a = 0;
        FadePanel.color = startColor;
        float duration = 2.0f;
        float t = duration;
        while (t > 0)
        {
            FadePanel.color = Color.Lerp(startColor, c, Mathf.Clamp01(t / duration));
            yield return new WaitForEndOfFrame();

            t -= Time.deltaTime;
        }
    }
}
