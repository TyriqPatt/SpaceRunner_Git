using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    public float speed;
    public float RiseSpeed;
    public float RotSpeed;
    public float PincerRotSpeed;
    public float Lifetime;
    public Meteor Meteor;
    public GameObject W_impact;
    public Flight_Controller FC;
    public Stunner St;
    public bool IsEnemy;
    [HideInInspector]public bool curvein;
    public Transform Player;
    public Transform target;
    float dist;
    bool canFollow;
    public enum State { Default, ShockWave, CurveShot, Missile, PincerMissile, GrowingBall }

    public State BulletType;

    // Use this for initialization
    void Start () {
        if (BulletType == State.ShockWave || BulletType == State.PincerMissile|| BulletType == State.GrowingBall)
        {
            canFollow = true;
        }
        if (BulletType == State.Missile)
        {
            Destroy(transform.parent.gameObject, Lifetime);
        }
        else
        {
            Destroy(gameObject, Lifetime);
        }
    }
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * Time.deltaTime * speed;
        if(BulletType == State.Missile)
        {
            if(transform.position.y <= 7)
            {
                transform.parent.position += transform.up * Time.deltaTime * RiseSpeed;
            }
        }
        else if (BulletType == State.ShockWave)
        {
            if (canFollow)
            {
                dist = Vector3.Distance(Player.position, transform.position);
               
                if (dist <= 35)
                {
                    canFollow = false;
                }

                transform.LookAt(Player);
            }
            else
            {
                Destroy(gameObject, 2);
            }
        }
        else if (BulletType == State.CurveShot)
        {
            if (curvein)
            {
                transform.Rotate(0, RotSpeed, 0);
            }
            else
            {
                transform.Rotate(0, -RotSpeed, 0);
            }
        }
        else if (BulletType == State.PincerMissile)
        {
            transform.GetChild(0).Rotate(0, 0, -5);
            if (canFollow)
            {
                Vector3 targetDirection = target.transform.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, PincerRotSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                //dist = Vector3.Distance(Player.position, transform.position);

                if (dist <= 35)
                {
                    //canFollow = false;
                }

                
            }
            else
            {
               // Destroy(gameObject, 2);
            }
        }
        if (BulletType == State.GrowingBall)
        {
            if (canFollow)
            {
                dist = Vector3.Distance(Player.position, transform.position);

                if (dist <= 35)
                {
                    canFollow = false;
                }

                transform.LookAt(Player);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (IsEnemy)
        {
            if (other.gameObject.tag == "Player")
            {
                
                GameObject tempObj;
                tempObj = Instantiate(W_impact, transform.position, other.transform.rotation);
                //tempObj.transform.SetParent(other.transform);
                Destroy(tempObj, 1);
                if (BulletType == State.ShockWave)
                {
                    St.hitplayer = true;
                    FC = other.GetComponentInParent<Flight_Controller>();
                    StartCoroutine(FC.Disrupted());
                    Destroy(gameObject, 2);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Asteroid")
            {
                Destroy(gameObject);
                GameObject tempObj;
                tempObj = Instantiate(W_impact, transform.position, other.transform.rotation);
                //tempObj.transform.SetParent(other.transform);
                Destroy(tempObj, 2);
                if (BulletType == State.Missile)
                {
                    Destroy(transform.parent.gameObject);
                }

            }
        }
    }
}
