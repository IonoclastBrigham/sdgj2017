using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireCutterMinigame : MonoBehaviour {

    public AudioSource Audio;
    public AudioClip Explosion_Nuke;
    public AudioClip Explosion_Dud;


    public Sprite Uncut;
    public Sprite Cut;
    public Image[] Wires;

    public RectTransform WireCutter;

    public RectTransform[] Positions;

    private int _index;
    private bool _actionSelected = false;
    private GlobalState _globalState;
    private bool _succeeded = false;

	// Use this for initialization
	void Start () {
        _succeeded = false;
        _globalState = GameObject.FindObjectOfType<GlobalState>();
        SetPosition(0);
	}
	
	// Update is called once per frame
	void Update () {

        if (_actionSelected )
            return;

        float x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            SetPosition(0);

        } else if( x > 0 )
        {
            SetPosition(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CutAtPosition();
        }

	}

    private void CutAtPosition()
    {
        Wires [_index].sprite = Cut;
        _actionSelected = true;

        if (_globalState == null)
            return;

        if (_index != _globalState.CorrectWire)
        {
            _succeeded = false;
        } else
        {
            _succeeded = true;
        }

        if (_globalState.FirstSelectedWireIndex < 0)
            _globalState.CorrectWire = _index == 0 ? 1 : 0;
    }

    public void SetPosition( int index )
    {
        _index = index;
        WireCutter.anchoredPosition = Positions [index].anchoredPosition;
    }

    private void PlayFailure()
    {
        if (Audio)
            Audio.PlayOneShot(Explosion_Nuke);
    }

    private void PlaySuccess()
    {
        if (Audio)
            Audio.PlayOneShot(Explosion_Dud);
    }

    public void TriggerEnd()
    {
        if (_succeeded)
            PlaySuccess();
        else
            PlayFailure();
    }
}
