using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{


    public GameObject DodgePoint;
    public float speed;
    Vector3 movepos;
    Vector3 smoothpos;
    float Dir = 5;
    EnemyHealthBar EHB;
    bool canEvade = true;
    public GameObject Echo;
    public enum State { MoveRight, MoveLeft, ChooseDir, Shield, Health }
    public LayerMask layer;
    public State RecieverState;
    public State SupportState;
    // Start is called before the first frame update
    void Start()
    {
        smoothpos = transform.parent.position;
        SupportState = State.Shield;
        RecieverState = State.ChooseDir;
        StartCoroutine(RandomDir());
        EHB = GetComponent<EnemyHealthBar>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SeekerStates();
        transform.parent.position = smoothpos;
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
        switch (RecieverState)
        {
            case State.ChooseDir:
                float Randnum;
                Randnum = Random.Range(0, 2);
                if (Randnum == 0)
                {
                    RecieverState = State.MoveRight;
                }
                else if (Randnum == 1)
                {
                    RecieverState = State.MoveLeft;
                }
                break;
            case State.MoveRight:
                if (transform.parent.position.x >= 50)
                {
                    RecieverState = State.MoveLeft;
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
                    RecieverState = State.MoveRight;
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
            RecieverState = State.MoveRight;
        }
        else if (Randnum == 1)
        {
            RecieverState = State.MoveLeft;
        }
        StartCoroutine(RandomDir());
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
