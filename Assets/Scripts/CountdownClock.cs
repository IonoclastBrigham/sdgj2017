using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownClock : MonoBehaviour {

    public AudioSource Audio;
    public AudioClip Clip;
    public TextMesh Text;

    public WireCutterMinigame Minigame;

    public float Duration = 10f;

	// Use this for initialization
	void Start () {
        StartCoroutine(Countdown(Duration));	
	}

    public IEnumerator Countdown( float duration )
    {
        while (duration > 0)
        {
            PlayTick();

            duration -= 1.0f;

            yield return new WaitForSeconds(1.0f);

            UpdateText(duration);
        }


        if (Minigame)
            Minigame.TriggerEnd();
    }

    private void PlayTick()
    {
        if (Audio)
            Audio.PlayOneShot(Clip);
    }

    private void UpdateText( float seconds )
    {
        if (Text)
        {
            Text.text = string.Format("00:{0:00}", (int)seconds);
        }
    }
}
