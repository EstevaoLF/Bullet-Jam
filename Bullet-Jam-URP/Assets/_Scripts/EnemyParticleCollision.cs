using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleCollision : MonoBehaviour
{
    [SerializeField]
    float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
