using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [System.Serializable]
    public class Wave
    {
        public string name;
        [System.Serializable]
        public struct wavePoint
        {
            public Transform enemy;
            public Transform spawnPoint;
            public int count;
        }
        public float rate;
        public wavePoint[] wavePoints;
    }
    public GameObject timerUIprefab;
    public Wave[] waves;
    public Transform[] SpawnPoints;
    public int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchAliveCoutdown = 1f;

    public GameObject levelCompletePrefab;
    public GameObject waveCompletePrefab;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        SpawnTimerUIs();

    }

    void SpawnTimerUIs()
    {
        Wave upcoming = waves[nextWave];
        foreach (var wavepoint in upcoming.wavePoints)
        {
            GameObject timer = Instantiate(timerUIprefab, new Vector3(0,0,0), Quaternion.identity);
            timer.transform.GetChild(0).GetComponent<FixedScaleUI>().point = wavepoint.spawnPoint;
            Destroy(timer, timeBetweenWaves + 0.5f);
        }
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //check if enemies are still alive
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                Debug.Log("Wave Completed!");
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0f)
        {
            if(state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchAliveCoutdown -= Time.deltaTime;
        if (searchAliveCoutdown <= 0f)
        {
            searchAliveCoutdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //find max count in wavePoints
        int maxCount = 0;
        for(int i = 0; i < _wave.wavePoints.Length; i++)
        {
            if (_wave.wavePoints[i].count > maxCount)
                maxCount = _wave.wavePoints[i].count;
        }

        for(int i = 0; i < maxCount; i++)
        {
            for (int j = 0; j < _wave.wavePoints.Length; j++)
            {
                if(i < _wave.wavePoints[j].count)
                    Instantiate(_wave.wavePoints[j].enemy, _wave.wavePoints[j].spawnPoint.position, _wave.wavePoints[j].spawnPoint.rotation);
            }
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    /*
    void SpawnEnemy(Wave _wave)
    {
        //Spawning Enemy
        Debug.Log("Spawning enemy: " + _wave.enemy.name);
        Instantiate(_wave.enemy, _wave.spawnPoint.position, _wave.spawnPoint.rotation);

    }*/

    void WaveCompleted()
    {
        
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            GameObject instance = Instantiate(levelCompletePrefab, Vector3.zero, Quaternion.identity);
            //nextWave = 0;
            Debug.Log("All waves complete. Looping");
            //TO DO: SHOW LEVEL COMPLETE MESSGE THEN DO NEXT LEVEL
        }
        else
        {
            GameObject instance = Instantiate(waveCompletePrefab, Vector3.zero, Quaternion.identity);
            Destroy(instance, 3f);
            nextWave++;
            SpawnTimerUIs();
        }

    }
}
