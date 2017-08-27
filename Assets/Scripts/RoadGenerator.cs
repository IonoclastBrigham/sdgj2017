using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    public float zSize = 20;
    public GameObject RoadPiecePrefab;
    public int preAllocNum = 5;
    public float Speed = 30;

    private GameObject[] _instances;

    void Awake()
    {
        _instances = new GameObject[preAllocNum];

        for (int i = 0; i < preAllocNum; i++)
        {
            _instances [i] = CreateRoadPiece();
            _instances [i].transform.position = new Vector3(0, 0, i * zSize);
        }
    }

    void Update()
    {
        for (int i = 0; i < _instances.Length; i++)
        {
            var pos = _instances [i].transform.position;
            pos.z -= Speed * Time.deltaTime;
            if (pos.z < -30f)
                pos.z += _instances.Length * zSize;

            _instances [i].transform.position = pos;
        }
    }

    GameObject CreateRoadPiece()
    {
        var obj = GameObject.Instantiate(RoadPiecePrefab);
        obj.transform.position = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        return obj;
    }
}
