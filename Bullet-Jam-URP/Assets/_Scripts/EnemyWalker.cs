using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy
{
    Vector3 movePosition;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GetNextMovePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - movePosition).magnitude < 3)
        {
            GetNextMovePosition();
        }
        else
        {
            agent.SetDestination(movePosition);
        }
    }

    Vector3 GetNextMovePosition()
    {
        movePosition = new Vector3();
        movePosition.x = Random.Range(-35, 35);
        movePosition.z = Random.Range(-35, 35);
        movePosition.y = transform.position.y;
        return movePosition;
    }
}
