using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfRunnerMinigame : MonoBehaviour {

    public float CharacterMoveSpeed = 0;
    public Transform Character;
    public GameObject Ragdoll;

    private float _startX;
    private bool _running = false;

    void Awake()
    {
        _running = true;
        _startX = Character.position.x;
    }

    void Update()
    {
        if (_running)
            return;
        
        if (Character)
        {
            if (Character.position.x < _startX - 0.5f)
            {
                TriggerRagdoll();
                _running = false;
            }
        }
    }

    void TriggerRagdoll()
    {
        Character.gameObject.SetActive(false);
        Ragdoll.gameObject.SetActive(true);

        Ragdoll.transform.position = Character.transform.position;

        StartCoroutine(EndMinigame(2.0f, false));
    }

    IEnumerator EndMinigame( float delay, bool success )
    {
        yield return new WaitForSeconds(delay);

        var globalState = GameObject.FindObjectOfType<GlobalState>();
        globalState.CompletedRunnerMinigame = success;
        globalState.GoToNextMinigame();
    }
}
