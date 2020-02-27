﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public GameObject AstrSpawner;
    public GameObject Grav;
    public Transform[] GravTarget;
    public float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float MaxAmmo = 3;
    public float TimeBtwShots = 1;
    float Ammo;
    Vector3 movepos;
    public Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    public GameObject Echo;
    int RanGravTarget;
    public enum State { MoveRight, MoveLeft, ChooseDir, StartingMovePhase, MoveToLevelPos, MoveToOffsetPos, OffsetPhase, OffScreenIdle }
    public State BossState;
    bool LookingAtPlayer;
    float wait;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;
        BossState = State.OffScreenIdle;
        ShootDelay = StartDelay;
        //StartCoroutine(RandomDir());
        StartCoroutine(StartBossFight());
        EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
        LookingAtPlayer = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(player.transform);
        BossStates();
        FacingTarget();
        transform.parent.position = smoothpos;
        if (ShootDelay > 0 && ShootDelay <= 5)
        {
            ShootDelay -= Time.deltaTime;
        }
        if (ShootDelay <= 0)
        {
            ShootDelay = 5;
            Ammo = MaxAmmo;
            StartCoroutine(shootOrbs());
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
            case State.StartingMovePhase:
                LookingAtPlayer = true;
                if (transform.position.y > 30)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                        transform.parent.position = new Vector3(transform.parent.position.x,
                        transform.parent.position.y - Dir, transform.parent.position.z), speed * Time.deltaTime);
                }
                else if (transform.position.y <= 30)
                {
                    
                    RanGravTarget = Random.Range(0, GravTarget.Length);
                    //transform.parent.position = new Vector3(transform.parent.position.x, 30, transform.parent.position.z);
                    StartCoroutine(shootBlackHole(1));
                    BossState = State.OffsetPhase;
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
            case State.OffsetPhase:
                wait += Time.deltaTime;
                if(wait >= 5)
                {
                    BossState = State.MoveToLevelPos;
                    StopAllCoroutines();
                }
               
                break;
            case State.MoveToLevelPos:
                LookingAtPlayer = true;
                if (transform.position.y > 10)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                      transform.parent.position = new Vector3(transform.parent.position.x,
                      transform.parent.position.y - .5f, transform.parent.position.z), speed * Time.deltaTime);
                }
                if (transform.position.z > 146)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                       transform.parent.position = new Vector3(transform.parent.position.x,
                       transform.parent.position.y, transform.parent.position.z - Dir), speed * Time.deltaTime);
                }
                if (transform.position.z <= 146)
                {
                    BossState = State.ChooseDir;
                    wait = 0;
                    ShootDelay = 5;
                }
                break;
            case State.MoveToOffsetPos:
                LookingAtPlayer = true;
                if (transform.position.z < 200)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                       transform.parent.position = new Vector3(transform.parent.position.x,
                       transform.parent.position.y, transform.parent.position.z + 1), speed * Time.deltaTime);
                }
                if (transform.position.y < 30)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                      transform.parent.position = new Vector3(transform.parent.position.x,
                      transform.parent.position.y + 1f, transform.parent.position.z), speed * Time.deltaTime);
                }
                if (transform.position.y >= 30)
                {
                    BossState = State.OffsetPhase;
                    StartCoroutine(shootBlackHole(4));
                }
                break;
            case State.OffScreenIdle:

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
                transform.LookAt(player.transform);
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
                transform.LookAt(player.transform);
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

    void FacingTarget()
    {
        if (LookingAtPlayer)
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            Vector3 targetDirection = GravTarget[RanGravTarget].transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
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

    IEnumerator shootOrbs()
    {
        GameObject bulletprefab;
        yield return new WaitForSeconds(0);
        bulletprefab = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bulletprefab.GetComponent<GrowingBall>().Boss = transform.parent;

        if (Ammo > 0)
        {
            if (Ammo > 1)
            {
                StartCoroutine(shootOrbs());
            }
            Ammo -= 1;
        }
    }


    IEnumerator shootBlackHole(float time)
    {
        //GameObject bulletprefab;
        LookingAtPlayer = false;
        RanGravTarget = Random.Range(0, GravTarget.Length);
        yield return new WaitForSeconds(time);
        //bulletprefab = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        //Instantiate(Grav, GravTarget[RanGravTarget].position, GravTarget[RanGravTarget].rotation);
        Grav.transform.position = GravTarget[RanGravTarget].position;
        Grav.GetComponent<BlackHole>().enabled = true;
        Grav.transform.GetChild(0).gameObject.SetActive(true);
        Grav.transform.GetChild(1).gameObject.SetActive(false);
        //GravTarget = player.transform.position;
        LookingAtPlayer = true;
        yield return new WaitForSeconds(6);
        StartCoroutine(shootBlackHole(2));
    }

    IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(1);//7
        BossState = State.StartingMovePhase;
    }
}
