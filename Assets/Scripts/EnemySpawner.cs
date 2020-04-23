using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    int startingWave = 0;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int i = startingWave; i < waveConfigs.Count; i++)
        {
            WaveConfig currentWave = waveConfigs[i];

            yield return StartCoroutine(SpawnEnemyWave(currentWave));
            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator SpawnEnemyWave(WaveConfig currentWave)
    {
        for(int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            var newEnemy = Instantiate(currentWave.EnemyPrefab, currentWave.GetWayPoints()[0].position, Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);            
        }
    }
}
