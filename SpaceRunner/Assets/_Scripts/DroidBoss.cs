using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject Pillar;
    public GameObject Grav;
    public Transform GravTarget;
    float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float MaxAmmo = 3;
    public float TimeBtwShots = 1;
    float Ammo;
    Vector3 movepos;
    public Vector3 smoothpos;
    public Vector3 offset;
    float Dir = 5;
    EnemyHealthBar EHB;
    public GameObject Echo;
    public enum State { MoveRight, MoveLeft, ChooseDir, BtwnPhases, OffsetPhaseGrav, OffsetPhase, OffsetPhasePillar }
    public State BossState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;
        BossState = State.BtwnPhases;
        ShootDelay = StartDelay;
        //StartCoroutine(RandomDir());
        EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(player.transform);
        BossStates();
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
        if (EHB.invulnerable)
        {
            GameObject temp = Instantiate(Echo, transform.position, Quaternion.identity);
            Destroy(temp, .15f);
        }
    }

    void BossStates()
    {
        switch (BossState)
        {
            case State.BtwnPhases:
                if(transform.position.y > 50)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                        transform.parent.position = new Vector3(transform.parent.position.x,
                        transform.parent.position.y - Dir, transform.parent.position.z), speed * Time.deltaTime);
                }
                else if (transform.position.y <= 50)
                {
                    BossState = State.OffsetPhaseGrav;
                    //smoothpos = Vector3.Lerp(transform.parent.position,
                    //    transform.parent.position = new Vector3(transform.parent.position.x,
                    //    transform.parent.position.y - Dir, transform.parent.position.z), speed * Time.deltaTime);
                }
                else if (transform.position.y == 10)
                {
                    //smoothpos = Vector3.Lerp(transform.parent.position,
                    //   transform.parent.position = new Vector3(transform.parent.position.x,
                    //   transform.parent.position.y + Dir, transform.parent.position.z), speed * Time.deltaTime);
                }
                break;
            case State.OffsetPhaseGrav:

                break;
            case State.OffsetPhase:

                break;
            case State.OffsetPhasePillar:

                break;
            case State.ChooseDir:
                float Randnum;
                Randnum = Random.Range(0, 2);
                if (Randnum == 0)
                {
                    BossState = State.MoveRight;
                }
                else if (Randnum == 1)
                {
                    BossState = State.MoveLeft;
                }
                break;
            case State.MoveRight:
                if (transform.parent.position.x >= 50)
                {
                    BossState = State.MoveLeft;
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
                    BossState = State.MoveRight;
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
            BossState = State.MoveRight;
        }
        else if (Randnum == 1)
        {
            BossState = State.MoveLeft;
        }

        StartCoroutine(RandomDir());

    }

    IEnumerator shoot()
    {
        GameObject bulletprefab;
        yield return new WaitForSeconds(TimeBtwShots);
        bulletprefab = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bulletprefab.GetComponent<GrowingBall>().Boss = transform.parent;
        if (Ammo > 0)
        {
            if (Ammo > 1)
            {
                StartCoroutine(shoot());
            }
            Ammo -= 1;
        }
    }
}
