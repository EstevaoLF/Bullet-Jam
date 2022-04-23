using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    Vector3 position;
    [SerializeField]
    GameObject meteorPrefab;

    WaitForSeconds seconds = new WaitForSeconds(0.125f);
    WaitForSeconds playerSeconds = new WaitForSeconds(0.5f);

    [SerializeField]
    Player player;
    [SerializeField]
    float targetPlayerTime;
    float countDown;

    Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        countDown = targetPlayerTime;
        StartCoroutine(SpawnMeteor());
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = targetPlayerTime;
            StartCoroutine(FireMeteorOnPlayer());
        }
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
    }

    IEnumerator SpawnMeteor()
    {
        for (int i = 0; i < 1000; i++)
        {
            Instantiate(meteorPrefab, GetSpawnPosition(), Quaternion.identity);
            yield return seconds;
        }
    }

    Vector3 GetSpawnPosition()
    {
        position.x = Random.Range(-35, 35);
        position.z = Random.Range(-35, 35);
        position.y = 0;
        return position;
    }

    IEnumerator FireMeteorOnPlayer()
    {        
        for (int i = 0; i < 3; i++)
        {
            Instantiate(meteorPrefab, playerPosition, Quaternion.identity);
            yield return playerSeconds;
        }
    }
}
