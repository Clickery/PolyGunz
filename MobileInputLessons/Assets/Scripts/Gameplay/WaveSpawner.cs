using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5.0f;
    private float waveCountDown;

    private float searchCountdown = 1.0f;

    private SpawnState state = SpawnState.COUNTING;

    private float difficulty = 1.0f;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {

        if(state == SpawnState.WAITING)
        {
            if(isWaveCleared())
            {
                //wave cleared, start new wave
                waveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //start spawning here
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
        
    }

    bool isWaveCleared()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1.0f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return true;
            }
        }
        return false;
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////
    
    private void waveCompleted()
    {
        Debug.Log("Wave Completed!!");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //this is where you can put upgrade scene
            //either next scene or pause
            nextWave = 0;//change to next scene
            Debug.Log("Level Completed, Looping");
        }
        else
        {
            difficulty += 0.1f;
            nextWave++;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i < _wave.count; i++)
        {
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    private void spawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform currentEnemy = Instantiate(_enemy, _sp.position, _sp.rotation);
        if(currentEnemy.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            currentEnemy.gameObject.GetComponent<EnemyBehavior>().increaseStats(difficulty);
        }
        else
        {
            Debug.LogError("Object does not have 'Enemy Behavior Component'!");
        }
        
    }
}
