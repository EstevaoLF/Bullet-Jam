using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;

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

    public NavMeshAgent agent;
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
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
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

    private void FaceMoveDirectionWhenCasting()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground);
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 2, Input.mousePosition.z));
        transform.LookAt(hitInfo.point);
    }
}
