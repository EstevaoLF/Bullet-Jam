using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : Enemy
{
    public GameObject player;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //if (player == null)
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            player = FindObjectOfType<Player>().gameObject;
        }
    }
}
