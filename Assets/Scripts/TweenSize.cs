using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenSize : MonoBehaviour {

    public Transform target;
    public Vector3 StartSize;
    public Vector3 EndSize;
    public AnimationCurve TweenCurve;
    public float Duration = 3;

    private float _time;

	// Update is called once per frame
	void Update () {

        if (!target)
            return;
        
        _time += Time.deltaTime;

        float t = Mathf.Clamp01 ( _time / Duration );

        if (TweenCurve != null && TweenCurve.keys.Length > 1)
        {
            t = TweenCurve.Evaluate(t);
        }

        target.localScale = Vector3.Lerp(StartSize, EndSize, t);
	}
}
