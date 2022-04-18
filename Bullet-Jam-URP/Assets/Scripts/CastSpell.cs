using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField]
    GameObject frozenOrb;

    [SerializeField]
    Transform spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject orb = Instantiate(frozenOrb, spawnPosition.position, transform.rotation);
            orb.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        }
    }
}
