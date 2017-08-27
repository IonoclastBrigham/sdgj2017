using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float xMinConstraint = -5f;
    public float xMaxConstraint = 5f;
    public float steerSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        Move(x, Time.fixedDeltaTime);
	}

    private void Move( float xAxis, float deltaTime )
    {
        var pos = transform.position;
        pos.x += steerSpeed * xAxis * deltaTime;
        pos.x = Mathf.Clamp(pos.x, xMinConstraint, xMaxConstraint);
        transform.position = pos;
    }
}
