using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour {

    public int FirstSelectedWireIndex = -1;
    public int CorrectWire = -1;

    public bool CompletedWireMinigame = false;
    public bool CompletedCarMinigame = false;


    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    public void GoToNextMinigame()
    {
        if (!CompletedWireMinigame)
            GetComponent<MasterSceneController>().StartWireScenario();
        else if (!CompletedCarMinigame)
            GetComponent<MasterSceneController>().StartCarScenario();
        else
            GetComponent<MasterSceneController>().StartEnding();
    }
}
