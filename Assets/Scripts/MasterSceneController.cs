using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneToLoad
{
    BombDiffusal,
    CarMinigame,
    FightScene,
    SniperRunner,
    QTEScene

}

public class MasterSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
        DontDestroyOnLoad(this);

	}

    public void Begin()
    {

        StartCoroutine( AutoLoad(1));
    }
	
    public IEnumerator AutoLoad( int index )
    {
        yield return new WaitForSeconds(1);

        StartWireScenario();
    }

    public void StartWireScenario()
    {
        LoadIntro(SceneToLoad.BombDiffusal);
    }

    public void StartRunnerScenario()
    {
        LoadIntro(SceneToLoad.SniperRunner);
    }

    public void StartFightScenario()
    {
        LoadIntro(SceneToLoad.FightScene);
    }

    public void StartQTEScenario()
    {
        LoadIntro(SceneToLoad.QTEScene);
    }

    public void StartCarScenario()
    {
        LoadIntro(SceneToLoad.CarMinigame);
    }

    public void StartEnding()
    {
        SceneManager.LoadScene("Scenes/Ending");
    }

    public void LoadIntro( SceneToLoad scene )
    {
        switch (scene)
        {
            case SceneToLoad.BombDiffusal:
                SceneManager.LoadScene("Scenes/IntroToBriefcase");
                return;

            case SceneToLoad.CarMinigame:
                SceneManager.LoadScene("Scenes/IntroToCar");
                return;

            case SceneToLoad.FightScene:
                SceneManager.LoadScene("Scenes/IntroToFight");
                return;

            case SceneToLoad.QTEScene:
                SceneManager.LoadScene("Scenes/IntroToQTE");
                return;

            case SceneToLoad.SniperRunner:
                SceneManager.LoadScene("Scenes/IntroToRunner");
                return;
        }
    }

    public void LoadScene( SceneToLoad scene )
    {
        switch (scene)
        {
            case SceneToLoad.BombDiffusal:
                SceneManager.LoadScene("Scenes/BombDiffusal");
                return;

            case SceneToLoad.CarMinigame:
                SceneManager.LoadScene("Scenes/CarMinigame");
                return;

            case SceneToLoad.FightScene:
                SceneManager.LoadScene("Scenes/FightScene");
                return;

            case SceneToLoad.QTEScene:
                SceneManager.LoadScene("Scenes/QTEScene");
                return;

            case SceneToLoad.SniperRunner:
                SceneManager.LoadScene("Scenes/SniperRunner");
                return;
        }
    }
}
