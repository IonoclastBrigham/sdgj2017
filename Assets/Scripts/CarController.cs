using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float xMinConstraint = -5f;
    public float xMaxConstraint = 5f;
    public float steerSpeed = 3.0f;

    public ScreenShaker ScreenShaker;
    public AudioSource Audio;
    public AudioClip CarSmash;

    public GameObject ExplosionPrefab;

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
        var rbody = GetComponent<Rigidbody>();

        var pos = transform.position;
        pos.x += steerSpeed * xAxis * deltaTime;
        pos.x = Mathf.Clamp(pos.x, xMinConstraint, xMaxConstraint);
       
        rbody.MovePosition(pos);
    }

    void OnTriggerEnter( Collider other )
    {
        if (other.CompareTag("Car"))
        {
            SpawnExplosion( other.transform );
            Destroy(other.gameObject);
        }
    }

    public void SpawnExplosion( Transform other )
    {
        StartCoroutine(SpawnExplosions(other));
    }

    IEnumerator SpawnExplosions( Transform other )
    {
        ScreenShaker.StartShakin();
        Vector3 position = other.position;
        int count = Random.Range(3, 6);
        while (count > 0)
        {
            if (ExplosionPrefab)
            {
                var offset = 1.5f * Random.onUnitSphere;
                offset.y = 0;

                var obj = GameObject.Instantiate(ExplosionPrefab);
                obj.transform.position = position + 0.5f * Vector3.up + offset;
                Destroy(obj, 1.0f);
            }

            if (Audio)
                Audio.PlayOneShot(CarSmash);

            count--;
            yield return new WaitForSeconds(0.1f);
        }

        ScreenShaker.StopShakin();
    }
}
