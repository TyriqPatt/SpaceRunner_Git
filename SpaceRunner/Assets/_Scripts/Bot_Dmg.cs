using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Dmg : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
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
    //EnemyHealthBar EHB;
    public enum State { MoveRight, MoveLeft, ChooseDir }

    public State SeekerState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;

        SeekerState = State.ChooseDir;

        ShootDelay = StartDelay;
        StartCoroutine(RandomDir());
        //EHB = GetComponent<EnemyHealthBar>();
        Ammo = MaxAmmo;
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
            StartCoroutine(shoot());
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
