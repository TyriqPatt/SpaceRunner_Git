using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{

    public Transform player;
    public Flight_Controller fc;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bulletSpawn;
    public Transform CurveSpawn, CurveSpawn2, CurveSpawn3, CurveSpawn4, CurveSpawn5, CurveSpawn6;
    Transform curSpawnr, curSpawnl;
    public GameObject DodgePoint;
    float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float MaxAmmo = 3;
    public float AltMaxAmmo = 3;
    float Ammo;
    float AltAmmo;
    Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    public bool hitplayer;
    bool canEvade = true;
    public GameObject Echo;
    public enum State { MoveRight, MoveLeft, ChooseDir }

    public State SeekerState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fc = player.GetComponentInParent<Flight_Controller>();
        smoothpos = transform.parent.position;
        SeekerState = State.ChooseDir;

        ShootDelay = StartDelay;
        StartCoroutine(RandomDir());
        EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
        AltAmmo = AltMaxAmmo;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(player);
        SeekerStates();
        transform.parent.position = smoothpos;
        if (ShootDelay > 0)
        {
            ShootDelay -= Time.deltaTime;
        }
        if (ShootDelay <= 0)
        {
            StartCoroutine(shoot());
            StartCoroutine(Delay());
            ShootDelay = StartDelay;
            Ammo = MaxAmmo;
            AltAmmo = AltMaxAmmo;
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

    void SeekerStates()
    {
        switch (SeekerState)
        {
            case State.ChooseDir:
                float Randnum;
                Randnum = Random.Range(0, 2);
                if (Randnum == 0)
                {
                    SeekerState = State.MoveRight;
                }
                else if (Randnum == 1)
                {
                    SeekerState = State.MoveLeft;
                }


                break;
            case State.MoveRight:


                if (transform.parent.position.x >= 50)
                {
                    SeekerState = State.MoveLeft;
                }
                else
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                        transform.parent.position = new Vector3(transform.parent.position.x + Dir,
                        transform.parent.position.y, transform.parent.position.z), speed * Time.deltaTime);
                }
                break;
            case State.MoveLeft:

                if (transform.parent.position.x <= -50)
                {
                    SeekerState = State.MoveRight;
                }
                else
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                        transform.parent.position = new Vector3(transform.parent.position.x - Dir,
                        transform.parent.position.y, transform.parent.position.z), speed * Time.deltaTime);
                }

                break;

        }
    }

    IEnumerator RandomDir()
    {
        yield return new WaitForSeconds(Random.Range(5, 8));
        float Randnum;
        Randnum = Random.Range(0, 5);
        if (Randnum == 0)
        {
            SeekerState = State.MoveRight;
        }
        else if (Randnum == 1)
        {
            SeekerState = State.MoveLeft;
        }

        StartCoroutine(RandomDir());

    }

    IEnumerator shoot()
    {
        GameObject bulletprefab;
        yield return new WaitForSeconds(.2f);
        bulletprefab = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bulletprefab.GetComponent<ShotBehavior>().Player = player.transform;
        bulletprefab.GetComponent<ShotBehavior>().St = this.GetComponent<Stunner>();
        if (Ammo > 0)
        {
            if (Ammo > 1)
            {
                StartCoroutine(shoot());
            }
            Ammo -= 1;
        }
    }

    IEnumerator AltFire()
    {

        GameObject bulletprefab;
        GameObject bulletprefab2;
        curSpawnr = CurveSpawn4;
        curSpawnl = CurveSpawn;
        yield return new WaitForSeconds(.2f);
        bulletprefab = Instantiate(bullet2, curSpawnl.position, curSpawnl.transform.rotation);
        bulletprefab2 = Instantiate(bullet2, curSpawnr.position, curSpawnr.transform.rotation);
        bulletprefab2.GetComponent<ShotBehavior>().curvein = true;
        hitplayer = false;
        if (AltAmmo > 0)
        {
            if (AltAmmo > 1)
            {
                StartCoroutine(AltFire());

            }
            if (AltAmmo == 2)
            {
                curSpawnr = CurveSpawn5;
                curSpawnl = CurveSpawn2;
            }
            if (AltAmmo == 1)
            {
                curSpawnr = CurveSpawn6;
                curSpawnl = CurveSpawn3;
            }
            AltAmmo -= 1;
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.9f);
        if (hitplayer)
        {
            StartCoroutine(AltFire());
        }
    }

    IEnumerator Evade()
    {

        float Randnum;
        Randnum = Random.Range(0, 13);
        if (Randnum == 0)
        {
            speed = 50;
            EHB.invulnerable = true;
            canEvade = false;
            yield return new WaitForSeconds(.2f);
            speed = 8;
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
