using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    public Transform[] SpawnPoints;
    public GameObject[] CarPrefab;

    public float MinSpeed = 3f;
    public float MaxSpeed = 10f;
    public float Duration = 15f;

    private float _runTime = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(CarSpawnerEngine());
	}
	
    void Update()
    {
        _runTime += Time.deltaTime;
    }

    IEnumerator CarSpawnerEngine()
    {
        while (true)
        {
            float t = Mathf.Clamp01( _runTime / Duration );
            float upperLimit = Mathf.Lerp(5, 0.5f, t);
            float lowerLimit = Mathf.Lerp(2, 0.5f, t);
            yield return new WaitForSeconds(Random.Range(lowerLimit, upperLimit));

            float upperNumCarLimit = 2;
            if (t > 0.75f)
                upperNumCarLimit = 4;
            else if (t > 0.5f)
                upperNumCarLimit = 3;

            int numCars = Random.Range(1, 2);
            var shuffledIndices = GetShuffledIndices();
            for(int i = 0; i < numCars; i++)
                SpawnCar( shuffledIndices[i] );
        }
    }

    private List<int> GetShuffledIndices()
    {
        List<int> fresh = new List<int>();
        List<int> result = new List<int>();
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            fresh.Add(i);
        }

        while (fresh.Count > 0)
        {
            int index = Random.Range(0, fresh.Count);
            result.Add(fresh [index]);
            fresh.RemoveAt(index);
        }

        return result;
    }


    private void SpawnCar( int spawnIndex )
    {
        var prefab = CarPrefab[Random.Range(0, CarPrefab.Length)];
        var obj = GameObject.Instantiate(prefab);
        obj.transform.parent = null;
        obj.transform.position = SpawnPoints [spawnIndex].position;

        var car = obj.AddComponent<NpcCar>();
        car.Speed = Mathf.Lerp(MinSpeed, MaxSpeed, _runTime / Duration);
        obj.tag = "Car";
    }
}
