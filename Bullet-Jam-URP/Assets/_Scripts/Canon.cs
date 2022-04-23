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
    ParticleSystem detonate;

    [SerializeField]
    Transform firePoint;
    WaitForSeconds timeBetweenAttacks = new WaitForSeconds(1);
    WaitForSeconds timeBeforeDetonate = new WaitForSeconds(3f);

    Collider[] playerCollider = new Collider[1];
    [SerializeField]
    float detonateDamage;
    [SerializeField]
    LayerMask whatIsPlayer;

    [SerializeField]
    float checkIfPlayerIsNearTime;
    float countDown;

    private void Start()
    {
        warming.Stop();
        detonate.Stop();
        countDown = checkIfPlayerIsNearTime;
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

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = checkIfPlayerIsNearTime;
            if (Physics.CheckSphere(transform.position, 10f, whatIsPlayer))
            {
                StartCoroutine(StartDetonate());
            }
        }

    }
    IEnumerator StartDetonate()
    {
        detonate.Simulate(0, false, true);
        detonate.Play();
        yield return timeBeforeDetonate;
        int player = Physics.OverlapSphereNonAlloc(transform.position, 5, playerCollider, whatIsPlayer);
        if (player > 0)
        {
            playerCollider[0].GetComponent<Player>().TakeDamage(detonateDamage * GameManager.Instance.difficultyModifier);
        }
    }
}
