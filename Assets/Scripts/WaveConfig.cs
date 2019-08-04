using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Wave Config")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int totalEnemies = 1;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomness = 0.3f;
    [SerializeField] float moveSpeed = 3f;
    
    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public int GetTotalEnemies() { return totalEnemies; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomness() { return spawnRandomness; }

    public float GetMoveSpeed() { return moveSpeed; }

    
}
