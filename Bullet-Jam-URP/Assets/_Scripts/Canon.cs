using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;
    [SerializeField]
    ParticleSystem warming;

    [SerializeField]
    Transform firePoint;
    WaitForSeconds timeBetweenAttacks = new WaitForSeconds(1);
    WaitForSeconds timeBeforeDetonate = new WaitForSeconds(5f);

    Collider[] playerCollider = new Collider[1];
    [SerializeField]
    float detonateDamage;
    [SerializeField]
    LayerMask whatIsPlayer;

    private void Start()
    {
        warming.Stop();
        StartCoroutine(StartDetonate());
    }

    public void StartAttacking()
    {
        StartCoroutine(Attack());
    }

    public void StopAttacking()
    {
        StopCoroutine(Attack());
        warming.Stop();
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < 10; i++)
        {
            warming.Play();

            yield return timeBetweenAttacks;
            GameObject ball = Instantiate(ballPrefab, firePoint.position, transform.rotation);
            ball.GetComponent<Rigidbody>().velocity = (firePoint.forward * 50);
            warming.Simulate(0);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        StartCoroutine(StartDetonate());
    //    }
    //}
    IEnumerator StartDetonate()
    {        
        int player = Physics.OverlapSphereNonAlloc(transform.position, 10, playerCollider, whatIsPlayer);
        if (player > 0)
        {
            for (int i = 0; i < playerCollider.Length; i++)
            {
                playerCollider[i].GetComponent<Player>().TakeDamage(detonateDamage);
            }
        }
        yield return timeBeforeDetonate;
    }
}
