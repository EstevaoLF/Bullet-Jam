using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CastSpell : MonoBehaviour
{
    [SerializeField]
    GameObject frozenOrb, fireBall;

    [SerializeField]
    ParticleSystem shieldPs, teleportPs;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    Transform spawnPosition;

    [SerializeField]
    Image HealthPotImg, ManaPotImg, FrozenOrbImg;
    bool isHealthOnCooldown, isManaOnCooldown, isFrozenOrbOnCooldown;
    float potionCooldown = 5f;
    float frozenOrbCooldown = 0.5f;

    Player player;
    SimpleMove move;

    Camera cam;
    public LayerMask ground;

    float frozenOrbManaCost = 50;
    float fireBallManaCost = 15;
    float shieldManaCost = 50;
    float teleportManaCost = 25;

    WaitForSeconds shieldDuration = new WaitForSeconds(1.5f);
    WaitForSeconds animationDuration = new WaitForSeconds(0.25f);

    Animator anim;
    
    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
        move = GetComponent<SimpleMove>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
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
            move.FaceMoveDirectionWhenCasting();

            GameObject orb = Instantiate(frozenOrb, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            orb.GetComponent<Rigidbody>().AddForce(direction.normalized * 1000);
            player.currentMana -= frozenOrbManaCost * GameManager.Instance.manaModifier;

            StartCoroutine(CastAnimation());

            isFrozenOrbOnCooldown = true;
            FrozenOrbImg.enabled = true;
            FrozenOrbImg.fillAmount = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && player.currentMana >= fireBallManaCost)
        {
            move.FaceMoveDirectionWhenCasting();

            GameObject ball = Instantiate(fireBall, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            ball.GetComponent<Rigidbody>().velocity = (direction.normalized * 50);
            player.currentMana -= fireBallManaCost * GameManager.Instance.manaModifier;

            StartCoroutine(CastAnimation());

            audioSource.clip = SoundManager.Instance.fireballAudio[Random.Range(0, SoundManager.Instance.fireballAudio.Length)];
            audioSource.PlayOneShot(audioSource.clip);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && player.currentMana >= shieldManaCost && !shieldPs.isPlaying)
        {
            StartCoroutine(Shield());
            StartCoroutine(CastAnimation());
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isHealthOnCooldown)
        {
            isHealthOnCooldown = true;
            player.currentHealth += player.maxHealth * 0.3f;
            player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.maxHealth);
            HealthPotImg.enabled = true;
            HealthPotImg.fillAmount = 1;
        }
        if (Input.GetKeyDown(KeyCode.W) && !isManaOnCooldown)
        {
            isManaOnCooldown = true;
            player.currentMana += player.maxMana * 0.4f;
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

    IEnumerator CastAnimation()
    {
        anim.SetBool("isCasting", true);
        yield return animationDuration;
        anim.SetBool("isCasting", false);
    }
}
