using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
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
    Vector3 movepos;
    public Vector3 smoothpos;
    public Vector3 offset;
    float Dir = 5;
    //EnemyHealthBar EHB;
    public float clampAngle = .5f;
    public float clamppos = 50f;
    public float rotspeed;
    public float _speed;
    public float rotResetSpeed;

    public GameObject Echo;
    public enum State { MoveRight, MoveLeft, ChooseDir }

    public State SeekerState;

    // Start is called before the first frame update
    void Start()
    {

        smoothpos = transform.position;

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


        transform.position = smoothpos;
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
        RaycastHit hit;
        if (Physics.SphereCast(DodgePoint.transform.position, 7, transform.forward, out hit, 10))
        {
            if (hit.transform.tag == "Shootable")
            {
                StartCoroutine(Evade());
            }
        }
        //Restricts z rot to max right
        if (transform.rotation.z <= -clampAngle)
        {
            transform.rotation = new Quaternion(0, 0, -clampAngle, transform.rotation.w);
        }
        //Restricts z rot to max left
        if (transform.rotation.z >= clampAngle)
        {
            transform.rotation = new Quaternion(0, 0, clampAngle, transform.rotation.w);
        }
        //if (EHB.invulnerable)
        //{
        //    GameObject temp = Instantiate(Echo, transform.position, Quaternion.identity);
        //    Destroy(temp, .15f);
        //}

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


                if (transform.position.x >= 50)
                {
                    SeekerState = State.MoveLeft;
                }
                else
                {

                    
                    transform.Rotate(0.0f, 0.0f, 1 * rotspeed);
                    smoothpos = Vector3.Lerp(transform.position,
                        transform.position = new Vector3(transform.position.x + Dir,
                        transform.position.y, transform.position.z), speed * Time.deltaTime);
                }
                break;
            case State.MoveLeft:

                if (transform.position.x <= -50)
                {
                    SeekerState = State.MoveRight;
                }
                else
                {

                    
                    transform.Rotate(0.0f, 0.0f, -1 * rotspeed);
                    smoothpos = Vector3.Lerp(transform.position,
                        transform.position = new Vector3(transform.position.x - Dir,
                        transform.position.y, transform.position.z), speed * Time.deltaTime);
                }
                /////
                //if (input != 0)
                //{



                //}
                //else
                //{
                //    //Resets rot back to normal after turning left
                //    if (transform.rotation.z > 0)
                //    {
                //        transform.Rotate(0.0f, 0.0f, -rotResetSpeed);
                //        if (transform.rotation.z < .05 && transform.rotation.z > 0)
                //        {
                //            transform.rotation = Quaternion.identity;
                //        }
                //    }
                //    //Resets rot back to normal after turning right
                //    if (transform.rotation.z < 0)
                //    {

                //        transform.Rotate(0.0f, 0.0f, rotResetSpeed);
                //        if (transform.rotation.z > -0.05f && transform.rotation.z < 0)
                //        {
                //            transform.rotation = Quaternion.identity;
                //        }
                //    }
                //}
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



    IEnumerator Evade()
    {

        float Randnum;
        Randnum = Random.Range(0, 10);
        if (Randnum == 0)
        {
            speed = 50;
            //EHB.invulnerable = true;
            yield return new WaitForSeconds(.2f);
            speed = 5;
            //EHB.invulnerable = false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(DodgePoint.transform.position, 7);
    }
}
