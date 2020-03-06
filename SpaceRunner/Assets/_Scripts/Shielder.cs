using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : MonoBehaviour
{
    public GameObject player;
    public GameObject beam;
    public GameObject Chargebeam;
    public GameObject DodgePoint;
    public GameObject Mine;
    public float ShootDelay;
    public float StartDelay = 5;
    public float speed;
    public float rotspeed;
    Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    bool BeamOn;
    public GameObject Echo;
    bool MoveDir;
    bool canBeam;
    bool canEvade = true;
    public Transform MineSpawn, MineSpawn2, MineSpawn3;
    public enum State { MoveRight, MoveLeft, ChooseDir, ShootBeam, Charge, idle}

    public State SeekerState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smoothpos = transform.parent.position;

        SeekerState = State.ChooseDir;

        ShootDelay = StartDelay;
        EHB = GetComponent<EnemyHealthBar>();
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
        if (ShootDelay <= 0 && !canBeam)
        {
            SeekerState = State.Charge;
            //StartCoroutine(shoot());
            canBeam = true;
        }
        if (canEvade)
        {
            RaycastHit hit;
            if (Physics.SphereCast(DodgePoint.transform.position, 7, transform.forward, out hit, 10) && !BeamOn)
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

            case State.Charge:

                //Store player position relative to this.position on x (if left or right) when charging or at beginning of charge
                //Move that direction when shootng beam
                Chargebeam.SetActive(true);
                StartCoroutine(shoot());
                SeekerState = State.ChooseDir;
                break;

            case State.idle:

               

                break;

            case State.ShootBeam:

                //Store player position relative to this.position on x (if left or right) when charging or at beginning of charge
                //Move that direction when shootng beam
                Vector3 tempPos;
                tempPos = transform.parent.position;
                if (tempPos.x <= player.transform.position.x)//Right
                {
                    if (transform.parent.position.x >= 50)
                    {
                        
                    }
                    else
                    {
                        smoothpos = Vector3.Lerp(transform.parent.position,
                            transform.parent.position = new Vector3(transform.parent.position.x + Dir,
                            transform.parent.position.y, transform.parent.position.z), speed * Time.deltaTime);
                    }
                }
                else if (tempPos.x >= player.transform.position.x)//Left
                {
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
                }
                //Chargebeam.SetActive(false);
                //beam.SetActive(true);
                break;
        }
    }

    void DropMines()
    {
        Instantiate(Mine, MineSpawn.position, MineSpawn.transform.rotation);
        Instantiate(Mine, MineSpawn2.position, MineSpawn2.transform.rotation);
        Instantiate(Mine, MineSpawn3.position, MineSpawn3.transform.rotation);
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(5f);
        SeekerState = State.ShootBeam;
        beam.SetActive(true);
        Chargebeam.SetActive(false);
        BeamOn = true;
        speed = 5;
        yield return new WaitForSeconds(2f);
        beam.SetActive(false);
        BeamOn = false;
        SeekerState = State.ChooseDir;
        speed = 0;
        ShootDelay = StartDelay;
        canBeam = false;
    }



    IEnumerator Evade()
    {
        float Randnum;
        Randnum = Random.Range(0, 7);
        if (Randnum == 0)
        {
            speed = 50;
            EHB.invulnerable = true;
            DropMines();
            canEvade = false;
            yield return new WaitForSeconds(.2f);
            speed = 0;
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
