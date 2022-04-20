using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    [SerializeField]
    GameObject frozenOrb;

    [SerializeField]
    Transform spawnPosition;

    Player player;

    Camera cam;
    public LayerMask ground;

    float frozenOrbManaCost = 50;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && player.currentMana >= frozenOrbManaCost)
        {
            GameObject orb = Instantiate(frozenOrb, spawnPosition.position, transform.rotation);
            Vector3 direction = GetMousePosition() - transform.position;
            orb.GetComponent<Rigidbody>().AddForce(direction.normalized * 500);
            player.currentMana -= frozenOrbManaCost;
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
}
