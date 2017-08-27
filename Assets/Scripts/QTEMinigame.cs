using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEMinigame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedQTEMinigame = true;
            globalState.GoToNextMinigame();
        }

	}
}
