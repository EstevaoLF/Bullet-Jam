using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreTxt;
    public int playerScore;

    Player player;

    [SerializeField]
    GameObject UpgradeCanvas;

    public float difficultyModifier = 1;
    public float damageModifier = 1;
    public float manaModifier = 1;
    public float damageTakenModifier = 1;

    //[HideInInspector]
    public int lifeModifierLvl, manaModifierLvl, increaseDamageModifierLvl, manCostLvl, damageTakenLvl;

    public int enemiesAlive;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerScore = 0;
        scoreTxt.text = playerScore.ToString();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    public void IncreaseScore(int score)
    {
        playerScore += score;
        scoreTxt.text = playerScore.ToString();
    }

    public void IncreaseHealth()
    {
        lifeModifierLvl++;
        player.maxHealth *= Mathf.Pow(1.1f, lifeModifierLvl);
    }
    public void IncreaseMana()
    {
        manaModifierLvl++;
        player.maxMana *= Mathf.Pow(1.1f, manaModifierLvl);
    }

    public void IncreaseDamage()
    {
        increaseDamageModifierLvl++;
        damageModifier *= Mathf.Pow(1.1f, increaseDamageModifierLvl);
    }

    public void ReduceManaCost()
    {
        manCostLvl++;
        manaModifier *= Mathf.Pow(0.9f, manCostLvl);
    }

    public void ReduceDamageTaken()
    {
        damageTakenLvl++;
        damageTakenModifier *= Mathf.Pow(0.9f, damageTakenLvl);
    }

    public void WaveCompleted()
    {
        Time.timeScale = 0f;
        difficultyModifier += 0.15f;
        UpgradeCanvas.SetActive(true);
    }

    public void UpgradeSelected()
    {
        Time.timeScale = 1.1f;
        UpgradeCanvas.SetActive(false);
    }

    void CheckIfClickOnGameObject()
    {

    }
}
