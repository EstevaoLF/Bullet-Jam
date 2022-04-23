using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public float moveSpeed;
    [HideInInspector]
    public NavMeshAgent agent;

    public event Action<float, float> OnEnemyHealthChanged;
    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed *= GameManager.Instance.difficultyModifier;
        maxHealth *= GameManager.Instance.difficultyModifier;
        currentHealth = maxHealth;
        GameManager.Instance.enemiesAlive++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnEnemyHealthChanged?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.IncreaseScore((int)(10 * GameManager.Instance.difficultyModifier));
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
            GameManager.Instance.enemiesAlive--;
    }
}
