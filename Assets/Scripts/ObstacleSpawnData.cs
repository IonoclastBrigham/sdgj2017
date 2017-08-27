using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ObstacleData",  menuName="Obstacles")]
public class ObstacleSpawnData : ScriptableObject {
     
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject Prefab;
    }

    public SpawnEntry[] Obstacles;
    public SpawnEntry[] Cover;
    public SpawnEntry[] Decorations;
}
