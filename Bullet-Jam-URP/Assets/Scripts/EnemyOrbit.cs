using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbit : Enemy
{
    Transform orbitCenter;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        orbitCenter = GameObject.FindGameObjectWithTag("Orbit").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(orbitCenter.position, Vector3.up, moveSpeed * Time.deltaTime);
    }
}
