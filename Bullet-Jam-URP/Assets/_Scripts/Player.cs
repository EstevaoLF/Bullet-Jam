using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float maxMana;
    public float currentMana;
    float manaRegen = 5;

    AudioSource audio;
    [SerializeField]
    AudioClip clip;

    [HideInInspector]
    public bool isShielded = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        audio = GetComponent<AudioSource>();
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
            audio.PlayOneShot(clip);
        }
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
