using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public Transform[] Lanes;
	public ObstacleSpawnData SpawnData;

    public InfRunnerMinigame Minigame;

    private List<GameObject> _instances;
    private float _nextSpawnDistance = 0;

    void Awake()
    {
        _instances = new List<GameObject>();
    }


	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnScheduler());
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = _instances.Count - 1; i >= 0; i--)
        {
            var pos = _instances [i].transform.position;
            pos.x -= Minigame.CharacterMoveSpeed * Time.fixedDeltaTime;

            if (pos.x < -15f)
            {
                Destroy(_instances [i]);
                _instances.RemoveAt(i);
                continue;
            }
            _instances [i].transform.position = pos;
        }

        _nextSpawnDistance -= Minigame.CharacterMoveSpeed * Time.fixedDeltaTime;
	}

    private GameObject SpawnObject( GameObject prefab, int lane )
    {
        var instance = GameObject.Instantiate(prefab);
        instance.transform.position = Lanes [lane].position;
        _instances.Add(instance);
        return instance;
    }

    IEnumerator SpawnScheduler()
    {
        while (true)
        {
            if (ShouldSpawnObject())
            {
                int lane = GetRandomLane();
                SpawnObject(GetRandomObjPrefab(lane), lane);

                _nextSpawnDistance = Random.Range(3.5f, 6);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private bool ShouldSpawnObject()
    {
		if (SpawnData.Obstacles.Length == 0)
            return false;
        
        return _nextSpawnDistance < 0;
    }

    private GameObject GetRandomObjPrefab( int lane )
    {
        if (lane == 0)
            return SpawnData.Obstacles [Random.Range(0, SpawnData.Obstacles.Length)];
        else if (lane == 1)
            return SpawnData.Cover [Random.Range(0, SpawnData.Cover.Length)];
        else
            return SpawnData.Decorations [Random.Range(0, SpawnData.Decorations.Length)];
    }

    private int GetRandomLane()
    {
        return Random.Range(0, 3);
    }
}
