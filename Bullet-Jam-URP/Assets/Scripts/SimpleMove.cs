using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SimpleMove : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    float speed = 3.0F;

    [SerializeField]
    float faceMoveDirectionSpeed;

    CharacterController controller;

    Camera cam;
    Vector3 direction;
    private void Start()
    {
        cam = Camera.main;
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        Move();
        AnimationControl();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Multiply by cam.transform to make character walk direction relative to the camera

        Vector3 movementX = vertical * cam.transform.forward;
        movementX.y = 0;

        Vector3 movementZ = horizontal * cam.transform.right;
        movementZ.y = 0;

        direction = movementX + movementZ;


        //if statement prevents player from facing different direction if no key is being pressed       
        if (direction.magnitude != 0)
        {
            FaceMoveDirection();
            controller.SimpleMove(direction.normalized * speed);
        }

    }

    private void FaceMoveDirection()
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, faceMoveDirectionSpeed * Time.deltaTime);
    }

    void AnimationControl()
    {
        if (Mathf.Abs(direction.magnitude) > 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

}
