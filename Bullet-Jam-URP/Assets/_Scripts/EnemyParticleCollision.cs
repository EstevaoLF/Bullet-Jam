using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{
    [SerializeField]
    float damage = 10;

    [SerializeField]
    GameObject explosion;
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().TakeDamage(damage * GameManager.Instance.difficultyModifier);
        }
        if (explosion != null)
        {
            Vector3 position = other.GetComponent<Collider>().ClosestPoint(transform.position);
            Instantiate(explosion, position, Quaternion.identity);
        }
    }
}
