using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMinigame : MonoBehaviour {

    public UnityEngine.UI.Image FadePanel;
    public Color FadeInColor;
    public Color FadeOutColor = Color.black;

    public GameObject Player;
    public GameObject Agent;
    public GameObject AgentRagdoll;

    public AudioSource Audio;
    public AudioClip SniperShot;

    public GameObject BloodPrefab;
    public Transform HeadTransform;

    // Use this for initialization
    void Start () {

        StartCoroutine( FadeToColor(FadeInColor, 1.0f) );

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedFightMinigame = true;
            globalState.GoToNextMinigame();
        }

        StartCoroutine(SwitchToRagdoll(5.0f));

        StartCoroutine(EndScene());
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds( 7.0f );

        StartCoroutine(FadeToColor(FadeOutColor, 0f));

        yield return new WaitForSeconds(2.0f);

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedFightMinigame = true;
            globalState.GoToNextMinigame();
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

    IEnumerator SwitchToRagdoll( float delay )
    {
        yield return new WaitForSeconds(delay);

        var blood = GameObject.Instantiate(BloodPrefab);
        blood.transform.position = HeadTransform.position;
        Destroy(blood, 2.0f);

        Agent.gameObject.SetActive(false);
        AgentRagdoll.gameObject.SetActive(true);
        AgentRagdoll.transform.position = Agent.transform.position;

        if (Audio)
            Audio.PlayOneShot(SniperShot);

        Player.GetComponent<Animator>().speed = 0;
    }
}
