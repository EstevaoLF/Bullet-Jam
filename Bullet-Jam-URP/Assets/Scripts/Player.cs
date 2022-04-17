using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0F;
    [SerializeField]
    float faceMoveDirectionSpeed = 100f;

    CharacterController controller;
    Animator anim;

    [SerializeField]
    Camera cam;

    // vector 3 for the player movement direction
    private Vector3 direction;

    [SerializeField]
    private GameObject[] spellPrefabs;
    [SerializeField]
    private Transform spawnPosition;

    float animationDuration = 0.75f;
    private bool canMove = true;

    public Transform MyTarget { get; set; }
    //this second target transform is created to prevent a bug that would occur if you selected a target, clicked a spell and immediatelly clicked outside the target
    //the spell would not move;
    private Transform spellTarget;

    //Get the spell index in the button onClick event
    private int spellIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
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
        if (!canMove)
        {
            direction = Vector3.zero;
        }
        else
        {
            direction = movementX + movementZ;
        }

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
            anim.SetBool("isWalking", true);
        }
        if (direction.magnitude == 0)
        {
            anim.SetBool("isWalking", false);
        }
    }

    public IEnumerator Attack()
    {
        spellTarget = MyTarget;
        anim.SetBool("isCasting", true);
        canMove = false;
        transform.rotation = Quaternion.LookRotation(spellTarget.position - transform.position);
        yield return new WaitForSeconds(animationDuration);
        StopAttacking();
    }

    private void StopAttacking()
    {
        anim.SetBool("isCasting", false);
        StopCoroutine(Attack());
        canMove = true;
    }

    public void StartAttack()
    {
        if (MyTarget != null && canMove)
        {
            StartCoroutine(Attack());
        }
    }

    public void GetSpellIndex(int index)
    {
        spellIndex = index;
    }

    //public void CastSpell()
    //{
    //        GameObject spellPrefab = Instantiate(spellPrefabs[spellIndex], spawnPosition.position, Quaternion.identity);
    //        Vector3 spellDirection = spellTarget.position - spawnPosition.position;
    //        spellDirection.y = 0;
    //        spellPrefab.GetComponent<Spell>().GetSpellDirection((spellDirection.normalized));
    //}
}