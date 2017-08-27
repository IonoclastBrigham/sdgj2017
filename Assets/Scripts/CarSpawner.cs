using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    public Transform[] SpawnPoints;
    public GameObject[] CarPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(CarSpawnerEngine());
	}
	

    IEnumerator CarSpawnerEngine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        var prefab = CarPrefab[Random.Range(0, CarPrefab.Length)];
        var obj = GameObject.Instantiate(prefab);
        obj.transform.parent = null;
        obj.transform.position = SpawnPoints [Random.Range(0, SpawnPoints.Length)].position;

        var trigger = obj.AddComponent<BoxCollider>();
        trigger.size = new Vector3(1, 1, 3);

        obj.AddComponent<NpcCar>();
        obj.tag = "Car";
    }
}
