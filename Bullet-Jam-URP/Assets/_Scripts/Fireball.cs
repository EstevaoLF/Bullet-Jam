using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    bool isEnemySpell;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null && !isEnemySpell)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(go, 1f);
            Destroy(gameObject);            
        }
        if (isEnemySpell)
        {
            if (other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().TakeDamage(damage * GameManager.Instance.difficultyModifier);
                GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(go, 1f);
                Destroy(gameObject);
            }
        }
    }
}
