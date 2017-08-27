using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float xMinConstraint = -5f;
    public float xMaxConstraint = 5f;
    public float steerSpeed = 3.0f;

    public Transform[] Lanes;

    public ScreenShaker ScreenShaker;
    public AudioSource Audio;
    public AudioClip CarSmash;

    public GameObject ExplosionPrefab;

    private int _laneIndex = 0;
    private float _smoothVelocity;
    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _laneIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        if ( Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            _laneIndex = Mathf.Max(0, _laneIndex - 1);
        } else if ( Input.GetKeyDown( KeyCode.RightArrow))
        {
            _laneIndex = Mathf.Min(_laneIndex + 1, Lanes.Length - 1); 
        }

        TweenToLane();
	}

    private void TweenToLane()
    {
        var rbody = GetComponent<Rigidbody>();
        var lanePos = Lanes [_laneIndex].position;
        var x = lanePos.x;

        var pos = transform.position;
        pos.x = Mathf.SmoothDamp(pos.x, x, ref _smoothVelocity, 0.15f);
        //transform.position = pos;
        _rigidbody.MovePosition(pos);
    }

    private void Move( float xAxis, float deltaTime )
    {
        var pos = transform.position;
        pos.x += steerSpeed * xAxis * deltaTime;
        pos.x = Mathf.Clamp(pos.x, xMinConstraint, xMaxConstraint);
       
        _rigidbody.MovePosition(pos);
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
