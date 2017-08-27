using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireCutterMinigame : MonoBehaviour {



    public RectTransform WireCutter;

    public RectTransform[] Positions;

	// Use this for initialization
	void Start () {
        SetPosition(0);
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        if (x < 0)
        {
            SetPosition(0);

        } else if( x > 0 )
        {
            SetPosition(1);
        }

	}

    public void SetPosition( int index )
    {
        WireCutter.anchoredPosition = Positions [index].anchoredPosition;
    }
}
