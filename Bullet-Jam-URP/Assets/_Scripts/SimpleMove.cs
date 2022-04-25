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
        agent.speed = speed;
    }

    void FixedUpdate()
    {
        Move();
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
                anim.SetBool("Running", true);
            }
        }
        if (agent.velocity == Vector3.zero)
        {
            anim.SetBool("Running", false);
        }
    }

    public void FaceMoveDirectionWhenCasting()
    {
        agent.SetDestination(transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground);
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 2, Input.mousePosition.z));
        transform.LookAt(hitInfo.point);
    }
}
