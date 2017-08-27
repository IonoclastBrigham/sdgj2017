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
            if (Minigame.Running)
            {
                if (CanSeePlayer())
                {
                    _offset.x = 0;
                    _offset.y = 0;
                    float projectedT = GetCharacterProjectionToLine();
                    _moveT = Mathf.SmoothDamp(_moveT, projectedT, ref _smoothVelocity, 0.15f);
                } else
                {
                    _moveT = Mathf.SmoothDamp(_moveT, 1.0f, ref _smoothVelocity, 1.0f);
                }
            } else
            {
                _offset.x = 0;
                _offset.y = 0;
                float projectedT = GetCharacterProjectionToLine();
                _moveT = Mathf.SmoothDamp(_moveT, projectedT, ref _smoothVelocity, 0.05f); 
            }
        } else
        {
            _moveT = Mathf.SmoothDamp(_moveT, 0.0f, ref _smoothVelocity, 1.0f);
        }

        var pos = transform.position;
        pos = Vector3.Lerp(LeftLimit.position, RightLimit.position, _moveT);
        pos += _offset;
        transform.position = pos;
	}

    private float GetCharacterProjectionToLine()
    {
        var charPos = Minigame.Player.position + Vector3.up * 0.25f;

        var scrSpaceChar = Camera.main.WorldToScreenPoint(charPos);
        var left = Camera.main.WorldToScreenPoint(LeftLimit.position);
        var right = Camera.main.WorldToScreenPoint(RightLimit.position);

        var dist = Vector3.Distance(left, right);
        var d = scrSpaceChar.x - left.x;
        return d / dist;
    }

    private bool CanSeePlayer()
    {
        var camPoint = Camera.main.transform.position;
        var rayDir = Minigame.Player.position + (Vector3.up * 0.75f) - camPoint;
        rayDir.Normalize();

        RaycastHit hit;
        if (Physics.Raycast(camPoint, rayDir, out hit, 20.0f))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }

        return false;
    }

    void OnDrawGizmos()
    {
        if (!LeftLimit || !RightLimit)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(LeftLimit.position, RightLimit.position);
    }
}
