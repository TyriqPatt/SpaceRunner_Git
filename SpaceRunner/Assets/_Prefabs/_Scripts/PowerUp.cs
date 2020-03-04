using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour{

    [SerializeField]
    Rigidbody Pup;
    GameObject PUP;
    public float speed;
    public float waitTime;
    public bool HealthPickUp;
    public bool ShieldPickup;
    public bool AttackPickUp;
    public bool EnergyPickUp;
    Light lit;
    float _waitTime;
 
    
    void Start()
    {
        Pup = GetComponent<Rigidbody>();
        PUP = GameObject.Find("Pickupitem");
        lit = gameObject.AddComponent<Light>() as Light;
    }

    void Update()
    {
        //transform.Rotate(0, 45 * 5 * Time.deltaTime, 0);
        if (_waitTime < waitTime)
        {
            _waitTime += Time.deltaTime;
        }
        if(_waitTime >= waitTime)
        {
            transform.parent.Translate(0, 0, speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (AttackPickUp)
            {
                Canons C = other.GetComponentInParent<Canons>();
                C.HeavyCanons = true;
                C.heavyCanonDur = 0;
                Destroy(transform.parent.gameObject);
            }
            else if (EnergyPickUp)
            {
                Destroy(transform.parent.gameObject);
            }
            else if (HealthPickUp)
            {
                PlayerHealth PH = other.GetComponent<PlayerHealth>();
                PH.Heal(PH.HealAmount);
                Destroy(transform.parent.gameObject);
            }
            else if (ShieldPickup)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if (other.gameObject.name == "KillBox")
        {

            Destroy(transform.parent.gameObject);

        }
    }
}