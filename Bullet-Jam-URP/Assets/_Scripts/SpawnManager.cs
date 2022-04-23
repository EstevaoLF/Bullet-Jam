using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Enemies
    {
        public GameObject prefab;
        public string tag;
    }
    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemyPrefab = new List<GameObject>();

        public float timeBetweenEnemies;

        public int numberOfEnemiesPerWave;
    }

    public Wave[] waves;
    [SerializeField]
    List<Transform> spawnPosition = new List<Transform>();

    [HideInInspector]
    public int currentwave = 0;

    bool isSpawning, isWaiting;

    public float timeBetweenWaves;
    float countdownTime;

    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPosition == null)
        {
            Debug.Log("No spawn points or waypoints available");
        }
        StartCoroutine(SpawnWave(waves[currentwave]));
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();

        if (!isSpawning && countdownTime <= 0 && currentwave <= waves.Length - 1 && !isWaiting)
        {
            StartCoroutine(SpawnWave(waves[currentwave]));
        }
        if (isWaiting && GameManager.Instance.enemiesAlive == 0)
        {
            isWaiting = false;
            GameManager.Instance.WaveCompleted();
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        isSpawning = true;
        for (int i = 0; i < _wave.numberOfEnemiesPerWave; i++)
        {
            SpawnEnemy(_wave);
            yield return new WaitForSeconds(_wave.timeBetweenEnemies);
        }
        isSpawning = false;
        if (currentwave >= waves.Length - 1)
        {            
            currentwave = 0;
            isWaiting = true;
            yield break;
        }
        else
        {
            currentwave++;
        }
        countdownTime = timeBetweenWaves;
        Debug.Log(currentwave + " currentwave");
        Debug.Log(waves.Length - 1 + " waves");

    }

    void SpawnEnemy(Wave _wave)
    {
        int randomSpawnPoint = Random.Range(0, spawnPosition.Count);
        int randomEnemy = Random.Range(0, _wave.enemyPrefab.Count);

        Instantiate(_wave.enemyPrefab[randomEnemy], spawnPosition[randomSpawnPoint].transform.position, Quaternion.identity);
        if (_wave.enemyPrefab[randomEnemy].GetComponent<EnemyFollower>())
        {
            _wave.enemyPrefab[randomEnemy].GetComponent<EnemyFollower>().player = player;
        }
    }

    void Countdown()
    {
        if (!isSpawning)
        {
            countdownTime -= Time.deltaTime;
        }
    }
}
