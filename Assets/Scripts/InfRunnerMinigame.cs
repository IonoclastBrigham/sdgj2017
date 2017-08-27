using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfRunnerMinigame : MonoBehaviour {

    public float CharacterMoveSpeed = 0;
    public Transform Character;
    public GameObject Ragdoll; 
    public UnityEngine.UI.Image panel;

    private float _startX;
    private bool _running = false;
    private float _startTime;

    public bool Running { get {return _running; }}
    public float MinigameDuration = 30f;

    public Transform Player
    {
        get { return Character.gameObject.activeSelf ? Character : Ragdoll.transform; }
    }

    void Awake()
    {
        _running = true;
        _startX = Character.position.x;
        _startTime = Time.time;
    }


    void Update()
    {
        if (!_running)
            return;
        
        if (Character)
        {
            if (Time.time - _startTime > MinigameDuration)
            {
                _running = false;
                StartCoroutine(StopMovement(1.0f));
                StartCoroutine(FadeToColor(Color.black));
                StartCoroutine(EndMinigame(2.0f, true));
            }

            if (_running)
            {
                if (Character.position.x < _startX - 0.5f)
                {
                    TriggerRagdoll();
                    _running = false;
                }
            }
        }
    }

    void TriggerRagdoll()
    {
        Character.gameObject.SetActive(false);
        Ragdoll.gameObject.SetActive(true);

        Ragdoll.transform.position = Character.transform.position;

        StartCoroutine(StopMovement(0.5f));
        StartCoroutine(FadeToColor( Color.red ));
        StartCoroutine(EndMinigame(2.0f, false));
    }

    IEnumerator StopMovement( float delay )
    {
        yield return new WaitForSeconds(delay);
        CharacterMoveSpeed = 0;
    }

    IEnumerator FadeToColor( Color c )
    {
        yield return new WaitForSeconds(0.5f);

        var startColor = c;
        c.a = 0;
        panel.color = startColor;
        float duration = 2.0f;
        float t = duration;
        while (t > 0)
        {
            panel.color = Color.Lerp(startColor, c, Mathf.Clamp01(t / duration));
            yield return new WaitForEndOfFrame();

            t -= Time.deltaTime;
        }
    }

    IEnumerator EndMinigame( float delay, bool success )
    {
        yield return new WaitForSeconds(delay);

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedRunnerMinigame = success;
            globalState.GoToNextMinigame();
        }
    }
}
