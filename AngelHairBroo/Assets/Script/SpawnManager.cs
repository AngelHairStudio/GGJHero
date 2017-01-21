using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {


    [SerializeField]
    private List<GameObject> spawnPoints;

    public Enemy enemy;
    //Tower obj here
    //

    int spIndex;
    int waveLenght;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    public Text m_waveInfo;

    float nextSpawnTime;

    public bool devMode;
    bool isCamping;
    bool isDisabled;

    public event System.Action<int> OnNewWave;

    // Use this for initialization
    void Start()
    {
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if ((enemiesRemainingToSpawn > 0 /*|| currentWave.infinity*/) && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + 1;

            SpawnEnemy();
            //StartCoroutine("SpawnEnemy");
        }

        if (devMode)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //StopCoroutine("SpawnEnemy");
                foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                {
                    GameObject.Destroy(enemy.gameObject);
                }
                NextWave();
            }
        }
    }

    public void SpawnEnemy()
    {
        float spawnDelay = 1;
        float spawnTimer = 0;
        Transform spawnpos = spawnPoints[spIndex].transform;
        while (spawnTimer < spawnDelay)
        {
            spawnTimer += Time.deltaTime;
            //yield return null;
        }

        //Crate and instansiate the enemy here
        Enemy spawnedEnemy = Instantiate(enemy, spawnpos.position, Quaternion.identity) as Enemy;
        spawnedEnemy.onDeath += OnEnemyDeath;
        spawnedEnemy.SetCharacteristics(1, 1, 2);

        Debug.Log("Enemy created");
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

        // func that will incrace the wave size with 2.5 each new wave
        enemiesRemainingToSpawn = (int)(currentWaveNumber * 2 + ((currentWaveNumber * currentWaveNumber) / (2 * currentWaveNumber)));
        enemiesRemainingAlive = enemiesRemainingToSpawn;

        if (OnNewWave != null)
        {
            OnNewWave(currentWaveNumber);
        }
        m_waveInfo.text = currentWaveNumber.ToString();
        
    }


    void SelectSpawnPoint()
    {
        spIndex = Random.Range(0, spawnPoints.Count);
    }
}