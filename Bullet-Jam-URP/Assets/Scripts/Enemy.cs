using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    public float currentHealth;
    public float MaxHealth;

    public float moveSpeed;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmg;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
