using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float maxMana;
    public float currentMana;
    float manaRegen = 5;

    [HideInInspector]
    public bool isShielded = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        currentMana += manaRegen * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
    }

    public void TakeDamage(float dmg)
    {
        if (!isShielded)
        {
            currentHealth -= dmg * GameManager.Instance.damageTakenModifier;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
