using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonManager : MonoBehaviour
{
    [SerializeField]
    List<Canon> canons = new List<Canon>();
    [SerializeField]
    List<Canon> usedCanons;

    float timeBetweenCanons = 4;
    float countDown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRandomCanons());
        countDown = timeBetweenCanons;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            StartCoroutine(GetRandomCanons());
            countDown = timeBetweenCanons;
        }
    }

    IEnumerator GetRandomCanons()
    {
        usedCanons = new List<Canon>(canons);
        int randomCanon = Random.Range(2, 4);
        for (int i = 0; i < randomCanon; i++)
        {
            int random = Random.Range(0, usedCanons.Count - 1);
            usedCanons[random].StartAttacking();
            usedCanons.Remove(usedCanons[random]);
            yield return new WaitForSeconds(1.6f);
        }
        yield return new WaitForSeconds(4);
    }
}
