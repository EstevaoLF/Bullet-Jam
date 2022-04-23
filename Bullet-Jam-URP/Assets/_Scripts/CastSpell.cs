using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSpell : MonoBehaviour
{
    [SerializeField]
    GameObject frozenOrb, fireBall;

    [SerializeField]
    ParticleSystem shieldPs, teleportPs;


    [SerializeField]
    Transform spawnPosition;

    [SerializeField]
    Image HealthPotImg, ManaPotImg, FrozenOrbImg;
    bool isHealthOnCooldown, isManaOnCooldown, isFrozenOrbOnCooldown;
    float potionCooldown = 5f;
    float frozenOrbCooldown = 1f;

    Player player;
    SimpleMove move;

    Camera cam;
    public LayerMask ground;

    float frozenOrbManaCost = 50;
    float fireBallManaCost = 20;
    float shieldManaCost = 75;
    float teleportManaCost = 75;

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
            player.currentMana -= teleportManaCost * GameManager.Instance.manaModifier;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.currentMana >= frozenOrbManaCost && !isFrozenOrbOnCooldown)
        {
            GameObject orb = Instantiate(frozenOrb, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            orb.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000);
            player.currentMana -= frozenOrbManaCost * GameManager.Instance.manaModifier;

            isFrozenOrbOnCooldown = true;
            FrozenOrbImg.enabled = true;
            FrozenOrbImg.fillAmount = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && player.currentMana >= fireBallManaCost)
        {
            GameObject ball = Instantiate(fireBall, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            ball.GetComponent<Rigidbody>().velocity = (direction.normalized * 50);
            player.currentMana -= fireBallManaCost * GameManager.Instance.manaModifier;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && player.currentMana >= shieldManaCost && !shieldPs.isPlaying)
        {
            StartCoroutine(Shield());
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isHealthOnCooldown)
        {
            isHealthOnCooldown = true;
            player.currentHealth += 150;
            player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
            HealthPotImg.enabled = true;
            HealthPotImg.fillAmount = 1;
        }
        if (Input.GetKeyDown(KeyCode.W) && !isManaOnCooldown)
        {
            isManaOnCooldown = true;
            player.currentMana += 150;
            player.currentMana = Mathf.Clamp(player.currentMana, 0, player.maxMana);
            ManaPotImg.enabled = true;
            ManaPotImg.fillAmount = 1;
        }

        if (isHealthOnCooldown)
        {
            HealthPotImg.fillAmount -= Time.deltaTime / potionCooldown;
            if (HealthPotImg.fillAmount <= 0)
            {
                isHealthOnCooldown = false;
            }
        }

        if (isManaOnCooldown)
        {
            ManaPotImg.fillAmount -= Time.deltaTime / potionCooldown;
            if (ManaPotImg.fillAmount <= 0)
            {
                isManaOnCooldown = false;
            }
        }
        if (isFrozenOrbOnCooldown)
        {
            FrozenOrbImg.fillAmount -= Time.deltaTime / frozenOrbCooldown;
            if (FrozenOrbImg.fillAmount <= 0)
            {
                isFrozenOrbOnCooldown = false;
            }
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
        player.currentMana -= shieldManaCost * GameManager.Instance.manaModifier;
        yield return shieldDuration;
        shieldPs.gameObject.SetActive(false);
        player.isShielded = false;
    }
}
