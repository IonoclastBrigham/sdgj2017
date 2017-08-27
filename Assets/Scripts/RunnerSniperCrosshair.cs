using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSniperCrosshair : MonoBehaviour {

    private float _xStart;
    private Vector3 _offset;

    public InfRunnerMinigame Minigame;
    public Transform LeftLimit;
    public Transform RightLimit;

    public float wanderRadius = 1.0f;
    public float wanderSpeed = 1.0f;

    private float _smoothVelocity;
    private float _moveT;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Minigame)
            return;

        _offset.x = wanderRadius * Mathf.Sin(wanderSpeed * Time.time);
        _offset.y = wanderRadius * Mathf.Cos(wanderSpeed * Time.time);

        if (Minigame.CharacterMoveSpeed < 1.0f)
        {
            _moveT = Mathf.SmoothDamp(_moveT, 1.0f, ref _smoothVelocity, 1.0f);
        } else
        {
            _moveT = Mathf.SmoothDamp(_moveT, 0.0f, ref _smoothVelocity, 1.0f);
        }

        var pos = transform.position;
        pos = Vector3.Lerp(LeftLimit.position, RightLimit.position, _moveT);
        pos += _offset;
        transform.position = pos;
	}

    void OnDrawGizmos()
    {
        if (!LeftLimit || !RightLimit)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(LeftLimit.position, RightLimit.position);
    }
}
