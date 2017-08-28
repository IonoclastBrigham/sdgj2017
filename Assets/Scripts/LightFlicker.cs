using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public Light light;
	
	// Update is called once per frame
	void Update () {
        light.intensity = 0.9f + 0.1f * Mathf.Sin(15.0f * Time.time);
	}
}
