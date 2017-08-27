using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ObstacleData",  menuName="Obstacles")]
public class ObstacleSpawnData : ScriptableObject
{
	public GameObject[] Obstacles;
	public GameObject[] Cover;
	public GameObject[] Decorations;
}
