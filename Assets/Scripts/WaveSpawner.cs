using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;

    private int startingWaveIndex = 0;

    void Start()
    {
        var currentWave = waveConfigs[startingWaveIndex];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for(int i = 0; i < currentWave.GetTotalEnemies(); i++)
        {
            Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }
}
