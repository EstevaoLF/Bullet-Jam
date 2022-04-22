using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Image healthFill;
    [SerializeField]
    Image manaFill;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, player.currentHealth / player.maxHealth, 0.1f);
        manaFill.fillAmount = Mathf.Lerp(manaFill.fillAmount, player.currentMana / player.maxMana, 0.1f);
    }
}
