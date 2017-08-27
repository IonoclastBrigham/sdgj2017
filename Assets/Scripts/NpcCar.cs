using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCar : MonoBehaviour {
	
    public float Speed = 3;
    public float despawnAtZ = -7f;


	// Update is called once per frame
	void Update () {
        transform.Translate(Speed * Time.deltaTime * Vector3.back);

        if (transform.position.z < despawnAtZ)
            Destroy(gameObject);
	}
}
