using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    Vector3 position;
    [SerializeField]
    GameObject meteorPrefab;

    WaitForSeconds seconds = new WaitForSeconds(0.125f);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMeteor());
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
        position.x = Random.Range(-24, 24);
        position.z = Random.Range(-24, 24);
        position.y = 0;
        return position;
    }
}
