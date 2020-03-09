using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject Orb;
    public GameObject orbSpawn;
    public GameObject bullet;
    public GameObject AstrSpawner;
    public GameObject Grav;
    public GameObject[] Droids;
    public Transform[] GravTarget;
    public Transform[] FourSpread;
    public Transform[] ThreeSpread;
    public float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    public GameObject Echo;
    int RanGravTarget;
    public enum State { MoveRight, MoveLeft, ChooseDir, StartingMovePhase, MoveToLevelPos, MoveToOffsetPos, MoveToOffsetPosp2, OffsetPhase, OffScreenIdle }
    public State BossState;
    bool LookingAtPlayer;
    float wait;
    bool canShoot;
    bool CanResetPos;
    float ThresLevel;
    float Round;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;
        BossState = State.OffScreenIdle;
        ShootDelay = StartDelay;
        StartCoroutine(StartBossFight());
        EHB = GetComponent<EnemyHealthBar>();
        LookingAtPlayer = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        BossStates();
        FacingTarget();
        HealthThresholds();
        if (!CanResetPos)
        {
            transform.parent.position = smoothpos;
        }
        if (canShoot)
        {
            if (ShootDelay > 0)
            {
                ShootDelay -= Time.deltaTime;
            }
            if (ShootDelay <= 0)
            {
                StartDelay = 8;
                ShootDelay = StartDelay;
                StartCoroutine(shootOrbs());
            }
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
                if (transform.parent.position.y > 13)
                {
                    CanResetPos = false;
                    smoothpos = Vector3.Lerp(transform.parent.position,
                        transform.parent.position = new Vector3(transform.parent.position.x,
                        transform.parent.position.y - Dir, transform.parent.position.z), speed * Time.deltaTime);
                    if (Round == 1)
                    {
                        Droids[0].SetActive(true);
                    }
                    else if (Round == 2)
                    {
                        Droids[1].SetActive(true);
                    }
                    else if (Round == 3)
                    {
                        Droids[2].SetActive(true);
                    }
                }
                else if (transform.parent.position.y <= 13)
                {
                    BossState = State.ChooseDir;
                    ShootDelay = StartDelay;
                    canShoot = true;
                }
                break;
            case State.OffsetPhase:

               
                break;
            case State.MoveToLevelPos:
                LookingAtPlayer = true;
                if (transform.parent.position.y < 105)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                      transform.parent.position = new Vector3(transform.parent.position.x,
                      transform.parent.position.y + 3f, transform.parent.position.z), speed * Time.deltaTime);
                }
                else if (transform.parent.position.y >= 105)
                {
                    transform.parent.position = new Vector3(0, 105, 150);
                    CanResetPos = true;
                    BossState = State.StartingMovePhase;
                }
                break;
            case State.MoveToOffsetPos:
                LookingAtPlayer = true;
                if (transform.parent.position.y < 105)
                {
                    smoothpos = Vector3.Lerp(transform.parent.position,
                      transform.parent.position = new Vector3(transform.parent.position.x,
                      transform.parent.position.y + 3f, transform.parent.position.z), speed * Time.deltaTime);
                }
                else if (transform.parent.position.y >= 105)
                {
                    transform.parent.position = new Vector3(0, 105, 200);
                    CanResetPos = true;
                    BossState = State.MoveToOffsetPosp2;
                    AstrSpawner.SetActive(true);
                }
                break;
            case State.MoveToOffsetPosp2:
                LookingAtPlayer = true;
                if (transform.parent.position.y > 30)
                {
                    CanResetPos = false;
                    smoothpos = Vector3.Lerp(transform.parent.position,
                      transform.parent.position = new Vector3(transform.parent.position.x,
                      transform.parent.position.y - 3f, transform.parent.position.z), speed * Time.deltaTime);
                }
                if (transform.parent.position.y <= 30)
                {
                    BossState = State.OffsetPhase;
                    StartCoroutine(shootBlackHole(3));
                }
                break;
            case State.OffScreenIdle:

                break;
            case State.ChooseDir:
                Round += 1;
                if(Round == 2)
                {
                    Droids[0].transform.parent = null;
                    Droids[0].transform.position = new Vector3(Droids[0].transform.position.x, 7, 135);
                    Droids[0].transform.GetChild(0).GetComponent<Seeker>().enabled = true;
                }
                else if (Round == 3)
                {
                    Droids[1].transform.parent = null;
                    Droids[1].transform.position = new Vector3(Droids[1].transform.position.x, 7, 135);
                    Droids[1].transform.GetChild(0).GetComponent<Stunner>().enabled = true;
                }
                else if (Round == 4)
                {
                    Droids[2].transform.parent = null;
                    Droids[2].transform.position = new Vector3(Droids[2].transform.position.x, 7, 135);
                    Droids[2].transform.GetChild(0).GetComponent<Shielder>().enabled = true;
                }
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

    void HealthThresholds()
    {
        if(ThresLevel == 0)
        {
            if (EHB.CurrentHealth <= 750)
            {
                StopAllCoroutines();
                canShoot = false;
                BossState = State.MoveToOffsetPos;
                ThresLevel = 1;
            }
        }
        else if(ThresLevel == 1)
        {

            if (EHB.CurrentHealth <= 500)
            {
                StopAllCoroutines();
                canShoot = false;
                BossState = State.MoveToOffsetPos;
                ThresLevel = 2;
            }
        }
        else if(ThresLevel == 2)
        {
            if (EHB.CurrentHealth <= 250)
            {
                StopAllCoroutines();
                canShoot = false;
                BossState = State.MoveToOffsetPos;
                ThresLevel = 3;
            }
        }
        else if(ThresLevel == 3)
        {

        }
    }

    public void BackToLevelPos()
    {
        StopAllCoroutines();
        BossState = State.MoveToLevelPos;
        AstrSpawner.SetActive(false);
    }

    IEnumerator RandomDir()
    {
        yield return new WaitForSeconds(Random.Range(3, 8));
        float Randnum;
        Randnum = Random.Range(0, 4);
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
        bulletprefab = Instantiate(Orb, orbSpawn.transform.position, orbSpawn.transform.rotation);
        bulletprefab.GetComponent<GrowingBall>().Boss = transform.parent;
        bulletprefab.GetComponent<GrowingBall>().db = this.GetComponent<DroidBoss>();
        bulletprefab.GetComponent<ShotBehavior>().Player = player.transform;
    }

    public IEnumerator SpreadShot()
    {
        if(BossState == State.MoveLeft || BossState == State.MoveRight)
        {
            GameObject bulletprefab;
            yield return new WaitForSeconds(.5f);
            for (int i = 0; i < FourSpread.Length; i++)
            {
                bulletprefab = Instantiate(bullet, FourSpread[i].transform.position, FourSpread[i].transform.rotation);
            }
            yield return new WaitForSeconds(.5f);
            for (int i = 0; i < ThreeSpread.Length; i++)
            {
                bulletprefab = Instantiate(bullet, ThreeSpread[i].transform.position, ThreeSpread[i].transform.rotation);
            }
        }
        
    }

    IEnumerator shootBlackHole(float time)
    {
        LookingAtPlayer = false;
        RanGravTarget = Random.Range(0, GravTarget.Length);
        yield return new WaitForSeconds(time);
        Grav.transform.position = GravTarget[RanGravTarget].position;
        Grav.GetComponent<BlackHole>().enabled = true;
        Grav.transform.GetChild(0).gameObject.SetActive(true);
        Grav.transform.GetChild(1).gameObject.SetActive(false);
        LookingAtPlayer = true;
        yield return new WaitForSeconds(4);
        StartCoroutine(shootBlackHole(2));
    }

    IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(1);//7
        BossState = State.StartingMovePhase;
    }
}
