using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject Explosion;
    public float Damage = 0;
    public float CurrentHealth;
    Material CurMat;
    public Material HitMat;
    public bool NormMeteor;
    public bool Pickup;
    public bool Hitplayer;
    public GameObject[] PickUps;

    // Start is called before the first frame update
    void Start()
    {
        Damage = 5;
        CurrentHealth = 30;
        if (NormMeteor)
        {
            CurMat = transform.GetChild(0).GetComponent<Renderer>().material;
        }
        else
        {
            CurMat = GetComponent<Renderer>().material;
        }
        Explosion = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float DamageValue)
    {
        CurrentHealth -= DamageValue;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KillBox")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Shootable")
        {
            DealDamage(Damage * PlayerHealth.DmgMultiplier);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "EnemyLaser")
        {
            DealDamage(Damage * PlayerHealth.DmgMultiplier);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "TurretShot")
        {
            DealDamage(Damage + 5 * PlayerHealth.DmgMultiplier);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Missile")
        {
            DealDamage(Damage + 25);
            StartCoroutine(Hit());
        }
        if (other.gameObject.tag == "Debris")
        {
            DealDamage(Damage * PlayerHealth.DmgMultiplier);
            Die();
        }
        if (other.gameObject.tag == "Player")
        {
            Hitplayer = true;
            Die();
            
        }
        if (other.gameObject.tag == "Beam")
        {
            Die();
        }

    }

    private void Die()
    {
        if (!Hitplayer)
        {
            if (Pickup)
            {
                int randSpawn = Random.Range(0, 5);
                int randPu = Random.Range(0, PickUps.Length);
                if (randSpawn <= 5)
                {
                    Instantiate(PickUps[randPu], transform.position, transform.parent.rotation);
                }
            }
        }
        Destroy(gameObject);
        //GameObject tempObj;
        //tempObj = Instantiate(Explosion, transform.position, transform.rotation);
        //Destroy(tempObj, 2);
        Explosion.transform.parent = null;
        Explosion.SetActive(true);
        Destroy(Explosion, 2);
    }

    IEnumerator Hit()
    {
        if (NormMeteor)
        {

            transform.GetChild(0).GetComponent<Renderer>().material = HitMat;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = HitMat;
        }
        yield return new WaitForSeconds(.2f);
        if (NormMeteor)
        {

            transform.GetChild(0).GetComponent<Renderer>().material = CurMat;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = CurMat;
        }
        
    }
}
