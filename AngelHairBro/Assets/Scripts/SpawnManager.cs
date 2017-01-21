using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private List<GameObject> spawnPoints;

    //Tower obj here
    //

    int spIndex;
    int waveLenght;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;

    float nextSpawnTime;

    public bool devMode;
    bool isCamping;
    bool isDisabled;

    public event System.Action<int> OnNewWave;

    // Use this for initialization
    void Start () {

        NextWave();


    }
	
	// Update is called once per frame
	void Update () {

        if ((enemiesRemainingToSpawn > 0 /*|| currentWave.infinity*/) && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + 1;

            StartCoroutine("SpawnEnemy");
        }
    }

    public IEnumerable SpawnEnemy()
    {
        float spawnDelay = 0;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        //Crate and instansiate the enemy here
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        SelectSpawnPoint();
        Debug.Log("Wave: " + currentWaveNumber);

        if (currentWaveNumber - 1 < waveLenght)
        {
                                    // func that will incrace the wave size with 2.5 each new wave
            enemiesRemainingToSpawn = (int)(currentWaveNumber * 2 + ((currentWaveNumber * currentWaveNumber) / (2 * currentWaveNumber)));
            enemiesRemainingAlive = enemiesRemainingToSpawn;

            if (OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
        }
    }


    void SelectSpawnPoint()
    {
        spIndex = Random.Range(0, spawnPoints.Count - 1);
    }
}
