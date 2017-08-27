using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMinigame : MonoBehaviour {

    // Use this for initialization
    void Start () {

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        if (globalState)
        {
            globalState.CompletedFightMinigame = true;
            globalState.GoToNextMinigame();
        }

    }
}
