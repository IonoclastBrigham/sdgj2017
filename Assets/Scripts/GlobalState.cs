using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour {

    public int FirstSelectedWireIndex = -1;
    public int CorrectWire = -1;

    public bool CompletedWireMinigame = false;
    public bool CompletedCarMinigame = false;
    public bool CompletedRunnerMinigame = false;
    public bool CompletedFightMinigame = false;
    public bool CompletedQTEMinigame = false;


    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    public void GoToNextMinigame()
    {
        if (!CompletedWireMinigame)
            GetComponent<MasterSceneController>().StartWireScenario();
        else if (!CompletedRunnerMinigame)
            GetComponent<MasterSceneController>().StartRunnerScenario();
        else if (!CompletedFightMinigame)
            GetComponent<MasterSceneController>().StartFightScenario();
        else if (!CompletedCarMinigame)
            GetComponent<MasterSceneController>().StartCarScenario();
        else if (!CompletedQTEMinigame)
            GetComponent<MasterSceneController>().StartQTEScenario();
        else
            GetComponent<MasterSceneController>().StartEnding();
    }
}
