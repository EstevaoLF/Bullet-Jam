using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleCollision : MonoBehaviour
{
    [SerializeField]
    float damage = 10;

    ParticleSystem ps;
    [SerializeField]
    GameObject explosionPrefab;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.GetComponent<Enemy>()!= null)
        {
            other.GetComponent<Enemy>().TakeDamage(damage * GameManager.Instance.damageModifier);
            Instantiate(explosionPrefab, other.transform.position, transform.rotation);
        }       
    }
}
