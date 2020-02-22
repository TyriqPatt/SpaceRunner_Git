using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public GameObject player;
    public GameObject _Blackhole;
    public GameObject bulletSpawn;
    float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float MaxAmmo = 3;
    float Ammo;
    Vector3 movepos;
    public Vector3 smoothpos;
    public Vector3 offset;
    float Dir = 5;
    EnemyHealthBar EHB;
    bool doOnce;
    public float healthThreshold;
    bool canEvade = true;
    public enum State { MoveRight, MoveLeft, ChooseDir, ActivateBlackHole }

    public State SeekerState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;
        
        SeekerState = State.ChooseDir;

        ShootDelay = StartDelay;
        StartCoroutine(RandomDir());
        EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
        healthThreshold = EHB.CurrentHealth;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SeekerStates();

        transform.parent.position = smoothpos;
        if (ShootDelay > 0)
        {
            ShootDelay -= Time.deltaTime;
        }
        if (ShootDelay <= 0)
        {
            //StartCoroutine(shoot());
            ShootDelay = StartDelay;
            Ammo = MaxAmmo;
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
                CheckHealth();
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
                CheckHealth();
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
            case State.ActivateBlackHole:

                //SeekerState = State.ChooseDir;
                break;
        }
    }

    void CheckHealth()
    {
        if (EHB.CurrentHealth <= healthThreshold - 25 && !doOnce)
        {
            StartCoroutine(shoot());
            doOnce = true;
        }

    }

    IEnumerator Evade()
    {
        
        speed = 50;
        EHB.invulnerable = true;
        canEvade = false;
        yield return new WaitForSeconds(.2f);
        speed = 5;
        EHB.invulnerable = false;
        canEvade = true;
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
        
        SeekerState = State.ActivateBlackHole;
        StopCoroutine(RandomDir());
        yield return new WaitForSeconds(.2f);
        //bulletprefab = Instantiate(Blackhole, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        _Blackhole.transform.position = transform.position;
        _Blackhole.GetComponent<BlackHole>().enabled = true;
        _Blackhole.transform.GetChild(0).gameObject.SetActive(true);
        _Blackhole.transform.GetChild(1).gameObject.SetActive(false);
        _Blackhole.transform.parent = null;
        yield return new WaitForSeconds(4f);
        SeekerState = State.ChooseDir;
        StartCoroutine(Evade());
        healthThreshold = EHB.CurrentHealth;
        doOnce = false;
        //_Blackhole.transform.parent = this.transform;
    }
}
