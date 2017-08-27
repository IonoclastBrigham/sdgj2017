using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTween : MonoBehaviour {

    public Transform StartPos;
    public Transform End;
    public float Duration = 5.0f;

    private float _startTime;


	// Use this for initialization
	void Start () {
        _startTime = Time.time;
        transform.position = StartPos.position;
        transform.rotation = Quaternion.LookRotation(( End.position - StartPos.position).normalized );
	}
	
	// Update is called once per frame
	void Update () {

        float t = Mathf.Clamp01( ( Time.time - _startTime ) / Duration);
        transform.position = Vector3.Lerp( StartPos.position, End.position, t);
	}

    void OnDrawGizmos()
    {
        if (!StartPos)
            return;

        if (!End)
            return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(StartPos.position, End.position);
    }
}
