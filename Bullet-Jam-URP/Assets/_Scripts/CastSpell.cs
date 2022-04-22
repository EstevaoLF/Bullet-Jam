using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField]
    GameObject frozenOrb;
    [SerializeField]
    GameObject fireBall;
    [SerializeField]
    ParticleSystem shieldPs;
    [SerializeField]
    ParticleSystem teleportPs;

    [SerializeField]
    Transform spawnPosition;

    Player player;
    SimpleMove move;

    Camera cam;
    public LayerMask ground;

    float frozenOrbManaCost = 50;
    float fireBallManaCost = 10;
    float shieldManaCost = 100;
    float teleportManaCost = 50;
    WaitForSeconds shieldDuration = new WaitForSeconds(3);    
    
    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
        move = GetComponent<SimpleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && player.currentMana >= teleportManaCost) 
        {
            Vector3 destination = GetMousePosition();
            teleportPs.gameObject.SetActive(true);
            teleportPs.Play();
            move.agent.SetDestination(destination);
            transform.position = destination;
            player.currentMana -= teleportManaCost;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.currentMana >= frozenOrbManaCost)
        {
            GameObject orb = Instantiate(frozenOrb, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            orb.GetComponent<Rigidbody>().AddForce(direction.normalized * 500);
            player.currentMana -= frozenOrbManaCost;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && player.currentMana >= fireBallManaCost)
        {
            GameObject ball = Instantiate(fireBall, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            ball.GetComponent<Rigidbody>().velocity = (direction.normalized * 50);
            player.currentMana -= fireBallManaCost;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && player.currentMana >= shieldManaCost && !shieldPs.isPlaying)
        {
            StartCoroutine(Shield());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.currentHealth += 150;
            player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.currentMana += 150;
            player.currentMana = Mathf.Clamp(player.currentMana, 0, player.maxMana);
        }
    }

    Vector3 GetMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ground))
        {
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    IEnumerator Shield()
    {
        shieldPs.gameObject.SetActive(true);
        player.isShielded = true;
        player.currentMana -= shieldManaCost;
        yield return shieldDuration;
        shieldPs.gameObject.SetActive(false);
        player.isShielded = false;
    }
}
