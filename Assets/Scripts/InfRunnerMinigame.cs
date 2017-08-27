using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfRunnerMinigame : MonoBehaviour {

    public float CharacterMoveSpeed = 0;
    public Transform Character;

    private float _startX;

    void Awake()
    {
        _startX = Character.position.x;
    }

    void Update()
    {
        if (Character)
        {
            if (Character.position.x < _startX - 0.5f)
            {
                TriggerRagdoll();
            }
        }
    }

    void TriggerRagdoll()
    {
        
    }
}
