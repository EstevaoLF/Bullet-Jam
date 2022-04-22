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

    bool isSpawning;

    public float timeBetweenWaves;
    float countdownTime;

    [SerializeField]
    Transform player;
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
        if (!isSpawning && countdownTime <= 0 && currentwave <= waves.Length - 1)
        {
            StartCoroutine(SpawnWave(waves[currentwave]));
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
        if (currentwave <= waves.Length - 1)
        {
            currentwave++;
        }
        countdownTime = timeBetweenWaves;
    }

    void SpawnEnemy(Wave _wave)
    {
        int randomSpawnPoint = Random.Range(0, spawnPosition.Count);
        int randomEnemy = Random.Range(0, _wave.enemyPrefab.Count);

        Instantiate(_wave.enemyPrefab[randomEnemy], spawnPosition[randomSpawnPoint].transform.position, Quaternion.identity);
        if (_wave.enemyPrefab[randomEnemy].GetComponent<EnemyFollower>() != null)
        {
            _wave.enemyPrefab[randomEnemy].GetComponent<EnemyFollower>().player = player;
        }
        ////This is for debug only, should be removed later
        //if (!GameManager.Instance.enemies.Contains(enemySpawned.GetComponent<ITakeDamage>()))
        //{
        //    GameManager.Instance.enemies.Add(enemySpawned.GetComponent<ITakeDamage>());
        //}
    }

    void Countdown()
    {
        if (!isSpawning)
        {
            countdownTime -= Time.deltaTime;
        }
    }
}
