using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =  "EnemyWaveConfig")]
//Class này dùng để chứa và gửi đi các thông tin về Wave 
public class WaveConfig : ScriptableObject
{

    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private GameObject pathPrefabs;
    [SerializeField] private float spawnRandomFactor;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float moveSpeed;

    public GameObject GetEnemyPrefabs(){return enemyPrefabs;}

    public List<Transform> GetWayPoints()
    {
        var wayPoints= new List<Transform>();
        foreach (Transform child in pathPrefabs.transform)
        {
            wayPoints.Add(child);
        }
        return wayPoints;
    }
    public float GetSpawnRandomFactor(){return spawnRandomFactor;}
    public float GetTimeBetweenSpawn(){return timeBetweenSpawn;}
    public int GetNumberOfEnemies(){return numberOfEnemies;}
    public float GetMoveSpeed(){return moveSpeed;}
}
