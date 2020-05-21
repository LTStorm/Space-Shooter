using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    //Parameters
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private bool loop = false;
    [SerializeField] private int curWave = 0;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        do
        {
            yield return StartCoroutine(SpawmAllWaves());
        } while (loop);
    }
    //Hàm được sử dụng để tạo ra tất cả các wave trong màn chơi
    private IEnumerator SpawmAllWaves()
    {
        while (loop)
        {
            curWave = Random.Range(0, waveConfigs.Count - 1);//Random wave nào sẽ được tạo ra
            yield return StartCoroutine(SpawnEnemiesInWave(waveConfigs[curWave]));//Tạo các kẻ địch di chuyển trong wave đó
        }

    }
    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i=0;i<waveConfig.GetNumberOfEnemies();i++)
        {
            var newEnemy=Instantiate(waveConfig.GetEnemyPrefabs(), waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);//Tạo ra kẻ địch tại vị trí bắt đầu của wave
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);//Set đường đi cho kẻ địch
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

}
