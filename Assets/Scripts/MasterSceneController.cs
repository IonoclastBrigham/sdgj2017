﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
        DontDestroyOnLoad(this);

        StartCoroutine( AutoLoad(1));
	}
	
    public IEnumerator AutoLoad( int index )
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(index);
    }

    public void StartWireScenario()
    {
        SceneManager.LoadScene("Scenes/BombDiffusal");
    }

    public void StartCarScenario()
    {
        SceneManager.LoadScene("Scenes/CarMinigame");
    }

    public void StartEnding()
    {
        SceneManager.LoadScene("Scenes/Ending");
    }
}
