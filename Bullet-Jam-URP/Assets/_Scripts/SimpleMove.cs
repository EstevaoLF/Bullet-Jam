using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SimpleMove : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    float speed = 3.0F;

    [SerializeField]
    float faceMoveDirectionSpeed;

    Rigidbody rb;
    public LayerMask ground;

    Camera cam;
    Vector3 direction;

    NavMeshAgent agent;
    private void Start()
    {
        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {

        Move();
        //AnimationControl();
    }

    void Move()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        ////Multiply by cam.transform to make character walk direction relative to the camera

        //Vector3 movementX = vertical * cam.transform.forward;
        //movementX.y = 0;

        //Vector3 movementZ = horizontal * cam.transform.right;
        //movementZ.y = 0;

        //direction = movementX + movementZ;


        ////if statement prevents player from facing different direction if no key is being pressed       
        //if (direction.magnitude != 0)
        //{
        //    FaceMoveDirection();
        //    rb.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
        //}
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground))
            {
                agent.SetDestination(hitInfo.point);
                agent.speed = speed;
                anim.SetBool("Running", true);
            }
        }
        if (agent.velocity == Vector3.zero)
        {
            anim.SetBool("Running", false);
        }
    }

    private void FaceMoveDirection()
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, faceMoveDirectionSpeed * Time.deltaTime);
    }

    private void FaceMoveDirectionWhenCasting()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground);
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 2, Input.mousePosition.z));
        transform.LookAt(hitInfo.point);
    }
    //void AnimationControl()
    //{
    //    if (Mathf.Abs(direction.magnitude) > 0)
    //    {
    //        anim.SetBool("Running", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("Running", false);
    //    }
    //}

}
