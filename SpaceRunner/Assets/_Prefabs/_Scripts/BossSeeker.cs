using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSeeker : MonoBehaviour
{

    public GameObject player;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject DodgePoint;
    float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float MaxAmmo = 3;
    float Ammo;
    Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    bool canEvade = true;
    public GameObject Echo;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;

        ShootDelay = StartDelay;
        EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(player.transform);

        transform.parent.position = smoothpos;
        if (ShootDelay > 0)
        {
            ShootDelay -= Time.deltaTime;
        }
        if (ShootDelay <= 0)
        {
            StartCoroutine(shoot());
            ShootDelay = StartDelay;
            Ammo = MaxAmmo;
        }
        if (canEvade)
        {
            RaycastHit hit;
            if (Physics.SphereCast(DodgePoint.transform.position, 7, transform.forward, out hit, 10))
            {
                if (hit.transform.tag == "Shootable")
                {
                    StartCoroutine(Evade());
                }
            }
        }
        if (EHB.invulnerable)
        {
            GameObject temp = Instantiate(Echo, transform.position, Quaternion.identity);
            Destroy(temp, .15f);
        }
    }


    IEnumerator shoot()
    {

        GameObject bulletprefab;
        yield return new WaitForSeconds(.2f);
        bulletprefab = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        if (Ammo > 0)
        {
            if (Ammo > 1)
            {
                StartCoroutine(shoot());
            }
            Ammo -= 1;
        }
    }



    IEnumerator Evade()
    {

        float Randnum;
        Randnum = Random.Range(0, 10);
        if (Randnum == 0)
        {
            speed = 50;
            EHB.invulnerable = true;
            canEvade = false;
            yield return new WaitForSeconds(.2f);
            speed = 5;
            EHB.invulnerable = false;
            canEvade = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DodgePoint.transform.position, 7);
    }
}
